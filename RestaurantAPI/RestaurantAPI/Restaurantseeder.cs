using RestaurantAPI.Entities;

namespace RestaurantAPI
{
    public class Restaurantseeder
    {
        public readonly RestaurantDbContext _dbContext;

        public Restaurantseeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "Some description",
                    ContactEmail = "contact@kfc.com",
                    ContactNumber = "123456789",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Chicken Strips",
                            Price = 15.6M,
                            Description = "Some description"
                        },
                        new Dish()
                        {
                            Name = "Chicken Winds",
                            Price = 12.8M,
                            Description = "Some description"
                        }
                    },
                    Address = new Address()
                    {
                        City = "Warszawa",
                        Street = "Wolności",
                        PostalCode = "01-123"
                    }
                },
                new Restaurant()
                {
                    Name = "McDonalds",
                    Category = "Fast Food",
                    Description = "Some description",
                    ContactEmail = "contact@mcdonalds.com",
                    ContactNumber = "987654321",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Chicken Drums",
                            Price = 11.2M,
                            Description = "Some description"
                        },
                        new Dish()
                        {
                            Name = "Wrap",
                            Price = 9.9M,
                            Description = "Some description"
                        }
                    },
                    Address = new Address()
                    {
                        City = "Warszawa",
                        Street = "Morska",
                        PostalCode = "01-124"
                    }
                }
            };
            return restaurants;
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };
            return roles;
        }
    }
}
