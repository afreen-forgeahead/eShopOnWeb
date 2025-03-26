// File: D:\Unit_tests\eShopOnWeb\tests\UnitTests\PublicApi\CatalogItemEndpoints\DeleteCatalogItemRequestTests.cs

using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.PublicApi.CatalogItemEndpoints
{
    public class DeleteCatalogItemRequestTests
    {
        [Fact]
        public void Should_SetId_WhenConstructed()
        {
            // Arrange & Act
            int expectedId = 123;
            var request = new DeleteCatalogItemRequest(expectedId);

            // Assert
            Assert.Equal(expectedId, request.CatalogItemId);  // Changed from Id to CatalogItemId
        }
    }
}
