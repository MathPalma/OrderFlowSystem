namespace API.Domain.Entities
{
    public class Item
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int OrderId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }

        public Item(string name, decimal unitPrice, int amount)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvalidOperationException("Item name must be filled");

            if (unitPrice <= 0)
                throw new InvalidOperationException("Item price must be greather than zero");

            if (amount <= 0)
                throw new InvalidOperationException("Item price must be greather than zero");

            Name = name;
            UnitPrice = unitPrice;
            Amount = amount;
        }
    }
}
