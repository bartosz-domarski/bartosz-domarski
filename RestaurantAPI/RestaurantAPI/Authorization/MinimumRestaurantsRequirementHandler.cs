using Microsoft.AspNetCore.Authorization;
using RestaurantAPI.Entities;
using System.Security.Claims;

namespace RestaurantAPI.Authorization
{
    public class MinimumRestaurantsRequirementHandler : AuthorizationHandler<MinimumRestaurantsRequirement>
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly ILogger<MinimumRestaurantsRequirementHandler> _logger;

        public MinimumRestaurantsRequirementHandler(RestaurantDbContext dbContext, ILogger<MinimumRestaurantsRequirementHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumRestaurantsRequirement requirement)
        {
            var userId = int.Parse(context.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier).Value);
            var amountOfCreatedRestaurants = _dbContext.Restaurants.Count(r => r.CreatedById.Value == userId);

            _logger.LogInformation($"User: {userId}");

            if (amountOfCreatedRestaurants >= requirement.MinimumRestaurants)
            {
                _logger.LogInformation("Authorization succedded");
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogInformation("Authorization failed");
            }

            return Task.CompletedTask;
        }
    }
}
