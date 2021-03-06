using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.DTO.BasketDTO.Basket;
using WA.Pizza.Infrastructure.DTO.BasketDTO.BasketItem;

namespace WA.Pizza.Api.Controllers
{
    [Authorize]
    public class BasketController: BaseApiController
    {
        private readonly IBasketDataService _service;

        public BasketController(IBasketDataService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetBaskets()
        {
            BasketDto[] result = await _service.GetAllBasketsAsync();

            if (!result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBasketItem([FromBody] CreateBasketItemRequest basketItemRequest)
        {
            int result = await _service.CreateBasketItemAsync(basketItemRequest);

            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBasket([FromBody] UpdateBasketItemRequest modifyDto)
        {
            int result = await _service.UpdateBasketItemAsync(modifyDto);

            return Ok(result);
        }
    }
}
