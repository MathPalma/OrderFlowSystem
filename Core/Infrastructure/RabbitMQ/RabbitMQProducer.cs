using Core.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Core.Infrastructure.RabbitMQ
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        private readonly IConfiguration _configuration;
        public RabbitMQProducer(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void PublishMessage<T>(T message, string queueName)
        {
            var factory = new ConnectionFactory() { Uri = new Uri(_configuration["RabbitMQ:ConnectionString"]) };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
