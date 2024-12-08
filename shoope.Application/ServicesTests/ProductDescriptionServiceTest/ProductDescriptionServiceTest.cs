using Moq;
using Shoope.Application.DTOs;
using Shoope.Application.Services;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Xunit;

namespace Shoope.Application.ServicesTests.ProductDescriptionServiceTest
{
    public class ProductDescriptionServiceTest
    {
        private readonly ProductDescriptionServiceConfiguration _productDescriptionServiceConfiguration;
        private readonly ProductDescriptionService _productDescriptionService;

        public ProductDescriptionServiceTest()
        {
            _productDescriptionServiceConfiguration = new();
            var productDescriptionService = new ProductDescriptionService(_productDescriptionServiceConfiguration.ProductDescriptionRepositoryMock.Object,
                _productDescriptionServiceConfiguration.MapperMock.Object, _productDescriptionServiceConfiguration.UnitOfWorkMock.Object);

            _productDescriptionService = productDescriptionService;
        }

        [Fact]
        public async Task Should_GetProductDescriptionByProductId_Success()
        {
            Guid productId = Guid.NewGuid();

            _productDescriptionServiceConfiguration.ProductDescriptionRepositoryMock
                .Setup(rep => rep.GetProductDescriptionByProductId(It.IsAny<Guid>()))
                .ReturnsAsync(new ProductDescription());

            var result = await _productDescriptionService.GetProductDescriptionByProductId(productId);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_GetProductDescriptionByProductId()
        {
            Guid cuponId = Guid.NewGuid();

            _productDescriptionServiceConfiguration.ProductDescriptionRepositoryMock
                .Setup(rep => rep.GetProductDescriptionByProductId(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("error get GetProductDescriptionByProductId"));

            var result = await _productDescriptionService.GetProductDescriptionByProductId(cuponId);

            Assert.False(result.IsSucess);
            Assert.Equal("error get GetProductDescriptionByProductId", result.Message);
        }

        [Fact]
        public async Task Should_CreateAsync_Success()
        {
            ProductDescriptionDTO productDescriptionDTO = new ProductDescriptionDTO();

            _productDescriptionServiceConfiguration.ProductDescriptionRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<ProductDescription>()))
                .ReturnsAsync(new ProductDescription());

            var result = await _productDescriptionService.CreateAsync(productDescriptionDTO);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Null_DTO_CreateAsync()
        {
            var result = await _productDescriptionService.CreateAsync(null);
            Assert.False(result.IsSucess);
            Assert.Equal("error DTO is null", result.Message);
        }

        [Fact]
        public async Task Should_Throw_Error_Exception_CreateAsync()
        {
            ProductDescriptionDTO productDescriptionDTO = new ProductDescriptionDTO();

            _productDescriptionServiceConfiguration.ProductDescriptionRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<ProductDescription>()))
                .ThrowsAsync(new Exception("error when Create Product"));

            var result = await _productDescriptionService.CreateAsync(productDescriptionDTO);
            Assert.False(result.IsSucess);
            Assert.Equal("error when Create Product", result.Message);
        }
    }
}
