namespace Worker.Config
{
    public class RabbitMQConfig
    {
        public string HostName { get; set; } = "localhost";
        public string QueueName { get; set; } = "orderQueue";
    }
}
