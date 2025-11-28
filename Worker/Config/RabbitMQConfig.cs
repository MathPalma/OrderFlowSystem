namespace Worker.Config
{
    public class RabbitMQConfig
    {
        public string ConnectionString { get; set; }
        public string QueueName { get; set; } = "orderQueue";
    }
}
