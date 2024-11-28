using API.Application.Interfaces;
using API.Application.Services;
using API.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace API.AppConfig
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

            return services;
        }
    }
}
