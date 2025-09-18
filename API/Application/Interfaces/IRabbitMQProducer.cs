namespace API.Application.Interfaces
{
    public interface IRabbitMQProducer
    {
        void PublishMessage<T>(T message, string queueName);
    }
}
