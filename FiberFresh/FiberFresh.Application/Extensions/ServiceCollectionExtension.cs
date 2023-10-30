using FiberFresh.Application.Mappings;
using FiberFresh.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FiberFresh.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IFiberFreshService, FiberFreshService>();

            services.AddAutoMapper(typeof(BookingMappingProfile));
        }
    }
}
