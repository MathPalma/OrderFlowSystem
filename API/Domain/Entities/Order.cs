namespace API.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public List<Item> Items { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public Order(int id, int customerId, string customerName)
        {
            if (id <= 0)
                throw new InvalidOperationException("Order Id cannot be lower than 1");
            if (customerId <= 0)
                throw new InvalidOperationException("Customer Id cannot be lower than 1");
            if (string.IsNullOrEmpty(customerName))
                throw new InvalidOperationException("Customer name must be filled");

            Id = id;
            CustomerId = customerId;
            CustomerName = customerName;
            Items = new List<Item>();
            Status = "Teste";
            CreatedAt = DateTime.Now;
        }

        public void AddItem(Item item)
        {
            if (item == null)
                throw new InvalidOperationException("Item cannot be null");

            if (item.Amount <= 0)
                throw new InvalidOperationException("The amount of itens must be greather than zero");

            if (item.UnitPrice <= 0)
                throw new InvalidOperationException("The item price must be greather than zero");

            Items.Add(item);
        }
    }
}
