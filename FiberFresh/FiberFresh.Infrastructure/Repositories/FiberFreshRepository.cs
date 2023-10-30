using FiberFresh.Domain.Entities;
using FiberFresh.Domain.Interfaces;
using FiberFresh.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FiberFresh.Infrastructure.Repositories
{
    public class FiberFreshRepository : IFiberFreshRepository
    {
        private readonly FiberFreshDbContext _dbContext;

        public FiberFreshRepository(FiberFreshDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Service>> Create(Booking booking)
        {
            var client = _dbContext.Clients.FirstOrDefault(x =>
                x.FirstName == booking.Client.FirstName &&
                x.LastName == booking.Client.LastName &&
                x.Email == booking.Client.Email &&
                x.PhoneNumber == booking.Client.PhoneNumber &&
                x.City == booking.Client.City &&
                x.Street == booking.Client.Street &&
                x.Floor == booking.Client.Floor &&
                x.IsElevator == booking.Client.IsElevator
            );

            if (client != null)
            {
                booking.Client = client;
            }

            _dbContext.Bookings.Add(booking);

            var serviceDate = _dbContext.DateOfServices.FirstOrDefault(x => x.Date == booking.DateOfService && x.TimeOfDay == booking.TimeOfDay);
            _dbContext.DateOfServices.Remove(serviceDate!);

            await _dbContext.SaveChangesAsync();

            return booking.Services;
        }

        public async Task<List<DateOfService>> GetDates() =>
            await _dbContext.DateOfServices.ToListAsync();

        public async Task AddDate(DateOfService dateOfService)
        {
            if (!_dbContext.DateOfServices.Any(x => x.Date == dateOfService.Date && x.TimeOfDay == dateOfService.TimeOfDay))
            {
                _dbContext.DateOfServices.Add(dateOfService);
                await _dbContext.SaveChangesAsync();
            }
        }

        public Task AddDates(List<DateOfService> dateOfServicePool)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteDate(DateOfService dateOfService)
        {
            var date = _dbContext.DateOfServices.First(x => x.Date == dateOfService.Date && x.TimeOfDay == dateOfService.TimeOfDay);

            _dbContext.DateOfServices.Remove(date);
            await _dbContext.SaveChangesAsync();
        }
    }
}
