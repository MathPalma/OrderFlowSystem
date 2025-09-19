using API.ViewModels;
using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Core.Domain.Models;
using Core.Domain.Repositories;
using System.Net;

namespace API.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRabbitMQProducer _rabbitMQProducer;
        public OrderService(IOrderRepository orderRepository, IRabbitMQProducer rabbitMQProducer) 
        {
            _orderRepository = orderRepository;
            _rabbitMQProducer = rabbitMQProducer;
        }

        public async Task<Result> GetOrders(OrderFilter filters)
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

        public async Task<Result> CreateOrder(Order order)
        {
            try
            {
                //validate if the customer exists

                await _orderRepository.CreateOrder(order);

                _rabbitMQProducer.PublishMessage(order, "order-processing-queue");

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
