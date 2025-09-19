using Core.Domain.Entities;
using Core.Domain.Models;

namespace Core.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderById(int id);
        Task<List<Order>> GetOrdersByFilters(OrderFilter filters);
        Task CreateOrder(Order order);
        Task UpdateOrder(Order order);
    }
}
