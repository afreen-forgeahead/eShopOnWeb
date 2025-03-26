using Microsoft.AspNetCore.Http;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Exceptions;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.PublicApi.CatalogItemEndpoints
{
    public class CreateCatalogItemEndpointTests
    {
        private readonly Mock<IRepository<CatalogItem>> _mockItemRepository;
        private readonly Mock<IUriComposer> _mockUriComposer;
        private readonly CreateCatalogItemEndpoint _endpoint;

        public CreateCatalogItemEndpointTests()
        {
            _mockItemRepository = new Mock<IRepository<CatalogItem>>();
            _mockUriComposer = new Mock<IUriComposer>();
            _endpoint = new CreateCatalogItemEndpoint(_mockUriComposer.Object);
        }

        [Fact]
        public async Task HandleAsync_WithValidRequest_CreatesNewItem()
        {
            // Arrange
            var request = new CreateCatalogItemRequest
            {
                Name = "Test Item",
                Description = "Test Description",
                Price = 10.99m,
                CatalogBrandId = 1,
                CatalogTypeId = 2,
                PictureUri = "test.jpg"
            };

            _mockItemRepository
                .Setup(x => x.CountAsync(It.IsAny<CatalogItemNameSpecification>(), default))
                .ReturnsAsync(0);

            var createdItem = new CatalogItem(
                request.CatalogTypeId,
                request.CatalogBrandId,
                request.Description,
                request.Name,
                request.Price,
                request.PictureUri);

            // Use reflection to set the Id since it's not publicly settable
            typeof(CatalogItem).GetProperty("Id")?.SetValue(createdItem, 1, null);

            _mockItemRepository
                .Setup(x => x.AddAsync(It.IsAny<CatalogItem>(), default))
                .ReturnsAsync(createdItem);

            _mockUriComposer
                .Setup(x => x.ComposePicUri(It.IsAny<string>()))
                .Returns("http://test.com/test.jpg");

            // Act
            var result = await _endpoint.HandleAsync(request, _mockItemRepository.Object);

            // Assert
            Assert.NotNull(result);
            var resultValue = (result as IResult);
            Assert.NotNull(resultValue);
        }

        [Fact]
        public async Task HandleAsync_WithDuplicateName_ThrowsDuplicateException()
        {
            // Arrange
            var request = new CreateCatalogItemRequest
            {
                Name = "Existing Item",
                Description = "Test Description",
                Price = 10.99m,
                CatalogBrandId = 1,
                CatalogTypeId = 2,
                PictureUri = "test.jpg"
            };

            _mockItemRepository
                .Setup(x => x.CountAsync(It.IsAny<CatalogItemNameSpecification>(), default))
                .ReturnsAsync(1);

            // Act & Assert
            await Assert.ThrowsAsync<DuplicateException>(() =>
                _endpoint.HandleAsync(request, _mockItemRepository.Object));
        }


        [Fact]
        public async Task HandleAsync_ShouldComposeUri_ForCreatedItem()
        {
            // Arrange
            var request = new CreateCatalogItemRequest
            {
                Name = "Test Item",
                Description = "Test Description",
                Price = 10.99m,
                CatalogBrandId = 1,
                CatalogTypeId = 2,
                PictureUri = "test.jpg"
            };

            _mockItemRepository
                .Setup(x => x.CountAsync(It.IsAny<CatalogItemNameSpecification>(), default))
                .ReturnsAsync(0);

            var createdItem = new CatalogItem(
                request.CatalogTypeId,
                request.CatalogBrandId,
                request.Description,
                request.Name,
                request.Price,
                request.PictureUri);

            // Use reflection to set the Id
            typeof(CatalogItem).GetProperty("Id")?.SetValue(createdItem, 1, null);

            _mockItemRepository
                .Setup(x => x.AddAsync(It.IsAny<CatalogItem>(), default))
                .ReturnsAsync(createdItem);

            const string expectedUri = "http://test.com/eCatalog-item-default.png";
            _mockUriComposer
                .Setup(x => x.ComposePicUri(It.IsAny<string>()))
                .Returns(expectedUri);

            // Act
            var result = await _endpoint.HandleAsync(request, _mockItemRepository.Object);

            // Assert
            Assert.NotNull(result);
        }
    }
}
