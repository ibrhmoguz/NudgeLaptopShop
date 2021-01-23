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
        private ILaptopService LaptopService { get; }

        public LaptopShopController(ILaptopService laptopService)
        {
            LaptopService = laptopService;
        }

        [Route("laptop/list")]
        [HttpGet]
        public async Task<ActionResult> LaptopList()
        {
            var list = await this.LaptopService.GetLaptopList();
            return Ok(list);
        }

        [Route("basket/add")]
        [HttpPost]
        public async Task<ActionResult> AddToBasket([FromBody] BasketItem laptop)
        {
            var basket = await this.LaptopService.AddToBasket(laptop);
            return Ok(basket);
        }

        [ResponseCache(Duration = int.MaxValue)]
        [Route("configuration/list")]
        [HttpGet]
        public async Task<ActionResult> ConfigurationList()
        {
            var list = await this.LaptopService.GetConfigurationList();
            return Ok(list);
        }
    }
}
