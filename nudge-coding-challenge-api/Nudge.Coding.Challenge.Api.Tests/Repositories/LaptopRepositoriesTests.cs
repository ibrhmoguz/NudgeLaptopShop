using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Nudge.LaptopShop.Api.Common;
using Nudge.LaptopShop.Api.Data;
using Nudge.LaptopShop.Api.Models;
using Nudge.LaptopShop.Api.Repositories;
using Xunit;

namespace Nudge.LaptopShop.Api.Tests.Repositories
{
    public class LaptopRepositoriesTests
    {
        DbContextOptions<LaptopShopContext> _options = new DbContextOptionsBuilder<LaptopShopContext>()
            .UseInMemoryDatabase(databaseName: "LaptopShopDb")
            .Options;

        [Fact]
        public async Task GetLaptopList_ReturnsLaptopList()
        {
            // Arrange
            await using var context = new LaptopShopContext(_options);
            await context.Laptops.AddRangeAsync(MockDataProvider.GetMockLaptopList());
            await context.SaveChangesAsync();

            // Act
            var result = await new LaptopRepository(context).GetLaptopList();

            // Assert
            var returnValue = Assert.IsType<Laptop[]>(result);
            var laptop = returnValue.FirstOrDefault();
            Assert.NotNull(laptop);
            Assert.Equal("Dell", laptop.Name);
        }

        [Fact]
        public async Task GetConfigurationList_ReturnsLaptopList()
        {
            // Arrange
            await using var context = new LaptopShopContext(_options);
            await context.LaptopConfigurations.AddRangeAsync(MockDataProvider.GetMockLaptopConfigurationList());
            await context.SaveChangesAsync();

            // Act
            var result = await new LaptopRepository(context).GetConfigurationList();

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
            await using var context = new LaptopShopContext(_options);
            await context.Basket.AddRangeAsync(MockDataProvider.GetBasketItems());
            await context.SaveChangesAsync();

            // Act
            var item = new BasketItem
            {
                LaptopId = 2,
                LaptopConfigurationIdList = new List<int> { 1, 4, 5 }
            };
            var result = await new LaptopRepository(context).AddToBasket(item);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Basket[]>(result);
            Assert.Equal(6, result.Length);
            Assert.Equal(2, result.Select(r => r.LaptopId).Distinct().Count());
        }
    }
}
