using Worker.Application.Consumers;

namespace Worker
{
    public class OrderWorker : BackgroundService
    {
        private readonly ILogger<OrderWorker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly OrderConsumer _consumer;

        public OrderWorker(ILogger<OrderWorker> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

           
            using var scope = _scopeFactory.CreateScope();
            var consumer = scope.ServiceProvider.GetRequiredService<OrderConsumer>();

            consumer.Consume("order-processing-queue");

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}