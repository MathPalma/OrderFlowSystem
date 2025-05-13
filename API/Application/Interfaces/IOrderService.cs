using API.Domain.Models;
using API.ViewModels;

namespace API.Application.Interfaces
{
    public interface IOrderService
    {
        Task<Result> GetOrders(OrderFilterViewModel filters);
        Task<Result> CreateOrder(OrderViewModel orderViewModel);
        Task<Result> CancelOrder(int orderId);
    }
}
