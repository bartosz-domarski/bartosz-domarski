using FiberFresh.Domain.Entities;

namespace FiberFresh.Domain.Interfaces
{
    public interface IFiberFreshRepository
    {
        Task<List<Service>> Create(Booking booking);
        Task<List<DateOfService>> GetDates();
        Task AddDate(DateOfService dateOfService);
        Task AddDates(List<DateOfService> dateOfServicePool);
        Task DeleteDate(DateOfService dateOfService);
    }
}
