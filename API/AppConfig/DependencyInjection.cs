using API.Application.Services;
using Core.Domain.Interfaces;
using Core.Domain.Repositories;
using Core.Infrastructure.DataAccess.DbContexts;
using Core.Infrastructure.DataAccess.Repositories;
using Core.Infrastructure.RabbitMQ;
using Microsoft.EntityFrameworkCore;

namespace API.AppConfig
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection AddRabbitMQ(this IServiceCollection services)
        {
            services.AddSingleton<IRabbitMQProducer, RabbitMQProducer>();

            return services;
        }
    }
}
