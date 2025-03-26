using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using System.Linq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Entities.BasketTests
{
    public class BasketTotalItems
    {
        private const string TestBuyerId = "test-buyer-id";

        [Fact]
        public void ReturnsTotalQuantityOfItems()
        {
            var basket = new Basket(TestBuyerId);
            basket.AddItem(1, 10.00m, 2);
            basket.AddItem(2, 15.00m, 3);

            var total = basket.TotalItems;

            Assert.Equal(5, total);
        }

        [Fact]
        public void ReturnsZeroWhenBasketIsEmpty()
        {
            var basket = new Basket(TestBuyerId);

            var total = basket.TotalItems;

            Assert.Equal(0, total);
        }

        [Fact]
        public void CalculatesTotalWithSameItemMultipleTimes()
        {
            var basket = new Basket(TestBuyerId);
            basket.AddItem(1, 10.00m, 2);
            basket.AddItem(1, 10.00m, 3);

            var total = basket.TotalItems;

            Assert.Equal(5, total);
        }

        [Fact]
        public void HandlesDifferentPricesForSameItem()
        {
            var basket = new Basket(TestBuyerId);
            basket.AddItem(1, 10.00m, 2);
            basket.AddItem(1, 15.00m, 3);

            var total = basket.TotalItems;

            Assert.Equal(5, total);
        }

        [Fact]
        public void HandlesZeroQuantityItems()
        {
            var basket = new Basket(TestBuyerId);
            basket.AddItem(1, 10.00m, 0);
            basket.AddItem(2, 15.00m, 2);
            basket.AddItem(3, 20.00m, 0);

            var total = basket.TotalItems;

            Assert.Equal(2, total);
        }

        [Fact]
        public void UpdatesTotalWhenItemQuantityChanged()
        {
            var basket = new Basket(TestBuyerId);
            basket.AddItem(1, 10.00m, 2);
            basket.AddItem(2, 15.00m, 3);

            Assert.Equal(5, basket.TotalItems);

            var firstItem = basket.Items.First(i => i.CatalogItemId == 1);
            firstItem.SetQuantity(4);

            Assert.Equal(7, basket.TotalItems);
        }

        [Fact]
        public void UpdatesWhenItemRemoved()
        {
            var basket = new Basket(TestBuyerId);
            basket.AddItem(1, 10.00m, 2);
            basket.AddItem(2, 15.00m, 3);

            Assert.Equal(5, basket.TotalItems);

            basket.RemoveEmptyItems();
            
            Assert.Equal(5, basket.TotalItems);
        }

        [Fact]
        public void HandlesLargeQuantities()
        {
            var basket = new Basket(TestBuyerId);
            basket.AddItem(1, 10.00m, 999);
            basket.AddItem(2, 15.00m, 999);

            var total = basket.TotalItems;

            Assert.Equal(1998, total);
        }
    }
}
