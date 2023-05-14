using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RestaurantAPI.Models;
using RestaurantAPI.IntegrationTests.Helpers;
using RestaurantAPI.Services;

namespace RestaurantAPI.IntegrationTests
{
    public class AccountControllerTests
    {
        private HttpClient _client;
        private WebApplicationFactory<Program> _factory;
        private Mock<IAccountService> _accountServiceMock = new Mock<IAccountService>();

        public AccountControllerTests()
        {
            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbContextOptions = services
                            .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<RestaurantDbContext>));

                        services.Remove(dbContextOptions!);

                        services.AddSingleton<IAccountService>(_accountServiceMock.Object);

                        services.AddDbContext<RestaurantDbContext>(options => options.UseInMemoryDatabase("RestaurantDb"));
                    });
                });

            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task RegisterUser_ForValidModel_ReturnsOk()
        {
            //arrange
            var registerUser = new RegisterUserDto()
            {
                Email = "test@test.com",
                Password = "password123",
                ConfirmPassword = "password123"
            };

            var httpContent = registerUser.ToJsonHttpContent();

            //act
            var response = await _client.PostAsync("/api/account/register", httpContent);

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task RegisterUser_ForInvalidModel_ReturnsBadRequest()
        {
            //arrange
            var registerUser = new RegisterUserDto()
            {
                Password = "password123",
                ConfirmPassword = "password321"
            };

            var httpContent = registerUser.ToJsonHttpContent();

            //act
            var response = await _client.PostAsync("/api/account/register", httpContent);

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task login_ForRegisteredUser_ReturnsOk()
        {
            //arrange
            _accountServiceMock
                .Setup(x => x.GenerateJwt(It.IsAny<LoginDto>()))
                .Returns("jwt");


            var login = new LoginDto()
            {
                Email = "test@test.com",
                Password = "password123"
            };

            var httpContent = login.ToJsonHttpContent();

            //act
            var response = await _client.PostAsync("/api/account/login", httpContent);

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
