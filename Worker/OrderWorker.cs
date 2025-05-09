using Worker.Application.Consumers;

namespace Worker
{
    public class OrderWorker : BackgroundService
    {
        private readonly ILogger<OrderWorker> _logger;
        private readonly OrderConsumer _consumer;

        public OrderWorker(ILogger<OrderWorker> logger, OrderConsumer orderConsumer)
        {
            _logger = logger;
            _consumer = orderConsumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                _consumer.Consume("OrderQueue");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}