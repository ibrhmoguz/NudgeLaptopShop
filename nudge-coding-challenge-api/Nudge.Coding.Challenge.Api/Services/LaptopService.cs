using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nudge.LaptopShop.Api.Interfaces;
using Nudge.LaptopShop.Api.Models;

namespace Nudge.LaptopShop.Api.Services
{
    public class LaptopService : ILaptopService
    {
        private readonly ILaptopRepository _laptopRepository;
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

        public LaptopService(ILaptopRepository laptopRepository)
        {
            _laptopRepository = laptopRepository;
        }

        public async Task<Laptop[]> GetLaptopList()
        {
            return await _laptopRepository.GetLaptopList();
        }

        public async Task<LaptopConfiguration[]> GetConfigurationList()
        {
            return await _laptopRepository.GetConfigurationList();
        }

        public async Task<BasketViewModel> AddToBasket(BasketItem laptop)
        {
            // Add new laptop and its configuration into basket.
            var basketItems = await _laptopRepository.AddToBasket(laptop);

            // laptops in basket
            var laptops = basketItems.Select(basket => basket.LaptopId).Distinct().ToList();

            // create basket view model
            var laptopList = await _laptopRepository.GetLaptopList();
            var laptopConfigurationList = await _laptopRepository.GetConfigurationList();
            var basketViewModel = new BasketViewModel();
            laptops.ForEach(laptopId =>
            {
                var laptopConfigurations = basketItems.Where(b => b.LaptopId == laptopId)
                    .Select(basket => basket.LaptopConfigurationId).ToList();
                basketViewModel.BasketItems.Add(new BasketItems
                {
                    Laptop = laptopList.FirstOrDefault(l => l.Id == laptopId),
                    LaptopConfigurations = laptopConfigurationList.Where(lc => laptopConfigurations.Contains(lc.Id))
                        .ToList()
                });
            });

            return basketViewModel;
        }
    }
}
