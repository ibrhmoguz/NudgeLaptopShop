using System.Collections.Generic;
using System.Linq;
using Nudge.LaptopShop.Api.Models;

namespace Nudge.LaptopShop.Api.Data
{
    public class DbInitializer
    {
        public static void Initialize(LaptopShopContext context)
        {
            context.Database.EnsureCreated();

            // Look for any laptops.
            if (context.Laptops.Any())
            {
                return;   // DB has been seeded
            }

            var laptops = new List<Laptop>
            {
                new Laptop {Name = "Dell", Price = (decimal) 349.87},
                new Laptop {Name = "Toshiba", Price = (decimal) 359.87},
                new Laptop {Name = "Hp", Price = (decimal) 456.87},
                new Laptop {Name = "MacBook Pro", Price = (decimal) 867.56}
            };
            laptops.ForEach(laptop => context.Laptops.Add(laptop));
            context.SaveChanges();

            var laptopConfigurations = new List<LaptopConfiguration>
            {
                new LaptopConfiguration {Name = "Ram", Value = "8GB", Price = (decimal) 45.67},
                new LaptopConfiguration {Name = "Ram", Value = "16GB", Price = (decimal) 87.88},
                new LaptopConfiguration {Name = "HDD", Value = "500GB", Price = (decimal) 123.34},
                new LaptopConfiguration {Name = "HDD", Value = "1TB", Price = (decimal) 200.00},
                new LaptopConfiguration {Name = "Color", Value = "Red", Price = (decimal) 56.32},
                new LaptopConfiguration {Name = "Color", Value = "Blue", Price = (decimal) 34.56},
            };
            laptopConfigurations.ForEach(lc => context.LaptopConfigurations.Add(lc));
            context.SaveChanges();
        }
    }
}
