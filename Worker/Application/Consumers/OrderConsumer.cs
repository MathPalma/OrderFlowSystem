using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Worker.Infrastructure.RabbitMQ;

namespace Worker.Application.Consumers
{
    public class OrderConsumer
    {
        private readonly RabbitMQConnection _rabbitMQConnection;
        private readonly RabbitMQQueueManager _queueManager;

        public OrderConsumer(RabbitMQConnection rabbitMQConnection, RabbitMQQueueManager queueManager)
        {
            _rabbitMQConnection = rabbitMQConnection;
            _queueManager = queueManager;
        }

        public void Consume(string queueName)
        {
            _queueManager.CreateQueueIfNotExists(queueName);

            var connection = _rabbitMQConnection.CreateConnection();
            var channel = connection.CreateModel();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"mensagem recebida: {message}");

                channel.BasicAck(ea.DeliveryTag, false);
            };

            channel.BasicConsume(
            queue: queueName,
            consumerTag: "",     
            noLocal: false,      
            autoAck: false,       
            arguments: null,      
            consumer: consumer
            );
        }
    }
}
