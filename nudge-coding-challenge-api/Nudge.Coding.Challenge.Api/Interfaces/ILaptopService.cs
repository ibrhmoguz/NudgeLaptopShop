using System.Threading.Tasks;
using Nudge.Coding.Challenge.Api.Models;

namespace Nudge.Coding.Challenge.Api.Interfaces
{
    public interface ILaptopService
    {
        Task<Laptop[]> GetLaptopList();

        Task<LaptopConfiguration[]> GetConfigurationList();

        Task<Basket> AddToBasket(BasketItem laptop);
    }
}
