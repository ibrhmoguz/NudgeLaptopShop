using System.Collections.Generic;
using System.Linq;

namespace Nudge.Coding.Challenge.Api.Models
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
        public Laptop Laptop { get; set; }
        public List<LaptopConfiguration> LaptopConfigurations { get; set; }
        public decimal TotalPrice
        {
            get
            {
                return decimal.Add(Laptop?.Price ?? 0, LaptopConfigurations.Sum(configuration => configuration.Price));
            }
        }

        public BasketItems()
        {
            LaptopConfigurations = new List<LaptopConfiguration>();
        }
    }
}
