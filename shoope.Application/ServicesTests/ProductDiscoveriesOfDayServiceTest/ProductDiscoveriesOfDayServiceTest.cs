using FluentValidation.Results;
using Moq;
using Shoope.Application.DTOs;
using Shoope.Application.Services;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Shoope.Infra.Data.CloudinaryConfigClass;
using Xunit;

namespace Shoope.Application.ServicesTests.ProductDiscoveriesOfDayServiceTest
{
    public class ProductDiscoveriesOfDayServiceTest
    {
        private readonly ProductDiscoveriesOfDayServiceConfiguration _productDiscoveriesOfDayServiceConfiguration;
        private readonly ProductDiscoveriesOfDayService _productDiscoveriesOfDayService;

        public ProductDiscoveriesOfDayServiceTest()
        {
            _productDiscoveriesOfDayServiceConfiguration = new();
            var productDiscoveriesOfDayService = new ProductDiscoveriesOfDayService(
                _productDiscoveriesOfDayServiceConfiguration.ProductDiscoveriesOfDayRepositoryMock.Object,
                _productDiscoveriesOfDayServiceConfiguration.MapperMock.Object,
                _productDiscoveriesOfDayServiceConfiguration.UnitOfWorkMock.Object,
                _productDiscoveriesOfDayServiceConfiguration.CloudinaryUtiMock.Object,
                _productDiscoveriesOfDayServiceConfiguration.ProductDiscoveriesOfDayCreateDTOValidatorMock.Object
                );

            _productDiscoveriesOfDayService = productDiscoveriesOfDayService;
        }

        [Fact]
        public async Task Should_GetProductDiscoveriesOfDayById_Success()
        {
            Guid cuponId = Guid.NewGuid();

            _productDiscoveriesOfDayServiceConfiguration.ProductDiscoveriesOfDayRepositoryMock
                .Setup(rep => rep.GetProductDiscoveriesOfDayById(It.IsAny<Guid>()))
                .ReturnsAsync(new ProductDiscoveriesOfDay());

            var result = await _productDiscoveriesOfDayService.GetProductDiscoveriesOfDayById(cuponId);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_GetProductDiscoveriesOfDayById()
        {
            Guid cuponId = Guid.NewGuid();

            _productDiscoveriesOfDayServiceConfiguration.ProductDiscoveriesOfDayRepositoryMock
                .Setup(rep => rep.GetProductDiscoveriesOfDayById(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("error get productDiscoveries by id"));

            var result = await _productDiscoveriesOfDayService.GetProductDiscoveriesOfDayById(cuponId);

            Assert.False(result.IsSucess);
            Assert.Equal("error get productDiscoveries by id", result.Message);
        }

        [Fact]
        public async Task Should_GetAllProductDiscoveriesOfDays_Success()
        {
            _productDiscoveriesOfDayServiceConfiguration.ProductDiscoveriesOfDayRepositoryMock
                .Setup(rep => rep.GetAllProductDiscoveriesOfDay())
                .ReturnsAsync(new List<ProductDiscoveriesOfDay>());

            var result = await _productDiscoveriesOfDayService.GetAllProductDiscoveriesOfDays();
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_GetAllProductDiscoveriesOfDays()
        {
            _productDiscoveriesOfDayServiceConfiguration.ProductDiscoveriesOfDayRepositoryMock
                .Setup(rep => rep.GetAllProductDiscoveriesOfDay())
                .ThrowsAsync(new Exception("error get GetAllProductDiscoveries by id"));

            var result = await _productDiscoveriesOfDayService.GetAllProductDiscoveriesOfDays();

            Assert.False(result.IsSucess);
            Assert.Equal("error get GetAllProductDiscoveries by id", result.Message);
        }

        [Fact]
        public async Task Should_CreateAsync_Success()
        {
            ProductDiscoveriesOfDayDTO productDiscoveriesOfDayDTO = new ProductDiscoveriesOfDayDTO();
            productDiscoveriesOfDayDTO.SetImgProduct("seila");

            _productDiscoveriesOfDayServiceConfiguration.ProductDiscoveriesOfDayCreateDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<ProductDiscoveriesOfDayDTO>())).Returns(new ValidationResult());

            var cloudinary = new CloudinaryCreate
            {
                ImgUrl = "http://res.cloudinary.com/dyqsqg7pk/image/upload/pd6iwh7kprpcrauaiao1",
                PublicId = "ascascascas"
            };

            _productDiscoveriesOfDayServiceConfiguration.CloudinaryUtiMock
                .Setup(cloud => cloud.CreateMedia(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(cloudinary);

            _productDiscoveriesOfDayServiceConfiguration.ProductDiscoveriesOfDayRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<ProductDiscoveriesOfDay>()))
                .ReturnsAsync(new ProductDiscoveriesOfDay());

            var result = await _productDiscoveriesOfDayService.CreateAsync(productDiscoveriesOfDayDTO);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Give_Error_DTO_Is_Null_When_Create()
        {
            var result = await _productDiscoveriesOfDayService.CreateAsync(null);
            Assert.False(result.IsSucess);
            Assert.Equal("error DTO is null", result.Message);
        }

        [Fact]
        public async Task Should_Give_Error_When_Validate_DTO_Create()
        {
            ProductDiscoveriesOfDayDTO productDiscoveriesOfDayDTO = new ProductDiscoveriesOfDayDTO();
            productDiscoveriesOfDayDTO.SetImgProduct("seila");

            _productDiscoveriesOfDayServiceConfiguration.ProductDiscoveriesOfDayCreateDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<ProductDiscoveriesOfDayDTO>()))
                 .Returns(new ValidationResult(new List<ValidationFailure>
                            {
                            new ValidationFailure("PropertyName", "Error message 1"),
                            }));

            var result = await _productDiscoveriesOfDayService.CreateAsync(productDiscoveriesOfDayDTO);
            Assert.False(result.IsSucess);
            Assert.Equal("validation error check the information", result.Message);
        }

        [Fact]
        public async Task Should_Error_Create_Img_CreateAsync()
        {
            ProductDiscoveriesOfDayDTO productDiscoveriesOfDayDTO = new ProductDiscoveriesOfDayDTO();
            productDiscoveriesOfDayDTO.SetImgProduct("seila");

            _productDiscoveriesOfDayServiceConfiguration.ProductDiscoveriesOfDayCreateDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<ProductDiscoveriesOfDayDTO>())).Returns(new ValidationResult());

            var cloudinary = new CloudinaryCreate
            {
                ImgUrl = null,
                PublicId = null
            };

            _productDiscoveriesOfDayServiceConfiguration.CloudinaryUtiMock
                .Setup(cloud => cloud.CreateMedia(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(cloudinary);

            var result = await _productDiscoveriesOfDayService.CreateAsync(productDiscoveriesOfDayDTO);
            Assert.False(result.IsSucess);
            Assert.Equal("error when create ImgProduct", result.Message);
        }

        [Fact]
        public async Task Should_Throw_Exception_CreateAsync()
        {
            ProductDiscoveriesOfDayDTO productDiscoveriesOfDayDTO = new ProductDiscoveriesOfDayDTO();
            productDiscoveriesOfDayDTO.SetImgProduct("seila");

            _productDiscoveriesOfDayServiceConfiguration.ProductDiscoveriesOfDayCreateDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<ProductDiscoveriesOfDayDTO>())).Returns(new ValidationResult());

            var cloudinary = new CloudinaryCreate
            {
                ImgUrl = "http://res.cloudinary.com/dyqsqg7pk/image/upload/pd6iwh7kprpcrauaiao1",
                PublicId = "ascascascas"
            };

            _productDiscoveriesOfDayServiceConfiguration.CloudinaryUtiMock
                .Setup(cloud => cloud.CreateMedia(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(cloudinary);

            _productDiscoveriesOfDayServiceConfiguration.ProductDiscoveriesOfDayRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<ProductDiscoveriesOfDay>()))
                 .ThrowsAsync(new Exception("error when create ProductDiscoveriesOfDay"));

            var result = await _productDiscoveriesOfDayService.CreateAsync(productDiscoveriesOfDayDTO);
            Assert.False(result.IsSucess);
            Assert.Equal("error when create ProductDiscoveriesOfDay", result.Message);
        }
    }
}
