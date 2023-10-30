using FiberFresh.Application.Dtos;
using FiberFresh.Domain.Entities;
using System.Net;

namespace FiberFresh.Application.Services
{
    public interface IFiberFreshService
    {
        Task<HttpStatusCode> Create(BookingDto booking);
        Task<List<DateOfService>> GetDates();

        Task AddDate(DateOfService dateOfService);
        Task AddDates(List<DateOfService> dateOfServicePool);
        Task DeleteDate(DateOfService dateOfService);
    }
}