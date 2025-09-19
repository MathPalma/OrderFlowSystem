using API.ViewModels;
using Core.Domain.Entities;
using Core.Domain.Models;

namespace API.Application.Mappers
{
    public static class OrderMapper
    {
        public static Order ToDomainModel(this OrderViewModel orderViewModel)
        {
            Order order = new Order(orderViewModel.Id, orderViewModel.CustomerId, orderViewModel.CustomerName, orderViewModel.Total);
            foreach (var item in orderViewModel.Items)
            {
                order.AddItem(item.ToDomainModel());
            }

            return order;
        }

        public static OrderFilter ToDomainModel(this OrderFilterViewModel filterViewModel) => new OrderFilter
        {
            CustomerId = filterViewModel.CustomerId,
            Status = filterViewModel.Status,
            StartDate = filterViewModel.StartDate,
            EndDate = filterViewModel.EndDate
        };

        public static Item ToDomainModel(this OrderItemViewModel itemViewModel) => new Item(itemViewModel.Name, itemViewModel.UnitPrice, itemViewModel.Amount);

    }
}
