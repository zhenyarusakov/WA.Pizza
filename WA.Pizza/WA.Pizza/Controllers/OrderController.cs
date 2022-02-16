using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WA.Pizza.Core.Entities.OrderDomain;
using WA.Pizza.Infrastructure.Abstractions;

namespace WA.Pizza.Api.Controllers
{
    [Authorize]
    public class OrderController : BaseApiController
    {
        private readonly IOrderDataService _service;
        public OrderController(IOrderDataService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(int basketId, string userId)
        {
            int result = await _service.CreateOrderAsync(basketId, userId);

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
