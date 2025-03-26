using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Xunit;
using System.Linq;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Entities.BasketTests
{
    public class BasketTests
    {
        private readonly string _testBuyerId = "test-buyer-id";
        private readonly int _testCatalogItemId = 123;
        private readonly decimal _testUnitPrice = 10.00m;

        [Fact]
        public void CreateBasketWithValidBuyerId()
        {
            // Act
            var basket = new Basket(_testBuyerId);

            // Assert
            Assert.Equal(_testBuyerId, basket.BuyerId);
            Assert.Empty(basket.Items);
        }

        [Fact]
        public void AddNewItemToEmptyBasket()
        {
            // Arrange
            var basket = new Basket(_testBuyerId);

            // Act
            basket.AddItem(_testCatalogItemId, _testUnitPrice, 1);

            // Assert
            var item = Assert.Single(basket.Items);
            Assert.Equal(_testCatalogItemId, item.CatalogItemId);
            Assert.Equal(_testUnitPrice, item.UnitPrice);
            Assert.Equal(1, item.Quantity);
        }

        [Fact]
        public void AddItemIncreasesQuantityWhenItemExists()
        {
            // Arrange
            var basket = new Basket(_testBuyerId);
            basket.AddItem(_testCatalogItemId, _testUnitPrice, 1);

            // Act
            basket.AddItem(_testCatalogItemId, _testUnitPrice, 2);

            // Assert
            var item = Assert.Single(basket.Items);
            Assert.Equal(3, item.Quantity);
        }

        [Fact]
        public void AddMultipleDifferentItems()
        {
            // Arrange
            var basket = new Basket(_testBuyerId);

            // Act
            basket.AddItem(1, 10.0m, 1);
            basket.AddItem(2, 20.0m, 1);
            basket.AddItem(3, 30.0m, 1);

            // Assert
            Assert.Equal(3, basket.Items.Count);
        }

        [Fact]
        public void DefaultsToQuantityOfOneWhenNotSpecified()
        {
            // Arrange
            var basket = new Basket(_testBuyerId);

            // Act
            basket.AddItem(_testCatalogItemId, _testUnitPrice);

            // Assert
            Assert.Equal(1, basket.Items.First().Quantity);
        }

        [Fact]
        public void CantAddItemWithNegativeQuantity()
        {
            // Arrange
            var basket = new Basket(_testBuyerId);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => 
                basket.AddItem(_testCatalogItemId, _testUnitPrice, -1));
        }

        [Fact]
        public void AddItemWithMaxQuantity()
        {
            // Arrange
            var basket = new Basket(_testBuyerId);

            // Act
            basket.AddItem(_testCatalogItemId, _testUnitPrice, int.MaxValue);

            // Assert
            Assert.Equal(int.MaxValue, basket.Items.First().Quantity);
        }

        [Fact]
        public void MaintainsItemOrder()
        {
            // Arrange
            var basket = new Basket(_testBuyerId);

            // Act
            for (int i = 1; i <= 5; i++)
            {
                basket.AddItem(i, _testUnitPrice * i, 1);
            }

            // Assert
            var items = basket.Items.ToList();
            for (int i = 0; i < 5; i++)
            {
                Assert.Equal(i + 1, items[i].CatalogItemId);
            }
        }
    }
}
