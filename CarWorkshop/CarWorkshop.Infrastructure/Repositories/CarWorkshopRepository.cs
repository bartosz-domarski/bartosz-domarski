using CarWorkshop.Domain.Interfaces;
using CarWorkshop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Infrastructure.Repositories
{
    public class CarWorkshopRepository : ICarWorkshopRepository
    {
        private readonly CarWorkshopDbContext _dbContext;

        public CarWorkshopRepository(CarWorkshopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Domain.Entities.CarWorkshop carWorkshop)
        {
            _dbContext.CarWorkshops.Add(carWorkshop);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Domain.Entities.CarWorkshop?> GetByName(string name)
            => await _dbContext.CarWorkshops.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());

        public async Task<IEnumerable<Domain.Entities.CarWorkshop>> GetAll()
            => await _dbContext.CarWorkshops.ToListAsync();

        public async Task<Domain.Entities.CarWorkshop> GetByEncodedName(string encodedName)
            => await _dbContext.CarWorkshops.FirstAsync(x => x.EncodedName == encodedName);

        public async Task Commit()
            => await _dbContext.SaveChangesAsync();
    }
}
