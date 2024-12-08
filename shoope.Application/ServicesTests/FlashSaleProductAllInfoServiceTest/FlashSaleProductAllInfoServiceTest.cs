using Moq;
using Shoope.Application.DTOs;
using Shoope.Application.Services;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Xunit;

namespace Shoope.Application.ServicesTests.FlashSaleProductAllInfoServiceTest
{
    public class FlashSaleProductAllInfoServiceTest
    {
        private readonly FlashSaleProductAllInfoServiceConfiguration _flashSaleProductAllInfoServiceConfiguration;
        private readonly FlashSaleProductAllInfoService _flashSaleProductAllInfoService;

        public FlashSaleProductAllInfoServiceTest()
        {
            _flashSaleProductAllInfoServiceConfiguration = new();
            var flashSaleProductAllInfoService = new FlashSaleProductAllInfoService(
                _flashSaleProductAllInfoServiceConfiguration.FlashSaleProductAllInfoRepositoryMock.Object,
                _flashSaleProductAllInfoServiceConfiguration.MapperMock.Object, 
                _flashSaleProductAllInfoServiceConfiguration.UnitOfWorkMock.Object);

            _flashSaleProductAllInfoService = flashSaleProductAllInfoService;
        }

        [Fact]
        public async Task Should_GetFlashSaleProductByProductFlashSaleId_Success()
        {
            Guid productFlashSaleId = Guid.NewGuid();

            _flashSaleProductAllInfoServiceConfiguration.FlashSaleProductAllInfoRepositoryMock
                .Setup(rep => rep.GetFlashSaleProductByProductFlashSaleId(It.IsAny<Guid>()))
                .ReturnsAsync(new FlashSaleProductAllInfo());

            var result = await _flashSaleProductAllInfoService.GetFlashSaleProductByProductFlashSaleId(productFlashSaleId);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_GetFlashSaleProductByProductFlashSaleId()
        {
            Guid cuponId = Guid.NewGuid();

            _flashSaleProductAllInfoServiceConfiguration.FlashSaleProductAllInfoRepositoryMock
                .Setup(rep => rep.GetFlashSaleProductByProductFlashSaleId(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("error get flashSaleProduct by id"));

            var result = await _flashSaleProductAllInfoService.GetFlashSaleProductByProductFlashSaleId(cuponId);

            Assert.False(result.IsSucess);
            Assert.Equal("error get flashSaleProduct by id", result.Message);
        }

        [Fact]
        public async Task Should_CreateAsync_Success()
        {
            FlashSaleProductAllInfoDTO flashSaleProductAllInfoDTO = new FlashSaleProductAllInfoDTO();

            _flashSaleProductAllInfoServiceConfiguration.FlashSaleProductAllInfoRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<FlashSaleProductAllInfo>()))
                .ReturnsAsync(new FlashSaleProductAllInfo());


            var result = await _flashSaleProductAllInfoService.CreateAsync(flashSaleProductAllInfoDTO);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Throw_Error_When_CreateAsync_DTO_Is_Null()
        {
            var result = await _flashSaleProductAllInfoService.CreateAsync(null);
            Assert.False(result.IsSucess);
            Assert.Equal("error DTO is null", result.Message);
        }
    }
}
