using Azure;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RestaurantAPI.Entities;
using RestaurantAPI.IntegrationTests.Helpers;
using RestaurantAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.IntegrationTests
{
    public class RestaurantControllerTests
    {
        private HttpClient _client;
        private WebApplicationFactory<Program> _factory;

        public RestaurantControllerTests()
        {
            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbContextOptions = services
                            .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<RestaurantDbContext>));

                        services.Remove(dbContextOptions!);

                        services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                        services.AddMvc(options => options.Filters.Add(new FakeUserFilter()));

                        services.AddDbContext<RestaurantDbContext>(options => options.UseInMemoryDatabase("RestaurantDb"));
                    });
                });

            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task CreateRestaurant_WithValidModel_ReturnsCreatedStatus()
        {
            //arrange
            var model = new CreateRestaurantDto()
            {
                Name = "Test Restaurant",
                City = "Poznań",
                Street = "Wolności 13"
            };

            var httpContent = model.ToJsonHttpContent();

            //act
            var response = await _client.PostAsync("api/restaurant", httpContent);

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
        }

        public async Task CreateRestaurant_WithInvalidModel_ReturnsBadRequest()
        {
            //arrange
            var model = new CreateRestaurantDto()
            {
                ContactEmail = "test@email.com",
                ContactNumber = "123456789",
                Description = "Test Desc"
            };

            var httpContent = model.ToJsonHttpContent();

            //act
            var response = await _client.PostAsync("api/restaurant", httpContent);

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        private void SeedRestaurant(Restaurant restaurant)
        {
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory!.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<RestaurantDbContext>();

            _dbContext!.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task Delete_ForNoRestaurantOwner_ReturnsForbidden()
        {
            //arrange
            var restaurant = new Restaurant()
            {
                CreatedById = 9999999,
                Name = "Test"
            };

            SeedRestaurant(restaurant);

            //act
            var response = await _client.DeleteAsync("/api/restaurant/" + restaurant.Id);

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Delete_ForRestaurantOwner_ReturnsNoContent()
        {
            //arrange
            var restaurant = new Restaurant()
            {
                CreatedById = 1,
                Name = "Test"
            };

            SeedRestaurant(restaurant);

            //act
            var response = await _client.DeleteAsync("/api/restaurant/" + restaurant.Id);

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_ForNotExistingRestaurant_ReturnsNotFound()
        {
            //act
            var response = await _client.DeleteAsync("/api/restaurant/99999999");

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData("pageSize=5&pageNumber=1")]
        [InlineData("pageSize=10&pageNumber=3")]
        [InlineData("pageSize=15&pageNumber=5")]
        public async Task GetAll_WithQueryParameters_ReturnsOkResult(string queryParams)
        {
            //act
            var response = await _client.GetAsync("/api/restaurant?" + queryParams);

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("pageSize=1&pageNumber=1")]
        [InlineData("pageSize=11&pageNumber=3")]
        [InlineData("")]
        [InlineData(null)]
        public async Task GetAll_WithQueryInvalidParameters_ReturnsBadRequestResult(string queryParams)
        {
            //act
            var response = await _client.GetAsync("/api/restaurant?" + queryParams);

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
