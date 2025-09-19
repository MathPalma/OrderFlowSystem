namespace Core.Domain.Interfaces
{
    public interface IRabbitMQProducer
    {
        void PublishMessage<T>(T message, string queueName);
    }
}
