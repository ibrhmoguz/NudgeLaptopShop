using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Nudge.LaptopShop.Api.Models
{
    public class Basket
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public int LaptopId { get; set; }
        public int LaptopConfigurationId { get; set; }
    }

    public class BasketViewModel
    {
        public List<BasketItems> BasketItems { get; set; }
        public decimal TotalPrice
        {
            get
            {
                return BasketItems.Sum(items => items.TotalPrice);
            }
        }
        public BasketViewModel()
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
