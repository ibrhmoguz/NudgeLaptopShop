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
