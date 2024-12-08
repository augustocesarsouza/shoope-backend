using Moq;
using Shoope.Application.DTOs;
using Shoope.Application.Services;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Shoope.Infra.Data.CloudinaryConfigClass;
using Xunit;

namespace Shoope.Application.ServicesTests.ProductHighlightServiceTest
{
    public class ProductHighlightServiceTest
    {
        private readonly ProductHighlightServiceConfiguration _productHighlightServiceConfiguration;
        private readonly ProductHighlightService _productHighlightService;

        public ProductHighlightServiceTest()
        {
            _productHighlightServiceConfiguration = new();
            var productHighlightService = new ProductHighlightService(
                _productHighlightServiceConfiguration.ProductHighlightRepositoryMock.Object,
                _productHighlightServiceConfiguration.MapperMock.Object,
                _productHighlightServiceConfiguration.UnitOfWorkMock.Object,
                _productHighlightServiceConfiguration.CloudinaryUtiMock.Object);

            _productHighlightService = productHighlightService;
        }

        [Fact]
        public async Task Should_GetProductHighlightById_Success()
        {
            Guid productHighlightId = Guid.NewGuid();

            _productHighlightServiceConfiguration.ProductHighlightRepositoryMock
                .Setup(rep => rep.GetProductHighlightById(It.IsAny<Guid>()))
                .ReturnsAsync(new ProductHighlight());

            var result = await _productHighlightService.GetProductHighlightById(productHighlightId);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_GetProductHighlightById()
        {
            Guid productHighlightId = Guid.NewGuid();

            _productHighlightServiceConfiguration.ProductHighlightRepositoryMock
                .Setup(rep => rep.GetProductHighlightById(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("Exception GetProductHighlightById"));

            var result = await _productHighlightService.GetProductHighlightById(productHighlightId);

            Assert.False(result.IsSucess);
            Assert.Equal("Exception GetProductHighlightById", result.Message);
        }

        [Fact]
        public async Task Should_GetAllProductHighlights_Success()
        {
            Guid productHighlightId = Guid.NewGuid();

            _productHighlightServiceConfiguration.ProductHighlightRepositoryMock
                .Setup(rep => rep.GetAllProductHighlight())
                .ReturnsAsync(new List<ProductHighlight>());

            var result = await _productHighlightService.GetAllProductHighlights();
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_GetAllProductHighlights()
        {
            Guid productHighlightId = Guid.NewGuid();

            _productHighlightServiceConfiguration.ProductHighlightRepositoryMock
                .Setup(rep => rep.GetAllProductHighlight())
                .ThrowsAsync(new Exception("Exception GetAllProductHighlight"));

            var result = await _productHighlightService.GetAllProductHighlights();

            Assert.False(result.IsSucess);
            Assert.Equal("Exception GetAllProductHighlight", result.Message);
        }

        [Fact]
        public async Task Should_Create_Successfully()
        {
            var productHighlightDTO = new ProductHighlightDTO(null, null, "seilaImgProduct", null, null, null);

            var cloudinaryCreate = new CloudinaryCreate();
            cloudinaryCreate.ImgUrl = "ImgUrl1";
            cloudinaryCreate.PublicId = "PublicId1";

            _productHighlightServiceConfiguration.CloudinaryUtiMock
                .Setup(valid => valid.CreateMedia(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(cloudinaryCreate);

            _productHighlightServiceConfiguration.ProductHighlightRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<ProductHighlight>()))
                .ReturnsAsync(new ProductHighlight());

            var result = await _productHighlightService.CreateAsync(productHighlightDTO);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Give_Error_DTO_Is_Null_Create()
        {
            var result = await _productHighlightService.CreateAsync(null);

            Assert.False(result.IsSucess);
            Assert.Equal("error DTO is null", result.Message);
        }

        [Fact]
        public async Task Should_Give_Error_ImgProduct_Is_Null_Create()
        {
            var productHighlightDTO = new ProductHighlightDTO(null, null, null, null, null, null);

            var result = await _productHighlightService.CreateAsync(productHighlightDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("Error ImgProduct must be informed", result.Message);
        }

        [Fact]
        public async Task Should_Give_Error_ImgUrl_Is_Null_Create()
        {
            var productHighlightDTO = new ProductHighlightDTO(null, null, "seilaImgProduct", null, null, null);

            var cloudinaryCreate = new CloudinaryCreate();
            cloudinaryCreate.ImgUrl = null;
            cloudinaryCreate.PublicId = "PublicId1";

            _productHighlightServiceConfiguration.CloudinaryUtiMock
                .Setup(valid => valid.CreateMedia(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(cloudinaryCreate);

            var result = await _productHighlightService.CreateAsync(productHighlightDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("error when create ImgProduct", result.Message);
        }

        [Fact]
        public async Task Should_Give_Error_PublicId_Is_Null_Create()
        {
            var productHighlightDTO = new ProductHighlightDTO(null, null, "seilaImgProduct", null, null, null);

            var cloudinaryCreate = new CloudinaryCreate();
            cloudinaryCreate.ImgUrl = "ImgUrl1";
            cloudinaryCreate.PublicId = null;

            _productHighlightServiceConfiguration.CloudinaryUtiMock
                .Setup(valid => valid.CreateMedia(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(cloudinaryCreate);

            var result = await _productHighlightService.CreateAsync(productHighlightDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("error when create ImgProduct", result.Message);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_CreateAsync_Repository()
        {
            var productHighlightDTO = new ProductHighlightDTO(null, null, "seilaImgProduct", null, null, null);

            var cloudinaryCreate = new CloudinaryCreate();
            cloudinaryCreate.ImgUrl = "ImgUrl1";
            cloudinaryCreate.PublicId = "PublicId1";

            _productHighlightServiceConfiguration.CloudinaryUtiMock
                .Setup(valid => valid.CreateMedia(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(cloudinaryCreate);

            _productHighlightServiceConfiguration.ProductHighlightRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<ProductHighlight>()))
                .ThrowsAsync(new Exception("error CreateAsync ProductHighlight"));

            var result = await _productHighlightService.CreateAsync(productHighlightDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("error CreateAsync ProductHighlight", result.Message);
        }
    }
}
