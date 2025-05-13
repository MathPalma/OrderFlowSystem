using API.Domain.Entities;
using API.ViewModels;

namespace API.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderById(int id);
        Task<List<Order>> GetOrdersByFilters(OrderFilterViewModel filters);
        Task CreateOrder(Order order);
        Task UpdateOrder(Order order);
    }
}
