using Core.Domain.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
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

                var order = JsonSerializer.Deserialize<Order>(message);

                if (order == null)
                {
                    channel.BasicNack(ea.DeliveryTag, multiple: false, requeue: false);
                    Console.WriteLine($"[OrderWorker] Erro de desserialização. Mensagem rejeitada.");
                    return;
                }


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
