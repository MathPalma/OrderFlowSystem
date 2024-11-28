namespace API.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public List<OrderItemViewModel> Itens { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
    }
}
