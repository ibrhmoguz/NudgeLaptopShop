using System.Collections.Generic;
using System.Linq;

namespace Nudge.LaptopShop.Api.Models
{
    public class Basket
    {
        public List<BasketItems> BasketItems { get; set; }
        public decimal TotalPrice
        {
            get
            {
                return BasketItems.Sum(items => items.TotalPrice);
            }
        }
        public Basket()
        {
            BasketItems = new List<BasketItems>();
        }
    }

    public class BasketItems
    {
        public int LaptopId { get; set; }
        public List<int> LaptopConfigurationIdList { get; set; }
        public decimal TotalPrice
        {
            get
            {
                return decimal.Add(Laptop?.Price ?? 0, LaptopConfigurations.Sum(configuration => configuration.Price));
            }
        }

        // Navigation Properties
        public Laptop Laptop { get; set; }
        public ICollection<LaptopConfiguration> LaptopConfigurations { get; set; }

        public BasketItems()
        {
            LaptopConfigurations = new List<LaptopConfiguration>();
        }
    }
}
