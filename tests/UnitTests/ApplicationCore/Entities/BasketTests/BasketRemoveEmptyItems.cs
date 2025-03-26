using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Entities.BasketTests
{
    public class BasketRemoveEmptyItems
    {
        private const string TestBuyerId = "test-buyer-id";

        [Fact]
        public void RemovesEmptyBasketItems()
        {
            var basket = new Basket(TestBuyerId);
            basket.AddItem(1, 10, 1);
            basket.AddItem(2, 15, 0);
            basket.AddItem(3, 20, 0);
            basket.AddItem(4, 25, 2);

            basket.RemoveEmptyItems();

            Assert.Equal(2, basket.Items.Count);
            Assert.Contains(basket.Items, i => i.CatalogItemId == 1);
            Assert.Contains(basket.Items, i => i.CatalogItemId == 4);
        }

        [Fact]
        public void HandlesEmptyBasket()
        {
            var basket = new Basket(TestBuyerId);

            basket.RemoveEmptyItems();

            Assert.Empty(basket.Items);
        }

        [Fact]
        public void RemovesAllItemsWhenAllQuantitiesAreZero()
        {
            var basket = new Basket(TestBuyerId);
            basket.AddItem(1, 10, 0);
            basket.AddItem(2, 15, 0);
            basket.AddItem(3, 20, 0);

            basket.RemoveEmptyItems();

            Assert.Empty(basket.Items);
        }
    }
}
