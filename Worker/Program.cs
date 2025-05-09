using Worker;
using Worker.Application.Consumers;
using Worker.Config;
using Worker.Infrastructure.RabbitMQ;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.Configure<RabbitMQConfig>(hostContext.Configuration.GetSection("RabbitMQ"));
        services.AddSingleton<RabbitMQConnection>();
        services.AddSingleton<RabbitMQQueueManager>();
        services.AddSingleton<OrderConsumer>();
        services.AddHostedService<OrderWorker>();
    })
    .Build();

await host.RunAsync();
