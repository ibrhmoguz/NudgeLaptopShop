using System.Collections.Generic;
using Nudge.LaptopShop.Api.Models;

namespace Nudge.LaptopShop.Api.Tests
{
    public static class MockDataProvider
    {
        public static Laptop[] GetMockLaptopList()
        {
            return new List<Laptop>
            {
                new Laptop {Id = 1,Name = "Dell",Price = (decimal) 349.87},
                new Laptop {Id = 2,Name = "Toshiba",Price = (decimal) 359.87},
                new Laptop {Id = 3,Name = "Hp",Price = (decimal) 456.87}
            }.ToArray();
        }

        public static LaptopConfiguration[] GetMockLaptopConfigurationList()
        {
            return new List<LaptopConfiguration>
            {
                new LaptopConfiguration {Id = 1, Name = "Ram", Value = "8GB", Price = (decimal) 45.67},
                new LaptopConfiguration {Id = 2, Name = "Ram", Value = "16GB", Price = (decimal) 87.88},
                new LaptopConfiguration {Id = 3, Name = "HDD", Value = "500GB", Price = (decimal) 123.34},
                new LaptopConfiguration {Id = 4, Name = "HDD", Value = "1TB", Price = (decimal) 200.00},
                new LaptopConfiguration {Id = 5, Name = "Color", Value = "Red", Price = (decimal) 56.32},
                new LaptopConfiguration {Id = 6, Name = "Color", Value = "Blue", Price = (decimal) 34.56},
            }.ToArray();
        }

        public static BasketViewModel GetBasket()
        {
            return new BasketViewModel
            {
                BasketItems = new List<BasketItems>
                {
                    new BasketItems
                    {
                        Laptop = new Laptop {Id = 1,Name = "Dell",Price = (decimal) 349.87},
                        LaptopConfigurations = new List<LaptopConfiguration>
                        {
                            new LaptopConfiguration {Id = 1, Name = "Ram", Value = "8GB", Price = (decimal) 45.67},
                            new LaptopConfiguration {Id = 4, Name = "HDD", Value = "1TB", Price = (decimal) 200.00},
                            new LaptopConfiguration {Id = 5, Name = "Color", Value = "Red", Price = (decimal) 56.32},
                        }
                    }
                }
            };
        }

        public static Basket[] GetBasketItems()
        {
            return new List<Basket>
            {
                new Basket
                {
                    LaptopId = 1,
                    LaptopConfigurationId = 1
                },
                new Basket
                {
                    LaptopId = 1,
                    LaptopConfigurationId = 4
                },
                new Basket
                {
                    LaptopId = 1,
                    LaptopConfigurationId = 5
                }
            }.ToArray();
        }
    }
}
