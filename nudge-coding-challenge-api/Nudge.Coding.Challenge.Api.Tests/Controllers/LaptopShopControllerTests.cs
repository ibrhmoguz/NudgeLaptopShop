using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Moq;
using Nudge.LaptopShop.Api.Controllers;
using Nudge.LaptopShop.Api.Interfaces;
using Nudge.LaptopShop.Api.Models;
using Xunit;

namespace Nudge.LaptopShop.Api.Tests.Controllers
{
    public class LaptopShopControllerTests
    {
        private readonly HttpClient _client;

        public LaptopShopControllerTests()
        {
            var builder = new WebHostBuilder().UseStartup<Startup>();
            var testServer = new TestServer(builder);
            _client = testServer.CreateClient();
        }

        [Fact]
        public async Task Health_ReturnsHealthy_WhenApiIsRunning()
        {
            var response = await _client.GetAsync("/health");
            var responseString = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            responseString.Should().Be("Healthy");
        }

        [Fact]
        public async Task LaptopList_ReturnsLaptopList()
        {
            // Arrange
            var mockService = new Mock<ILaptopService>();
            mockService.Setup(s => s.GetLaptopList())
                .ReturnsAsync(MockDataProvider.GetMockLaptopList());
            var controller = new LaptopShopController(mockService.Object);

            // Act
            var result = await controller.LaptopList();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Laptop[]>(okResult.Value);
            var laptop = returnValue.FirstOrDefault();
            Assert.NotNull(laptop);
            Assert.Equal("Dell", laptop.Name);
        }

        [Fact]
        public async Task ConfigurationList_ReturnsLaptopConfigurationList()
        {
            // Arrange
            var mockService = new Mock<ILaptopService>();
            mockService.Setup(s => s.GetConfigurationList())
                .ReturnsAsync(MockDataProvider.GetMockLaptopConfigurationList());
            var controller = new LaptopShopController(mockService.Object);

            // Act
            var result = await controller.ConfigurationList();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<LaptopConfiguration[]>(okResult.Value);
            var configuration = returnValue.FirstOrDefault();
            Assert.NotNull(configuration);
            Assert.Equal("Ram", configuration.Name);
            Assert.Equal("8GB", configuration.Value);
        }

        [Fact]
        public async Task AddToBasket_ReturnsLaptopConfigurationList()
        {
            // Arrange
            var mockService = new Mock<ILaptopService>();
            mockService.Setup(s => s.AddToBasket(It.IsAny<BasketItem>()))
                .ReturnsAsync(MockDataProvider.GetBasket());
            var controller = new LaptopShopController(mockService.Object);

            // Act
            var result = await controller.AddToBasket(It.IsAny<BasketItem>());

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<BasketViewModel>(okResult.Value);
            Assert.NotNull(returnValue);
            var basketItems = Assert.IsType<List<BasketItems>>(returnValue.BasketItems);
            var basketItem = basketItems.FirstOrDefault();
            Assert.NotNull(basketItem);
            Assert.NotNull(basketItem.Laptop);
            Assert.Equal("Dell", basketItem.Laptop.Name);

            var configurations = Assert.IsType<List<LaptopConfiguration>>(basketItem.LaptopConfigurations);
            var configuration = configurations.FirstOrDefault();
            Assert.NotNull(configuration);
            Assert.Equal("8GB", configuration.Value);
        }
    }
}
