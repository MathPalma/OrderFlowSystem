using API.Application.Interfaces;
using API.Domain.Entities;
using API.Domain.Models;
using API.ViewModels;

namespace API.Application.Services
{
    public class OrderService : IOrderService
    {
        public OrderService() { }

        public async Task<Result> CreateOrder(OrderViewModel orderViewModel)
        {
            //validate if the customer exists

            Order order = new(orderViewModel.Id, orderViewModel.CustomerId, orderViewModel.CustomerName);

            orderViewModel.Itens.ForEach(i =>
            {
                Item item = new(i.Name, i.UnitPrice, i.Amount);
                order.Items.Add(item);
            });

            return null;

        }
    }
}
