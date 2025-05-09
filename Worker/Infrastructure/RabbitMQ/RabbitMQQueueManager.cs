using RabbitMQ.Client;

namespace Worker.Infrastructure.RabbitMQ
{
    public class RabbitMQQueueManager
    {
        private readonly RabbitMQConnection _rabbitMQConnection;
        public RabbitMQQueueManager(RabbitMQConnection rabbitMQConnection)
        {
            _rabbitMQConnection = rabbitMQConnection;
        }

        public void CreateQueueIfNotExists(string queueName)
        {
            using var connection = _rabbitMQConnection.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
            );
        }
    }
}
