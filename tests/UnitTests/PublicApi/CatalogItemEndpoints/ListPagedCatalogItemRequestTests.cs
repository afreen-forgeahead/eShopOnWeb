using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using Microsoft.eShopWeb.PublicApi;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.PublicApi.CatalogItemEndpoints
{
    public class ListPagedCatalogItemRequestTests
    {
        [Fact]
        public void Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            int expectedPageSize = 10;
            int expectedPageIndex = 1;
            int? expectedCatalogBrandId = 1;
            int? expectedCatalogTypeId = 2;

            // Act
            var request = new ListPagedCatalogItemRequest(
                pageSize: expectedPageSize,
                pageIndex: expectedPageIndex,
                catalogBrandId: expectedCatalogBrandId,
                catalogTypeId: expectedCatalogTypeId);

            // Assert
            Assert.Equal(expectedPageSize, request.PageSize);
            Assert.Equal(expectedPageIndex, request.PageIndex);
            Assert.Equal(expectedCatalogBrandId, request.CatalogBrandId);
            Assert.Equal(expectedCatalogTypeId, request.CatalogTypeId);
        }

        [Theory]
        [InlineData(0, 0, null, null)]
        [InlineData(20, 1, 1, 1)]
        [InlineData(50, 2, 2, 3)]
        [InlineData(100, 10, 999, 999)]
        [InlineData(1, 0, 0, 0)]
        [InlineData(int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue)]
        public void Request_WithDifferentValues_SetsPropertiesCorrectly(
            int pageSize, 
            int pageIndex, 
            int? catalogBrandId, 
            int? catalogTypeId)
        {
            // Arrange & Act
            var request = new ListPagedCatalogItemRequest(
                pageSize, 
                pageIndex, 
                catalogBrandId, 
                catalogTypeId);

            // Assert
            Assert.Equal(pageSize, request.PageSize);
            Assert.Equal(pageIndex, request.PageIndex);
            Assert.Equal(catalogBrandId, request.CatalogBrandId);
            Assert.Equal(catalogTypeId, request.CatalogTypeId);
        }

        [Theory]
        [InlineData(-1, 0, null, null)]
        [InlineData(0, -1, null, null)]
        [InlineData(-10, -10, null, null)]
        public void Request_WithNegativeValues_SetsPropertiesCorrectly(
            int pageSize,
            int pageIndex,
            int? catalogBrandId,
            int? catalogTypeId)
        {
            // Arrange & Act
            var request = new ListPagedCatalogItemRequest(
                pageSize,
                pageIndex,
                catalogBrandId,
                catalogTypeId);

            // Assert
            Assert.Equal(pageSize, request.PageSize);
            Assert.Equal(pageIndex, request.PageIndex);
            Assert.Null(request.CatalogBrandId);
            Assert.Null(request.CatalogTypeId);
        }

        [Fact]
        public void Request_WithNullBrandIdAndTypeId_SetsPropertiesCorrectly()
        {
            // Arrange & Act
            var request = new ListPagedCatalogItemRequest(10, 1, null, null);

            // Assert
            Assert.Equal(10, request.PageSize);
            Assert.Equal(1, request.PageIndex);
            Assert.Null(request.CatalogBrandId);
            Assert.Null(request.CatalogTypeId);
        }

        [Fact]
        public void Request_WithZeroPageSizeAndIndex_SetsPropertiesCorrectly()
        {
            // Arrange & Act
            var request = new ListPagedCatalogItemRequest(0, 0, 1, 1);

            // Assert
            Assert.Equal(0, request.PageSize);
            Assert.Equal(0, request.PageIndex);
            Assert.Equal(1, request.CatalogBrandId);
            Assert.Equal(1, request.CatalogTypeId);
        }

        [Fact]
        public void Request_WithMaxValues_SetsPropertiesCorrectly()
        {
            // Arrange & Act
            var request = new ListPagedCatalogItemRequest(
                int.MaxValue,
                int.MaxValue,
                int.MaxValue,
                int.MaxValue);

            // Assert
            Assert.Equal(int.MaxValue, request.PageSize);
            Assert.Equal(int.MaxValue, request.PageIndex);
            Assert.Equal(int.MaxValue, request.CatalogBrandId);
            Assert.Equal(int.MaxValue, request.CatalogTypeId);
        }

        [Fact]
        public void Request_InheritsFromBaseRequest()
        {
            // Arrange & Act
            var request = new ListPagedCatalogItemRequest(1, 0, null, null);

            // Assert
            Assert.IsAssignableFrom<BaseRequest>(request);
        }
    }
}
