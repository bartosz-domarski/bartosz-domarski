using RestaurantAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.IntegrationTests
{
    public class ProgramTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly List<Type> _controllerTypes;
        private readonly WebApplicationFactory<Program> _factory;

        public ProgramTests()
        {
            _controllerTypes = typeof(Program)
                .Assembly
                .GetTypes()
                .Where(x => x.IsSubclassOf(typeof(ControllerBase)))
                .ToList();

            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        _controllerTypes.ForEach(x => services.AddScoped(x));
                    });
                });
        }

        [Fact]
        public void ConfigureServices_ForControllers_RegisterAllDependencies()
        {
            //arrange
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory!.CreateScope();

            //assert
            _controllerTypes.ForEach(x =>
            {
                var controller = scope.ServiceProvider.GetService(x);
                controller.Should().NotBeNull();
            });
        }
    }
}
