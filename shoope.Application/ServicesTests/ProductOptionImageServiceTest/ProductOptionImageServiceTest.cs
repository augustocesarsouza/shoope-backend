using Moq;
using Shoope.Application.DTOs;
using Shoope.Application.Services;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Shoope.Infra.Data.CloudinaryConfigClass;
using Xunit;

namespace Shoope.Application.ServicesTests.ProductOptionImageServiceTest
{
    public class ProductOptionImageServiceTest
    {
        private readonly ProductOptionImageServiceConfiguration _productOptionImageServiceConfiguration;
        private readonly ProductOptionImageService _productOptionImageService;

        public ProductOptionImageServiceTest()
        {
            _productOptionImageServiceConfiguration = new();
            var productOptionImageService = new ProductOptionImageService(
                _productOptionImageServiceConfiguration.ProductOptionImageRepositoryMock.Object,
                _productOptionImageServiceConfiguration.MapperMock.Object,
                _productOptionImageServiceConfiguration.UnitOfWorkMock.Object,
                _productOptionImageServiceConfiguration.CloudinaryUtiMock.Object);

            _productOptionImageService = productOptionImageService;
        }

        [Fact]
        public async Task Should_GetByListFlashSaleProductImageAllId_Success()
        {
            Guid productsOfferFlashId = Guid.NewGuid();

            _productOptionImageServiceConfiguration.ProductOptionImageRepositoryMock
                .Setup(rep => rep.GetByListFlashSaleProductImageAllId(It.IsAny<Guid>()))
                .ReturnsAsync(new List<ProductOptionImage>());

            var result = await _productOptionImageService.GetByListFlashSaleProductImageAllId(productsOfferFlashId);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_GetByListFlashSaleProductImageAllId()
        {
            Guid productsOfferFlashId = Guid.NewGuid();

            _productOptionImageServiceConfiguration.ProductOptionImageRepositoryMock
                .Setup(rep => rep.GetByListFlashSaleProductImageAllId(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("error GetByListFlashSaleProductImageAllId"));

            var result = await _productOptionImageService.GetByListFlashSaleProductImageAllId(productsOfferFlashId);

            Assert.False(result.IsSucess);
            Assert.Equal("error GetByListFlashSaleProductImageAllId", result.Message);
        }

        [Fact]
        public async Task Should_Create_Successfully()
        {
            var productOptionImageDTO = new ProductOptionImageDTO(null, null, null, null, null, null);
            productOptionImageDTO.SetImageUrlBase64("imageUrlBase64");

            var cloudinaryCreate = new CloudinaryCreate();
            cloudinaryCreate.ImgUrl = "ImgUrl1";
            cloudinaryCreate.PublicId = "PublicId1";

            _productOptionImageServiceConfiguration.CloudinaryUtiMock
                .Setup(valid => valid.CreateMedia(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(cloudinaryCreate);

            _productOptionImageServiceConfiguration.ProductOptionImageRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<ProductOptionImage>()))
                .ReturnsAsync(new ProductOptionImage());

            var result = await _productOptionImageService.Create(productOptionImageDTO);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Give_Error_DTO_Is_Null_Create()
        {
            var result = await _productOptionImageService.Create(null);

            Assert.False(result.IsSucess);
            Assert.Equal("error DTO informed is Null", result.Message);
        }

        [Fact]
        public async Task Should_Give_Error_ImageUrlBase64_Is_Null_Create()
        {
            var productOptionImageDTO = new ProductOptionImageDTO(null, null, null, null, null, null);

            var result = await _productOptionImageService.Create(productOptionImageDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("error ImageUrlBase64 is null", result.Message);
        }

        [Fact]
        public async Task Should_Give_Error_ImgUrl_Is_Null_Create()
        {
            var productOptionImageDTO = new ProductOptionImageDTO(null, null, null, null, null, null);
            productOptionImageDTO.SetImageUrlBase64("imageUrlBase64");

            var cloudinaryCreate = new CloudinaryCreate();
            cloudinaryCreate.ImgUrl = null;
            cloudinaryCreate.PublicId = "PublicId1";

            _productOptionImageServiceConfiguration.CloudinaryUtiMock
                .Setup(valid => valid.CreateMedia(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(cloudinaryCreate);

            var result = await _productOptionImageService.Create(productOptionImageDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("error when create ImageUrlBase", result.Message);
        }

        [Fact]
        public async Task Should_Give_Error_PublicId_Is_Null_Create()
        {
            var productOptionImageDTO = new ProductOptionImageDTO(null, null, null, null, null, null);
            productOptionImageDTO.SetImageUrlBase64("imageUrlBase64");

            var cloudinaryCreate = new CloudinaryCreate();
            cloudinaryCreate.ImgUrl = "ImgUrl1";
            cloudinaryCreate.PublicId = null;

            _productOptionImageServiceConfiguration.CloudinaryUtiMock
                .Setup(valid => valid.CreateMedia(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(cloudinaryCreate);

            var result = await _productOptionImageService.Create(productOptionImageDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("error when create ImageUrlBase", result.Message);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Create_Repository()
        {
            var productOptionImageDTO = new ProductOptionImageDTO(null, null, null, null, null, null);
            productOptionImageDTO.SetImageUrlBase64("imageUrlBase64");

            var cloudinaryCreate = new CloudinaryCreate();
            cloudinaryCreate.ImgUrl = "ImgUrl1";
            cloudinaryCreate.PublicId = "PublicId1";

            _productOptionImageServiceConfiguration.CloudinaryUtiMock
                .Setup(valid => valid.CreateMedia(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(cloudinaryCreate);

            _productOptionImageServiceConfiguration.ProductOptionImageRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<ProductOptionImage>()))
                .ThrowsAsync(new Exception("error Create ProductOptionImage"));

            var result = await _productOptionImageService.Create(productOptionImageDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("error Create ProductOptionImage", result.Message);
        }
    }
}
