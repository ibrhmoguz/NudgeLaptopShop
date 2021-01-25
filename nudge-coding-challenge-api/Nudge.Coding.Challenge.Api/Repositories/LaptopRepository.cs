using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nudge.LaptopShop.Api.Common;
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
            try
            {
                laptop.LaptopConfigurationIdList.ForEach(lc =>
                    _laptopShopContext.Basket.Add(new Basket
                    {
                        LaptopId = laptop.LaptopId,
                        LaptopConfigurationId = lc
                    }));

                await _laptopShopContext.SaveChangesAsync();

                return await _laptopShopContext.Basket.ToArrayAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException exception  && exception.Number == 2627)
                {
                    //Violation of primary key. Handle Exception
                    throw new PrimaryKeyViolationException("Laptop and configuration already added!");
                }

                throw;
            }
        }
    }
}
