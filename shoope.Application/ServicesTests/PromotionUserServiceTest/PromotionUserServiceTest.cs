using FluentValidation.Results;
using Moq;
using Shoope.Application.DTOs;
using Shoope.Application.Services;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using System;
using Xunit;

namespace Shoope.Application.ServicesTests.PromotionUserServiceTest
{
    public class PromotionUserServiceTest
    {
        private readonly PromotionUserServiceConfiguration _promotionUserServiceConfiguration;
        private readonly PromotionUserService _promotionUserService;

        public PromotionUserServiceTest()
        {
            _promotionUserServiceConfiguration = new();
            var promotionUserService = new PromotionUserService(
                _promotionUserServiceConfiguration.PromotionUserRepositoryMock.Object,
                _promotionUserServiceConfiguration.MapperMock.Object,
                _promotionUserServiceConfiguration.UnitOfWorkMock.Object,
                _promotionUserServiceConfiguration.PromotionUserCreateDTOValidatorMock.Object);

            _promotionUserService = promotionUserService;
        }

        [Fact]
        public async Task Should_GetByUserIdAll_Success()
        {
            Guid guidId = Guid.NewGuid();

            _promotionUserServiceConfiguration.PromotionUserRepositoryMock
                .Setup(rep => rep.GetByUserIdAll(It.IsAny<Guid>()))
                .ReturnsAsync(new List<PromotionUser>());

            var result = await _promotionUserService.GetByUserIdAll(guidId);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_GetByUserIdAll()
        {
            Guid guidId = Guid.NewGuid();

            _promotionUserServiceConfiguration.PromotionUserRepositoryMock
                .Setup(rep => rep.GetByUserIdAll(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("error GetByUserIdAll"));

            var result = await _promotionUserService.GetByUserIdAll(guidId);

            Assert.False(result.IsSucess);
            Assert.Equal("error GetByUserIdAll", result.Message);
        }

        [Fact]
        public async Task Should_CreateAsync_Success()
        {
            var promotionUserDTO = new PromotionUserDTO(null, Guid.NewGuid(), null, Guid.NewGuid(), null);

            _promotionUserServiceConfiguration.PromotionUserCreateDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<PromotionUserDTO>()))
                .Returns(new ValidationResult());

            _promotionUserServiceConfiguration.PromotionUserRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<PromotionUser>()))
                .ReturnsAsync(new PromotionUser());

            var result = await _promotionUserService.Create(promotionUserDTO);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Give_Error_DTO_Is_Null()
        {
            var result = await _promotionUserService.Create(null);

            Assert.False(result.IsSucess);
            Assert.Equal("DTO Is Null", result.Message);
        }

        [Fact]
        public async Task Should_Give_Error_DTO_Is_Invalid()
        {
            var promotionUserDTO = new PromotionUserDTO(null, Guid.NewGuid(), null, Guid.NewGuid(), null);

            _promotionUserServiceConfiguration.PromotionUserCreateDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<PromotionUserDTO>()))
                .Returns(new ValidationResult(new List<ValidationFailure>
                            {
                            new ValidationFailure("PropertyName", "Error message 1"),
                            }));

            var result = await _promotionUserService.Create(promotionUserDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("validation error check the information", result.Message);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Create()
        {
            var promotionUserDTO = new PromotionUserDTO(null, Guid.NewGuid(), null, Guid.NewGuid(), null);

            _promotionUserServiceConfiguration.PromotionUserCreateDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<PromotionUserDTO>()))
                .Returns(new ValidationResult());

            _promotionUserServiceConfiguration.PromotionUserRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<PromotionUser>()))
                .ThrowsAsync(new Exception("error CreateAsync"));

            var result = await _promotionUserService.Create(promotionUserDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("error CreateAsync", result.Message);
        }
    }
}
