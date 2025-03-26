using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using System.Collections.Generic;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Entities.OrderTests
{
    public class OrderTotal
    {
        private readonly Address _testAddress = new("123 Main St.", "Seattle", "WA", "98101", "USA");

        [Fact]
        public void CalculatesTotalOfOrderItems()
        {
            var orderItems = new List<OrderItem>
            {
                new(new CatalogItemOrdered(1, "Test Item 1", "test.jpg"), 10.00m, 2),
                new(new CatalogItemOrdered(2, "Test Item 2", "test.jpg"), 15.00m, 3)
            };
            var order = new Order("buyerId", _testAddress, orderItems);

            var total = order.Total();

            Assert.Equal(65.00m, total);
        }

        [Fact]
        public void ReturnsZeroForEmptyOrder()
        {
            var order = new Order("buyerId", _testAddress, new List<OrderItem>());

            var total = order.Total();

            Assert.Equal(0m, total);
        }

        [Fact]
        public void CalculatesTotalWithSingleItem()
        {
            var orderItems = new List<OrderItem>
            {
                new(new CatalogItemOrdered(1, "Test Item", "test.jpg"), 10.00m, 1)
            };
            var order = new Order("buyerId", _testAddress, orderItems);

            var total = order.Total();

            Assert.Equal(10.00m, total);
        }

        [Fact]
        public void HandlesDecimalPrices()
        {
            var orderItems = new List<OrderItem>
            {
                new(new CatalogItemOrdered(1, "Test Item 1", "test.jpg"), 10.99m, 2),
                new(new CatalogItemOrdered(2, "Test Item 2", "test.jpg"), 15.99m, 3)
            };
            var order = new Order("buyerId", _testAddress, orderItems);

            var total = order.Total();

            Assert.Equal(69.95m, total);
        }

        [Fact]
        public void HandlesSameItemMultipleTimes()
        {
            var orderItems = new List<OrderItem>
            {
                new(new CatalogItemOrdered(1, "Test Item", "test.jpg"), 10.00m, 2),
                new(new CatalogItemOrdered(1, "Test Item", "test.jpg"), 10.00m, 3)
            };
            var order = new Order("buyerId", _testAddress, orderItems);

            var total = order.Total();

            Assert.Equal(50.00m, total);
        }

        [Fact]
        public void HandlesLargeQuantities()
        {
            var orderItems = new List<OrderItem>
            {
                new(new CatalogItemOrdered(1, "Test Item", "test.jpg"), 10.00m, 999)
            };
            var order = new Order("buyerId", _testAddress, orderItems);

            var total = order.Total();

            Assert.Equal(9990.00m, total);
        }

        [Fact]
        public void HandlesLargePrices()
        {
            var orderItems = new List<OrderItem>
            {
                new(new CatalogItemOrdered(1, "Test Item 1", "test.jpg"), 99999.99m, 1),
                new(new CatalogItemOrdered(2, "Test Item 2", "test.jpg"), 99999.99m, 1)
            };
            var order = new Order("buyerId", _testAddress, orderItems);

            var total = order.Total();

            Assert.Equal(199999.98m, total);
        }

        [Fact]
        public void CalculatesCorrectlyWithManyItems()
        {
            var orderItems = new List<OrderItem>();
            for (int i = 1; i <= 10; i++)
            {
                orderItems.Add(new OrderItem(
                    new CatalogItemOrdered(i, $"Test Item {i}", "test.jpg"),
                    i * 1.5m,
                    i));
            }
            var order = new Order("buyerId", _testAddress, orderItems);

            var total = order.Total();

            // Sum of (i * 1.5 * i) for i from 1 to 10
            Assert.Equal(577.50m, total);
        }

        [Fact]
        public void PreservesDecimalPrecision()
        {
            var orderItems = new List<OrderItem>
            {
                new(new CatalogItemOrdered(1, "Test Item 1", "test.jpg"), 1.23m, 3),
                new(new CatalogItemOrdered(2, "Test Item 2", "test.jpg"), 4.56m, 2)
            };
            var order = new Order("buyerId", _testAddress, orderItems);

            var total = order.Total();

            Assert.Equal(12.81m, total);
        }
    }
}
