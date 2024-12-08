using FluentValidation.Results;
using Moq;
using Shoope.Application.DTOs;
using Shoope.Application.Services;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Shoope.Domain.Enums;
using Shoope.Infra.Data.CloudinaryConfigClass;
using Xunit;

namespace Shoope.Application.ServicesTests.ProductsOfferFlashServiceTest
{
    public class ProductsOfferFlashServiceTest
    {
        private readonly ProductsOfferFlashServiceConfiguration _productsOfferFlashServiceConfiguration;
        private readonly ProductsOfferFlashService _productsOfferFlashService;

        public ProductsOfferFlashServiceTest()
        {
            _productsOfferFlashServiceConfiguration = new();
            var productsOfferFlashService = new ProductsOfferFlashService(
                _productsOfferFlashServiceConfiguration.ProductsOfferFlashRepositoryMock.Object,
                _productsOfferFlashServiceConfiguration.MapperMock.Object,
                _productsOfferFlashServiceConfiguration.UnitOfWorkMock.Object,
                _productsOfferFlashServiceConfiguration.CloudinaryUtiMock.Object,
                _productsOfferFlashServiceConfiguration.ProductsOfferFlashDTOValidatorMock.Object
                );

            _productsOfferFlashService = productsOfferFlashService;
        }

        [Fact]
        public async Task Should_GetAllProduct_Success()
        {
            _productsOfferFlashServiceConfiguration.ProductsOfferFlashRepositoryMock
                .Setup(rep => rep.GetAllProduct())
                .ReturnsAsync(new List<ProductsOfferFlash>());

            var result = await _productsOfferFlashService.GetAllProduct();
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_GetAllProduct()
        {
            _productsOfferFlashServiceConfiguration.ProductsOfferFlashRepositoryMock
                .Setup(rep => rep.GetAllProduct())
                .ThrowsAsync(new Exception("error GetAllProduct"));

            var result = await _productsOfferFlashService.GetAllProduct();

            Assert.False(result.IsSucess);
            Assert.Equal("error GetAllProduct", result.Message);
        }

        [Fact]
        public async Task Should_GetAllByTagProduct_Success()
        {
            _productsOfferFlashServiceConfiguration.ProductsOfferFlashRepositoryMock
                .Setup(rep => rep.GetAllByTagProduct(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<ProductsOfferFlash>());

            var result = await _productsOfferFlashService.GetAllByTagProduct("", "", 0, 0);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_GetAllByTagProduct()
        {
            _productsOfferFlashServiceConfiguration.ProductsOfferFlashRepositoryMock
                .Setup(rep => rep.GetAllByTagProduct(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("error GetAllByTagProduct"));

            var result = await _productsOfferFlashService.GetAllByTagProduct("", "", 0, 0);

            Assert.False(result.IsSucess);
            Assert.Equal("error GetAllByTagProduct", result.Message);
        }

        [Fact]
        public async Task Should_CreateAsync_Successfully()
        {
            var productsOfferFlashDTO = new ProductsOfferFlashDTO();
            productsOfferFlashDTO.SetTagProduct("Most_Popular");
            productsOfferFlashDTO.SetImgProduct("ImgProduct1");

            _productsOfferFlashServiceConfiguration.ProductsOfferFlashDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<ProductsOfferFlashDTO>()))
                .Returns(new ValidationResult());

            var cloudinaryCreate = new CloudinaryCreate();
            cloudinaryCreate.ImgUrl = "VideoUrl1";
            cloudinaryCreate.PublicId = "VideoPublicId1";

            _productsOfferFlashServiceConfiguration.CloudinaryUtiMock
                .Setup(rep => rep.CreateMedia(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(cloudinaryCreate);

            _productsOfferFlashServiceConfiguration.ProductsOfferFlashRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<ProductsOfferFlash>()))
                .ReturnsAsync(new ProductsOfferFlash());

            var result = await _productsOfferFlashService.CreateAsync(productsOfferFlashDTO);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_DTO_Is_Null_CreateAsync()
        {
            var result = await _productsOfferFlashService.CreateAsync(null);

            Assert.False(result.IsSucess);
            Assert.Equal("error DTO is null", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_When_ValidateDTO_CreateAsync()
        {
            var productsOfferFlashDTO = new ProductsOfferFlashDTO();

            _productsOfferFlashServiceConfiguration.ProductsOfferFlashDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<ProductsOfferFlashDTO>()))
                .Returns(new ValidationResult(new List<ValidationFailure>
                            {
                            new ValidationFailure("PropertyName", "Error message 1"),
                            }));

            var result = await _productsOfferFlashService.CreateAsync(productsOfferFlashDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("validation error check the information", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_TagProduct_Null_CreateAsync()
        {
            var productsOfferFlashDTO = new ProductsOfferFlashDTO();

            _productsOfferFlashServiceConfiguration.ProductsOfferFlashDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<ProductsOfferFlashDTO>()))
                .Returns(new ValidationResult());

            var result = await _productsOfferFlashService.CreateAsync(productsOfferFlashDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("error tag_product is null", result.Message);
        }

        [Fact]
        public async Task Should_Give_Error_IsValid_Tag_When_CreateAsync()
        {
            var productsOfferFlashDTO = new ProductsOfferFlashDTO();
            productsOfferFlashDTO.SetTagProduct("Most_Popular_Erro");

            _productsOfferFlashServiceConfiguration.ProductsOfferFlashDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<ProductsOfferFlashDTO>()))
                .Returns(new ValidationResult());

            var result = await _productsOfferFlashService.CreateAsync(productsOfferFlashDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("provided type is not valid", result.Message);
        }

        [Fact]
        public async Task Should_Give_Error_ImgProduct_Is_Null_CreateAsync()
        {
            var productsOfferFlashDTO = new ProductsOfferFlashDTO();
            productsOfferFlashDTO.SetTagProduct("Most_Popular");

            _productsOfferFlashServiceConfiguration.ProductsOfferFlashDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<ProductsOfferFlashDTO>()))
                .Returns(new ValidationResult());

            var result = await _productsOfferFlashService.CreateAsync(productsOfferFlashDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("Error img product must be informed", result.Message);
        }

        [Fact]
        public async Task Should_Give_Error_ImgUrl_Is_Null_CreateAsync()
        {
            var productsOfferFlashDTO = new ProductsOfferFlashDTO();
            productsOfferFlashDTO.SetTagProduct("Most_Popular");
            productsOfferFlashDTO.SetImgProduct("ImgProduct1");

            _productsOfferFlashServiceConfiguration.ProductsOfferFlashDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<ProductsOfferFlashDTO>()))
                .Returns(new ValidationResult());

            var cloudinaryCreate = new CloudinaryCreate();
            cloudinaryCreate.ImgUrl = null;
            cloudinaryCreate.PublicId = "VideoPublicId1";

            _productsOfferFlashServiceConfiguration.CloudinaryUtiMock
                .Setup(rep => rep.CreateMedia(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(cloudinaryCreate);

            _productsOfferFlashServiceConfiguration.ProductsOfferFlashRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<ProductsOfferFlash>()))
                .ReturnsAsync(new ProductsOfferFlash());

            var result = await _productsOfferFlashService.CreateAsync(productsOfferFlashDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("error when create ImgProduct", result.Message);
        }

        [Fact]
        public async Task Should_Give_Error_PublicId_Is_Null_CreateAsync()
        {
            var productsOfferFlashDTO = new ProductsOfferFlashDTO();
            productsOfferFlashDTO.SetTagProduct("Most_Popular");
            productsOfferFlashDTO.SetImgProduct("ImgProduct1");

            _productsOfferFlashServiceConfiguration.ProductsOfferFlashDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<ProductsOfferFlashDTO>()))
                .Returns(new ValidationResult());

            var cloudinaryCreate = new CloudinaryCreate();
            cloudinaryCreate.ImgUrl = "VideoUrl1";
            cloudinaryCreate.PublicId = null;

            _productsOfferFlashServiceConfiguration.CloudinaryUtiMock
                .Setup(rep => rep.CreateMedia(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(cloudinaryCreate);

            var result = await _productsOfferFlashService.CreateAsync(productsOfferFlashDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("error when create ImgProduct", result.Message);
        }

        [Fact]
        public async Task Should_Throw_Exception_CreateAsync()
        {
            var productsOfferFlashDTO = new ProductsOfferFlashDTO();
            productsOfferFlashDTO.SetTagProduct("Most_Popular");
            productsOfferFlashDTO.SetImgProduct("ImgProduct1");

            _productsOfferFlashServiceConfiguration.ProductsOfferFlashDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<ProductsOfferFlashDTO>()))
                .Returns(new ValidationResult());

            var cloudinaryCreate = new CloudinaryCreate();
            cloudinaryCreate.ImgUrl = "VideoUrl1";
            cloudinaryCreate.PublicId = "VideoPublicId1";

            _productsOfferFlashServiceConfiguration.CloudinaryUtiMock
                .Setup(rep => rep.CreateMedia(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(cloudinaryCreate);

            _productsOfferFlashServiceConfiguration.ProductsOfferFlashRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<ProductsOfferFlash>()))
                .ThrowsAsync(new Exception("error CreateAsync"));

            var result = await _productsOfferFlashService.CreateAsync(productsOfferFlashDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("error CreateAsync", result.Message);
        }
    }
}