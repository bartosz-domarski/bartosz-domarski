using FiberFresh.Domain.Entities;
using FiberFresh.Infrastructure.Persistence;

namespace FiberFresh.Infrastructure.Seeders
{
    public class FiberFreshSeeder
    {
        private readonly FiberFreshDbContext _dbContext;

        public FiberFreshSeeder(FiberFreshDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                if (!_dbContext.Bookings.Any())
                {
                    var booking = new Booking
                    (
                        new Client
                        {
                            FirstName = "John",
                            LastName = "Smith",
                            Email = "email@email.com",
                            PhoneNumber = "123456789",
                            City = "New York",
                            Street = "Wall Street 44",
                            Floor = 3,
                            IsElevator = true
                        },
                        new List<Service>
                        {
                            new Service
                            (
                                Furniture.Carpet,
                                Fabric.Polyester,
                                6f
                            ),
                            new Service
                            (
                                Furniture.Sofa,
                                Fabric.Cotton,
                                Size.Medium
                            ),
                        },
                        DateOnly.FromDateTime(new DateTime(2024, 2, 1)),
                        TimeOfDay.Morning,
                        "Example note"
                    );

                    _dbContext.Bookings.Add(booking);
                    await _dbContext.SaveChangesAsync();
                }

                if (!_dbContext.DateOfServices.Any())
                {
                    var dateOfServicePool1 = new DateOfService
                    {
                        Date = DateOnly.FromDateTime(DateTime.UtcNow),
                        TimeOfDay = TimeOfDay.Morning
                    };

                    _dbContext.DateOfServices.Add(dateOfServicePool1);

                    var dateOfServicePool2 = new DateOfService
                    {
                        Date = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(1),
                        TimeOfDay = TimeOfDay.Evening
                    };

                    _dbContext.DateOfServices.Add(dateOfServicePool2);

                    var dateOfServicePool3 = new DateOfService
                    {
                        Date = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(2),
                        TimeOfDay = TimeOfDay.Morning
                    };

                    _dbContext.DateOfServices.Add(dateOfServicePool3);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
