using FiberFresh.Domain.Interfaces;
using FiberFresh.Infrastructure.Persistence;
using FiberFresh.Infrastructure.Repositories;
using FiberFresh.Infrastructure.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FiberFresh.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<FiberFreshDbContext>(options => options.UseNpgsql(
                Environment.GetEnvironmentVariable("DB_CONN_STRING")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<FiberFreshDbContext>();

            services.AddScoped<FiberFreshSeeder>();

            services.AddScoped<IFiberFreshRepository, FiberFreshRepository>();
        }
    }
}
