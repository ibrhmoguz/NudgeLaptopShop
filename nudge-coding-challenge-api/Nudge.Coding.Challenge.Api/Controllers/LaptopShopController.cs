using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nudge.Coding.Challenge.Api.Interfaces;
using Nudge.Coding.Challenge.Api.Models;

namespace Nudge.Coding.Challenge.Api.Controllers
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

        [Route("basket/add")]
        [HttpPost]
        public async Task<ActionResult> AddToBasket([FromBody] BasketItem laptop)
        {
            var basket = await _laptopService.AddToBasket(laptop);
            return Ok(basket);
        }

        [ResponseCache(Duration = int.MaxValue)]
        [Route("configuration/list")]
        [HttpGet]
        public async Task<ActionResult> ConfigurationList()
        {
            var list = await _laptopService.GetConfigurationList();
            return Ok(list);
        }
    }
}
