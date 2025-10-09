using Core.Domain.Repositories;
using Core.Infrastructure.DataAccess.DbContexts;
using Core.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Worker;
using Worker.Application.Consumers;
using Worker.Config;
using Worker.Infrastructure.RabbitMQ;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.Configure<RabbitMQConfig>(hostContext.Configuration.GetSection("RabbitMQ"));

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(hostContext.Configuration.GetConnectionString("OrderProcessingDb")));
        services.AddScoped<IOrderRepository, OrderRepository>();

        services.AddSingleton<RabbitMQConnection>();
        services.AddSingleton<RabbitMQQueueManager>();
        services.AddSingleton<OrderConsumer>();
        services.AddHostedService<OrderWorker>();
    })
    .Build();

await host.RunAsync();
