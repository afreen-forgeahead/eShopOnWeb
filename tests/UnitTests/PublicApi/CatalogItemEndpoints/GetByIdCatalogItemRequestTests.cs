using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using Microsoft.eShopWeb.PublicApi; // Add this for BaseRequest
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.PublicApi.CatalogItemEndpoints
{
    public class GetByIdCatalogItemRequestTests
    {
        [Fact]
        public void Constructor_SetsPropertyCorrectly()
        {
            // Arrange
            int expectedId = 123;

            // Act
            var request = new GetByIdCatalogItemRequest(expectedId);

            // Assert
            Assert.Equal(expectedId, request.CatalogItemId);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(int.MaxValue)]
        public void CatalogItemId_SetViaConstructor_ReturnsExpectedValue(int catalogItemId)
        {
            // Arrange & Act
            var request = new GetByIdCatalogItemRequest(catalogItemId);

            // Assert
            Assert.Equal(catalogItemId, request.CatalogItemId);
        }

        [Fact]
        public void Request_InheritsFromBaseRequest()
        {
            // Arrange & Act
            var request = new GetByIdCatalogItemRequest(1);

            // Assert
            Assert.IsAssignableFrom<BaseRequest>(request);
        }
    }
}
