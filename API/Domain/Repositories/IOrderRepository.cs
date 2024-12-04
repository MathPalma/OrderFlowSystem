using API.Domain.Entities;

namespace API.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task CreateOrder(Order order);
    }
}
