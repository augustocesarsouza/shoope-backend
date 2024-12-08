using FluentValidation.Results;
using Microsoft.VisualBasic;
using Moq;
using Shoope.Application.DTOs;
using Shoope.Application.Services;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Xunit;

namespace Shoope.Application.ServicesTests.ProductDetailServiceTest
{
    public class ProductDetailServiceTest
    {
        private readonly ProductDetailServiceConfiguration _productDetailServiceConfiguration;
        private readonly ProductDetailService _productDetailService;

        public ProductDetailServiceTest()
        {
            _productDetailServiceConfiguration = new();
            var productDetailService = new ProductDetailService(_productDetailServiceConfiguration.ProductDetailRepositoryMock.Object,
                _productDetailServiceConfiguration.MapperMock.Object, _productDetailServiceConfiguration.UnitOfWorkMock.Object,
                _productDetailServiceConfiguration.ProductDetailCreateDTOValidatorMock.Object);

            _productDetailService = productDetailService;
        }

        [Fact]
        public async Task Should_GetProductDetailByProductId_Success()
        {
            Guid productId = Guid.NewGuid();

            _productDetailServiceConfiguration.ProductDetailRepositoryMock
                .Setup(rep => rep.GetProductDetailByProductId(It.IsAny<Guid>()))
                .ReturnsAsync(new ProductDetail());

            var result = await _productDetailService.GetProductDetailByProductId(productId);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_GetProductDetailByProductId()
        {
            Guid productId = Guid.NewGuid();

            _productDetailServiceConfiguration.ProductDetailRepositoryMock
                .Setup(rep => rep.GetProductDetailByProductId(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("error get cupon by id"));

            var result = await _productDetailService.GetProductDetailByProductId(productId);

            Assert.False(result.IsSucess);
            Assert.Equal("error get cupon by id", result.Message);
        }

        [Fact]
        public async Task Should_CreateAsync_Success()
        {
            ProductDetailDTO productDetailDTO = new ProductDetailDTO();

            _productDetailServiceConfiguration.ProductDetailCreateDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<ProductDetailDTO>())).Returns(new ValidationResult());

            _productDetailServiceConfiguration.ProductDetailRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<ProductDetail>()))
                .ReturnsAsync(new ProductDetail());

            var result = await _productDetailService.CreateAsync(productDetailDTO);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Give_Error_DTO_Is_Null_CreateAsync()
        {
            var result = await _productDetailService.CreateAsync(null);
            Assert.False(result.IsSucess);
            Assert.Equal("error DTO is null", result.Message);
        }

        [Fact]
        public async Task Should_Give_Error_Validate_DTO_CreateAsync()
        {
            ProductDetailDTO productDetailDTO = new ProductDetailDTO();

            _productDetailServiceConfiguration.ProductDetailCreateDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<ProductDetailDTO>()))
                .Returns(new ValidationResult(new List<ValidationFailure>
                            {
                            new ValidationFailure("PropertyName", "Error message 1"),
                            }));

            var result = await _productDetailService.CreateAsync(productDetailDTO);
            Assert.False(result.IsSucess);
            Assert.Equal("validation error check the information", result.Message);
        }

        [Fact]
        public async Task Should_Throw_Error_Exception_CreateAsync_()
        {
            ProductDetailDTO productDetailDTO = new ProductDetailDTO();

            _productDetailServiceConfiguration.ProductDetailCreateDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<ProductDetailDTO>())).Returns(new ValidationResult());

            _productDetailServiceConfiguration.ProductDetailRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<ProductDetail>()))
                .ThrowsAsync(new Exception("error create ProductDetail"));

            var result = await _productDetailService.CreateAsync(productDetailDTO);
            Assert.False(result.IsSucess);
            Assert.Equal("error create ProductDetail", result.Message);
        }
    }
}
