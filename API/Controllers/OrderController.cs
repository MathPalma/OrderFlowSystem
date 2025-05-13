using API.Application.Interfaces;
using API.Domain.Models;
using API.ViewModels;
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
            var result = await _orderService.GetOrders(filter);

            if (!result.IsSuccess)
                return StatusCode(500, result.Message);

            return Ok(result.Data);
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderViewModel orderViewModel)
        {
            Result result = await _orderService.CreateOrder(orderViewModel);

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
