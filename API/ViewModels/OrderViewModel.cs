using Core.Domain.Enums;

namespace API.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public List<OrderItemViewModel> Items { get; set; }
        public decimal Total { get; set; }
        public OrderStatus Status { get; set; }
    }
}
