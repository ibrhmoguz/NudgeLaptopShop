using System.Threading.Tasks;
using Nudge.LaptopShop.Api.Models;

namespace Nudge.LaptopShop.Api.Interfaces
{
    public interface ILaptopRepository
    {
        Task<Laptop[]> GetLaptopList();

        Task<LaptopConfiguration[]> GetConfigurationList();

        Task<Basket[]> AddToBasket(BasketItem laptop);
    }
}
