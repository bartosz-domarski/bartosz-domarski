using CarWorkshop.Domain.Entities;
using CarWorkshop.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Infrastructure.Seeders
{
    public class CarWorkshopSeeder
    {
        private readonly CarWorkshopDbContext _dbContext;

        public CarWorkshopSeeder(CarWorkshopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                if (!_dbContext.CarWorkshops.Any())
                {
                    var carWorkshop = new Domain.Entities.CarWorkshop()
                    {
                        Name = "Test",
                        Description = "Test",
                        ContactDetails = new()
                        {
                            City = "Test",
                            Street = "Test",
                            PostalCode = "Test",
                            PhoneNumber = "123123123"
                        }
                    };
                    carWorkshop.EncodeName();
                    _dbContext.CarWorkshops.Add(carWorkshop);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
