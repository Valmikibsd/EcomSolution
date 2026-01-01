using Ecom.Application.DTOs;
using Ecom.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcomApi.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto, string Userid)
        {
            await _orderService.CreateOrderAsync(dto, Userid);
            // Since CreateOrderAsync returns void, we cannot assign its result to a variable.
            // You may want to return a 201 Created response, but you do not have the created order's ID.
            // For now, return a generic Created response.
            return Created(string.Empty, null);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(string id)
        {
            var order = await _orderService.GetMyOrdersAsync(id);
            return order == null ? NotFound() : Ok(order);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
