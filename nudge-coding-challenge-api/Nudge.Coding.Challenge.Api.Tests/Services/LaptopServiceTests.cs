using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Nudge.LaptopShop.Api.Interfaces;
using Nudge.LaptopShop.Api.Models;
using Nudge.LaptopShop.Api.Services;
using Xunit;

namespace Nudge.LaptopShop.Api.Tests.Services
{
    public class LaptopServiceTests
    {

        [Fact]
        public async Task GetLaptopList_ReturnsLaptopList()
        {
            // Arrange
            var mockRepo = new Mock<ILaptopRepository>();
            mockRepo.Setup(s => s.GetLaptopList())
                .ReturnsAsync(MockDataProvider.GetMockLaptopList());
            var service = new LaptopService(mockRepo.Object);

            // Act
            var result = await service.GetLaptopList();

            // Assert
            var returnValue = Assert.IsType<Laptop[]>(result);
            var laptop = returnValue.FirstOrDefault();
            Assert.NotNull(laptop);
            Assert.Equal("Dell", laptop.Name);
        }

        [Fact]
        public async Task GetConfigurationList_ReturnsLaptopConfigurationList()
        {
            var mockRepo = new Mock<ILaptopRepository>();
            mockRepo.Setup(s => s.GetConfigurationList())
                .ReturnsAsync(MockDataProvider.GetMockLaptopConfigurationList());
            var service = new LaptopService(mockRepo.Object);

            // Act
            var result = await service.GetConfigurationList();

            // Assert
            var returnValue = Assert.IsType<LaptopConfiguration[]>(result);
            var configuration = returnValue.FirstOrDefault();
            Assert.NotNull(configuration);
            Assert.Equal("Ram", configuration.Name);
            Assert.Equal("8GB", configuration.Value);
        }

        [Fact]
        public async Task AddToBasket_ReturnsLaptopConfigurationList()
        {
            // Arrange
            var mockRepo = new Mock<ILaptopRepository>();
            mockRepo.Setup(s => s.GetLaptopList())
                .ReturnsAsync(MockDataProvider.GetMockLaptopList());
            mockRepo.Setup(s => s.GetConfigurationList())
                .ReturnsAsync(MockDataProvider.GetMockLaptopConfigurationList());
            mockRepo.Setup(s => s.AddToBasket(It.IsAny<BasketItem>()))
                .ReturnsAsync(MockDataProvider.GetBasketItems());
            var service = new LaptopService(mockRepo.Object);

            // Act
            var result = await service.AddToBasket(It.IsAny<BasketItem>());

            // Assert
            var returnValue = Assert.IsType<BasketViewModel>(result);
            Assert.NotNull(returnValue);
            var basketItems = Assert.IsType<List<BasketItems>>(returnValue.BasketItems);
            var basketItem = basketItems.FirstOrDefault();
            Assert.NotNull(basketItem);
            Assert.NotNull(basketItem.Laptop);
            Assert.Equal("Dell", basketItem.Laptop.Name);

            var configurations = Assert.IsType<List<LaptopConfiguration>>(basketItem.LaptopConfigurations);
            var configuration = configurations.FirstOrDefault();
            Assert.NotNull(configuration);
            Assert.Equal((decimal)45.67, configuration.Price);
        }
    }
}
