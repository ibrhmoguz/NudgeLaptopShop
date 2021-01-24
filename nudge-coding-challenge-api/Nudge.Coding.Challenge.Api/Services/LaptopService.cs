using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nudge.LaptopShop.Api.Interfaces;
using Nudge.LaptopShop.Api.Models;

namespace Nudge.LaptopShop.Api.Services
{
    public class LaptopService : ILaptopService
    {
        private readonly Laptop[] _laptopList = new List<Laptop>
        {
            new Laptop {Id = 1,Name = "Dell",Price = (decimal) 349.87},
            new Laptop {Id = 2,Name = "Toshiba",Price = (decimal) 359.87},
            new Laptop {Id = 3,Name = "Hp",Price = (decimal) 456.87}
        }.ToArray();

        private readonly LaptopConfiguration[] _laptopConfigurationList = new List<LaptopConfiguration>
        {
            new LaptopConfiguration {Id = 1, Name = "Ram", Value = "8GB", Price = (decimal) 45.67},
            new LaptopConfiguration {Id = 2, Name = "Ram", Value = "16GB", Price = (decimal) 87.88},
            new LaptopConfiguration {Id = 3, Name = "HDD", Value = "500GB", Price = (decimal) 123.34},
            new LaptopConfiguration {Id = 4, Name = "HDD", Value = "1TB", Price = (decimal) 200.00},
            new LaptopConfiguration {Id = 5, Name = "Color", Value = "Red", Price = (decimal) 56.32},
            new LaptopConfiguration {Id = 6, Name = "Color", Value = "Blue", Price = (decimal) 34.56},
        }.ToArray();

        private readonly BasketViewModel _basket = new BasketViewModel();

        public LaptopService()
        {

        }

        public async Task<Laptop[]> GetLaptopList()
        {
            return await Task.Run(() => _laptopList);
        }

        public async Task<LaptopConfiguration[]> GetConfigurationList()
        {
            return await Task.Run(() => _laptopConfigurationList);
        }

        public async Task<BasketViewModel> AddToBasket(BasketItem laptop)
        {
            // Fetch basket 

            // Add basket item into basket

            // Save basket down to db

            // return basket
            _basket.BasketItems.Add(new BasketItems
            {
                Laptop = _laptopList.ToList().SingleOrDefault(l => l.Id == laptop.LaptopId),
                LaptopConfigurations = _laptopConfigurationList.ToList()
                    .Where(lc => laptop.LaptopConfigurationIdList.Contains(lc.Id)).ToList()
            });
            return await Task.Run(() => _basket);
        }
    }
}
