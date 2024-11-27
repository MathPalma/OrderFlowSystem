namespace API.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public List<Item> Itens { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
    }
}
