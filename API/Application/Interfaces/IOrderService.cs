using API.Domain.Models;
using API.ViewModels;

namespace API.Application.Interfaces
{
    public interface IOrderService
    {
        Task<Result> CreateOrder(OrderViewModel orderViewModel);
    }
}
