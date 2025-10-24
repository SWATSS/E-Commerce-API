using Microsoft.AspNetCore.Mvc;
using Service.Abstraction;
using Shared.DataTransferObjects.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController(IServiceManager _serviceManager) : ControllerBase
    {
        // Get Basket
        [HttpGet] // Get: baseUrl/api/Basket
        public async Task<ActionResult<BasketDto>> GetBasket(string key)
        {
            var basket = await _serviceManager.BasketService.GetBasketAsync(key);
            return Ok(basket);
        }
        // Create Or Update Basket
        [HttpPost] // POST: bseUrl/api/Basket
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var Basket = await _serviceManager.BasketService.CreateOrUpdateAsync(basket);
            return Ok(Basket);
        }
        // Delete Basket
        [HttpDelete("{key}")] // Delete: baseUrl/api/Basket
        public async Task<ActionResult<bool>> DeleteBasket([FromRoute] string key)
        {
            var result = await _serviceManager.BasketService.DeleteBasketAsync(key);
            return Ok(result);
        }
    }
}
