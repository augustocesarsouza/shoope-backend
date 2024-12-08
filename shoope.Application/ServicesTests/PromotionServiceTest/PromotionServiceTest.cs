using Moq;
using Shoope.Application.Services;
using Shoope.Domain.Entities;
using Xunit;

namespace Shoope.Application.ServicesTests.PromotionServiceTest
{
    public class PromotionServiceTest
    {
        private readonly PromotionServiceConfiguration _promotionServiceConfiguration;
        private readonly PromotionService _promotionService;

        public PromotionServiceTest()
        {
            _promotionServiceConfiguration = new();
            var promotionService = new PromotionService(
                _promotionServiceConfiguration.PromotionRepositoryMock.Object,
                _promotionServiceConfiguration.PromotionUserServiceMock.Object,
                _promotionServiceConfiguration.MapperMock.Object,
                _promotionServiceConfiguration.UnitOfWorkMock.Object,
                _promotionServiceConfiguration.CloudinaryUtiMock.Object,
                _promotionServiceConfiguration.PromotionCreateDTOValidatorMock.Object,
                _promotionServiceConfiguration.PromotionCreateDTOIfPromotionNumber2ValidatorMock.Object
                );

            _promotionService = promotionService;
        }

        [Fact]
        public async Task Should_GetById_Success()
        {
            Guid promotionId = Guid.NewGuid();

            _promotionServiceConfiguration.PromotionRepositoryMock
                .Setup(rep => rep.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(new Promotion());

            var result = await _promotionService.GetById(promotionId);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_GetById()
        {
            Guid promotionId = Guid.NewGuid();

            _promotionServiceConfiguration.PromotionRepositoryMock
                .Setup(rep => rep.GetById(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("error GetById"));

            var result = await _promotionService.GetById(promotionId);

            Assert.False(result.IsSucess);
            Assert.Equal("error GetById", result.Message);
        }
    }
}
