using Core.Domain.Enums;

namespace Core.Domain.Models
{
    public class OrderFilter
    {
        public int? CustomerId { get; set; }
        public OrderStatus? Status { get; set; }
        public DateTime? StartDate { get; set; } // Min Filter Date
        public DateTime? EndDate { get; set; }   // Max Filter Date
    }
}
