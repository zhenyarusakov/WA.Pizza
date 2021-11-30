using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WA.Pizza.Core.Entities.OrderDomain;
using WA.Pizza.Infrastructure.Abstractions;

namespace WA.Pizza.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderDataService _service;
        public OrderController(IOrderDataService service)
        {
            _service = service;
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetOrder(int id)
        //{
        //    var result = await _service.GetOrderAsync(id);

        //    return Ok(result);
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetOrders()
        //{
        //    var result = await _service.GetOrdersAsync();

        //    if (!result.Any())
        //    {
        //        return NoContent();
        //    }

        //    return Ok(result);
        //}

        [HttpPost]
        public async Task<IActionResult> CreateOrder(int basketId, int userId)
        {
            var result = await _service.CreateOrderAsync(basketId, userId);

            return Ok(result);
        }

        [HttpPost("OrderStatusId/{id}")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromQuery] OrderStatus status)
        {
            await _service.UpdateOrderStatus(id, status);

            return Ok(status);
        }

    }
}
