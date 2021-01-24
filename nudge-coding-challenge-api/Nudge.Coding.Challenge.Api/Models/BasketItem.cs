using System.Collections.Generic;

namespace Nudge.LaptopShop.Api.Models
{
    public class BasketItem
    {
        public int LaptopId { get; set; }
        public List<int> LaptopConfigurationIdList { get; set; }

        public BasketItem()
        {
            LaptopConfigurationIdList = new List<int>();
        }
    }
}
