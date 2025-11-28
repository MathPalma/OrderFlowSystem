using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Worker.Config;

namespace Worker.Infrastructure.RabbitMQ
{
    public class RabbitMQConnection
    {
        private readonly RabbitMQConfig _config;
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        public RabbitMQConnection(IOptions<RabbitMQConfig> config)
        {
            _config = config.Value;
            _connectionFactory = new ConnectionFactory { Uri = new Uri(_config.ConnectionString) };
        }

        public IConnection CreateConnection()
        {
            if (_connection == null || !_connection.IsOpen)
            {
                _connection = _connectionFactory.CreateConnection();
            }
            return _connection;
        }
    }
}
