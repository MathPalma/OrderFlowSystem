using Core.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public List<Item> Items { get; set; }
        public decimal Total { get; set; }
        [Column(TypeName = "tinyint")]
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public Order(int id, int customerId, string customerName, decimal total)
        {
            if (customerId <= 0)
                throw new InvalidOperationException("Customer Id cannot be lower than 1");
            if (string.IsNullOrEmpty(customerName))
                throw new InvalidOperationException("Customer name must be filled");

            Id = id;
            CustomerId = customerId;
            CustomerName = customerName;
            Total = total;
            Items = new List<Item>();
            Status = OrderStatus.New;
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

            Total += item.Amount * item.UnitPrice;
            Items.Add(item);
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Shipped || Status == OrderStatus.Delivered || Status == OrderStatus.Canceled)
                throw new InvalidOperationException("Order cannot be canceled at this stage.");

            Status = OrderStatus.Canceled;
        }
    }
}
