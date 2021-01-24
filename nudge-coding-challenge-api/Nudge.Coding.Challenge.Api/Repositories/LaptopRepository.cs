using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nudge.LaptopShop.Api.Data;
using Nudge.LaptopShop.Api.Interfaces;
using Nudge.LaptopShop.Api.Models;

namespace Nudge.LaptopShop.Api.Repositories
{
    public class LaptopRepository : ILaptopRepository
    {
        private readonly LaptopShopContext _laptopShopContext;

        public LaptopRepository(LaptopShopContext laptopShopContext)
        {
            _laptopShopContext = laptopShopContext;
        }

        public async Task<Laptop[]> GetLaptopList()
        {
            return await _laptopShopContext.Laptops.ToArrayAsync();
        }

        public async Task<LaptopConfiguration[]> GetConfigurationList()
        {
            return await _laptopShopContext.LaptopConfigurations.ToArrayAsync();
        }

        public async Task<Basket[]> AddToBasket(BasketItem laptop)
        {
            var basketItems = await _laptopShopContext.BasketItems.ToListAsync();

            laptop.LaptopConfigurationIdList.ForEach(lc => basketItems.Add(new Basket
            {
                LaptopId = laptop.LaptopId,
                LaptopConfigurationId = lc
            }));

            await _laptopShopContext.SaveChangesAsync();

            return await _laptopShopContext.BasketItems.ToArrayAsync();
        }
    }
}
