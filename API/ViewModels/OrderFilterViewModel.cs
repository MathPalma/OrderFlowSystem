using API.Domain.Enums;

namespace API.ViewModels
{
    public class OrderFilterViewModel
    {
        public int? CustomerId { get; set; }
        public OrderStatus? Status { get; set; }
        public DateTime? StartDate { get; set; } // Min Filter Date
        public DateTime? EndDate { get; set; }   // Max Filter Date
    }
}
