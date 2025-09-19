using API.Application.Mappers;
using API.ViewModels;
using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService) 
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] OrderFilterViewModel filter)
        {
            OrderFilter filterDomainModel = filter.ToDomainModel();
            var result = await _orderService.GetOrders(filterDomainModel);

            if (!result.IsSuccess)
                return StatusCode(500, result.Message);

            return Ok(result.Data);
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderViewModel orderViewModel)
        {
            Order order = orderViewModel.ToDomainModel();
            Result result = await _orderService.CreateOrder(order);

            if (!result.IsSuccess)
                return result.StatusCode switch
                {
                    HttpStatusCode.BadRequest => BadRequest(result.Message),
                    _ => StatusCode(500, result.Message)
                };

            return Created("", result.Message);
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var result = await _orderService.CancelOrder(id);

            if (result.IsSuccess)
                return NoContent();

            return result.StatusCode switch
            {
                HttpStatusCode.NotFound => NotFound(result.Message),
                HttpStatusCode.BadRequest => BadRequest(result.Message),
                _ => StatusCode(500, result.Message)
            };
        }
    }
}
