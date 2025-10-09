using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.Domain.Repositories;
using Core.Infrastructure.DataAccess.Repositories;
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
        private readonly IServiceScopeFactory _scopeFactory;

        public OrderConsumer(RabbitMQConnection rabbitMQConnection, RabbitMQQueueManager queueManager,
            IServiceScopeFactory scopeFactory)
        {
            _rabbitMQConnection = rabbitMQConnection;
            _queueManager = queueManager;
            _scopeFactory = scopeFactory;
        }

        public void Consume(string queueName)
        {
            _queueManager.CreateQueueIfNotExists(queueName);

            var connection = _rabbitMQConnection.CreateConnection();
            var channel = connection.CreateModel();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    var order = JsonSerializer.Deserialize<Order>(message);

                    if (order == null || order.Id <= 0)
                    {
                        channel.BasicNack(ea.DeliveryTag, multiple: false, requeue: false);
                        Console.WriteLine($"[OrderConsumer] Error deserializing the message. Message rejected.");
                        return;
                    }

                    Console.WriteLine($"[OrderConsumer] Processing order {order.Id}...");

                    await orderRepository.UpdateOrderStatus(order.Id, OrderStatus.PreparingShipment);
                    Console.WriteLine($"[OrderConsumer] Order {order.Id} processed and status updated to 'PreparingShipment'.");

                    channel.BasicAck(ea.DeliveryTag, false);
                }
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
