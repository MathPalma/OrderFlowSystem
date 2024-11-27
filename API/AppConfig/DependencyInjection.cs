using API.Application.Interfaces;
using API.Application.Services;

namespace API.AppConfig
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
