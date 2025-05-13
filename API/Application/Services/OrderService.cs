using API.Application.Interfaces;
using API.Domain.Entities;
using API.Domain.Models;
using API.Domain.Repositories;
using API.ViewModels;
using System.Net;

namespace API.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository) 
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result> GetOrders(OrderFilterViewModel filters)
        {
            try
            {
                List<Order> orders = await _orderRepository.GetOrdersByFilters(filters);

                var response = orders.Select(o => new
                {
                    o.Id,
                    o.CustomerName,
                    o.Status,
                    o.CreatedAt,
                    o.Total,
                    Items = o.Items.Select(i => new
                    {
                        i.Name,
                        i.UnitPrice,
                        i.Amount
                    })
                });

                return Result.Success(HttpStatusCode.OK, response);
            }
            catch (Exception)
            {
                return Result.Failure(HttpStatusCode.InternalServerError, "An unexpected error occurred getting the orders. Please, contact the support.");
            }
        }

        public async Task<Result> CreateOrder(OrderViewModel orderViewModel)
        {
            try
            {
                //validate if the customer exists

                Order order = new(0, orderViewModel.CustomerId, orderViewModel.CustomerName, orderViewModel.Total);

                orderViewModel.Itens.ForEach(i =>
                {
                    Item item = new(i.Name, i.UnitPrice, i.Amount);
                    order.AddItem(item);
                });

                await _orderRepository.CreateOrder(order);

                return Result.Success(HttpStatusCode.Created);
            }
            catch (InvalidOperationException ex)
            {
                return Result.Failure(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception)
            {
                return Result.Failure(HttpStatusCode.InternalServerError, "An unexpected error occurred creating the order. Please, contact the support.");
            }
        }

        public async Task<Result> CancelOrder(int orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);

            if (order == null)
                return Result.Failure(HttpStatusCode.NotFound, "Order not found.");

            try
            {
                order.Cancel();
                await _orderRepository.UpdateOrder(order);
                return Result.Success(HttpStatusCode.NoContent);
            }
            catch (InvalidOperationException ex)
            {
                return Result.Failure(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception)
            {
                return Result.Failure(HttpStatusCode.InternalServerError,"An unexpected error occurred cancelling the order. Contact the support!");
            }
        }
    }
}
