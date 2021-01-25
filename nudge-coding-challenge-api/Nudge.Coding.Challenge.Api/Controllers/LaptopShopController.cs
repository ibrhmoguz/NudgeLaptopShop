using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nudge.LaptopShop.Api.Interfaces;
using Nudge.LaptopShop.Api.Models;

namespace Nudge.LaptopShop.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LaptopShopController : ControllerBase
    {
        private ILaptopService _laptopService { get; }

        public LaptopShopController(ILaptopService laptopService)
        {
            _laptopService = laptopService;
        }

        [Route("laptop/list")]
        [HttpGet]
        public async Task<ActionResult> LaptopList()
        {
            var list = await _laptopService.GetLaptopList();
            return Ok(list);
        }

        /*
         * Assume that configuration list not being updated frequently.
         * Added response cache to decrease the database load of configuration list call
         * The cache will be expired in 1 day
         */
        [ResponseCache(Duration = 86400)]
        [Route("configuration/list")]
        [HttpGet]
        public async Task<ActionResult> ConfigurationList()
        {
            var list = await _laptopService.GetConfigurationList();
            return Ok(list);
        }

        [Route("basket/add")]
        [HttpPost]
        public async Task<ActionResult> AddToBasket([FromBody] BasketItem laptop)
        {
            var basket = await _laptopService.AddToBasket(laptop);
            return Ok(basket);
        }
    }
}
