using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using System;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.PublicApi.CatalogItemEndpoints
{
    public class GetByIdCatalogItemResponseTests
    {
        [Fact]
        public void Should_SetCorrelationId_WhenConstructedWithGuid()
        {
            // Arrange
            var correlationId = Guid.NewGuid();

            // Act
            var response = new GetByIdCatalogItemResponse(correlationId);

            // Assert
            Assert.Equal(correlationId.ToString(), response.CorrelationId().ToString());
        }

        [Fact]
        public void Should_CreateNewCorrelationId_WhenConstructedWithoutParameters()
        {
            // Arrange & Act
            var response = new GetByIdCatalogItemResponse();

            // Assert
            Assert.NotEqual(Guid.Empty.ToString(), response.CorrelationId().ToString());
        }

        [Fact]
        public void Should_AllowSettingCatalogItem()
        {
            // Arrange
            var response = new GetByIdCatalogItemResponse();
            var catalogItem = new CatalogItemDto
            {
                Id = 1,
                Name = "Test Product",
                Description = "Test Description",
                Price = 19.99m,
                PictureUri = "test.jpg",
                CatalogTypeId = 2,
                CatalogBrandId = 3
            };

            // Act
            response.CatalogItem = catalogItem;

            // Assert
            Assert.NotNull(response.CatalogItem);
            Assert.Equal(1, response.CatalogItem.Id);
            Assert.Equal("Test Product", response.CatalogItem.Name);
            Assert.Equal("Test Description", response.CatalogItem.Description);
            Assert.Equal(19.99m, response.CatalogItem.Price);
            Assert.Equal("test.jpg", response.CatalogItem.PictureUri);
            Assert.Equal(2, response.CatalogItem.CatalogTypeId);
            Assert.Equal(3, response.CatalogItem.CatalogBrandId);
        }

        [Fact]
        public void Should_AllowNullCatalogItem()
        {
            // Arrange
            var response = new GetByIdCatalogItemResponse();

            // Act
            response.CatalogItem = null;

            // Assert
            Assert.Null(response.CatalogItem);
        }

        [Fact]
        public void Should_InitializeWithNullCatalogItem()
        {
            // Arrange & Act
            var response = new GetByIdCatalogItemResponse();

            // Assert
            Assert.Null(response.CatalogItem);
        }
    }
}
