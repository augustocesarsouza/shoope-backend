using Moq;
using Shoope.Application.DTOs;
using Shoope.Application.Services;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Xunit;

namespace Shoope.Application.ServicesTests.ProductSellerServiceTest
{
    public class ProductSellerServiceTest
    {
        private readonly ProductSellerServiceConfiguration _productSellerServiceConfiguration;
        private readonly ProductSellerService _productSellerService;

        public ProductSellerServiceTest()
        {
            _productSellerServiceConfiguration = new();
            var productSellerService = new ProductSellerService(
                _productSellerServiceConfiguration.ProductSellerRepositoryMock.Object,
                _productSellerServiceConfiguration.MapperMock.Object,
                _productSellerServiceConfiguration.UnitOfWorkMock.Object);

            _productSellerService = productSellerService;
        }

        [Fact]
        public async Task Should_GetById_Success()
        {
            Guid productId = Guid.NewGuid();

            _productSellerServiceConfiguration.ProductSellerRepositoryMock
                .Setup(rep => rep.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(new ProductSeller());

            var result = await _productSellerService.GetById(productId);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_GetById()
        {
            Guid productId = Guid.NewGuid();

            _productSellerServiceConfiguration.ProductSellerRepositoryMock
                .Setup(rep => rep.GetById(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("error GetById"));

            var result = await _productSellerService.GetById(productId);

            Assert.False(result.IsSucess);
            Assert.Equal("error GetById", result.Message);
        }

        [Fact]
        public async Task Should_CreateAsync_Successfully()
        {
            var productSellerDTO = new ProductSellerDTO();

            _productSellerServiceConfiguration.ProductSellerRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<ProductSeller>()))
                .ReturnsAsync(new ProductSeller());

            var result = await _productSellerService.Create(productSellerDTO);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Throw_Error_DTO_Is_Null_CreateAsync()
        {
            var result = await _productSellerService.Create(null);

            Assert.False(result.IsSucess);
            Assert.Equal("error DTO informed is Null", result.Message);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_CreateAsync()
        {
            var productSellerDTO = new ProductSellerDTO();

            _productSellerServiceConfiguration.ProductSellerRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<ProductSeller>()))
                .ThrowsAsync(new Exception("error CreateAsync"));

            var result = await _productSellerService.Create(productSellerDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("error CreateAsync", result.Message);
        }
    }
}
