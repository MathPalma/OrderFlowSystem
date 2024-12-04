using API.Application.Interfaces;
using API.Domain.Entities;
using API.Domain.Models;
using API.Domain.Repositories;
using API.ViewModels;

namespace API.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository) 
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result> CreateOrder(OrderViewModel orderViewModel)
        {
            try
            {
                //validate if the customer exists

                Order order = new(orderViewModel.Id, orderViewModel.CustomerId, orderViewModel.CustomerName, orderViewModel.Total);

                orderViewModel.Itens.ForEach(i =>
                {
                    Item item = new(i.Name, i.UnitPrice, i.Amount);
                    order.Items.Add(item);
                });

                await _orderRepository.CreateOrder(order);

                return null;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw;
            }
        }
    }
}
