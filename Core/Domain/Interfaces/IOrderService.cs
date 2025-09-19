using Core.Domain.Entities;
using Core.Domain.Models;

namespace Core.Domain.Interfaces
{
    public interface IOrderService
    {
        Task<Result> GetOrders(OrderFilter filters);
        Task<Result> CreateOrder(Order orderViewModel);
        Task<Result> CancelOrder(int orderId);
    }
}
