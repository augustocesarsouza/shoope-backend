using Moq;
using Shoope.Application.DTOs;
using Shoope.Application.Services;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Xunit;

namespace Shoope.Application.ServicesTests.UserCuponServiceTest
{
    public class UserCuponServiceTest
    {
        private readonly UserCuponServiceConfiguration _userCuponServiceConfiguration;
        private readonly UserCuponService _userCuponService;

        public UserCuponServiceTest()
        {
            _userCuponServiceConfiguration = new();
            var userCuponService = new UserCuponService(
                _userCuponServiceConfiguration.UserCuponRepositoryMock.Object,
                _userCuponServiceConfiguration.MapperMock.Object,
                _userCuponServiceConfiguration.UnitOfWorkMock.Object
                );

            _userCuponService = userCuponService;
        }

        [Fact]
        public async Task Should_GetAllCuponByUserId_Success()
        {
            var userId = Guid.NewGuid();

            _userCuponServiceConfiguration.UserCuponRepositoryMock
                .Setup(rep => rep.GetAllCuponByUserId(It.IsAny<Guid>()))
                .ReturnsAsync(new List<UserCupon>());

            var result = await _userCuponService.GetAllCuponByUserId(userId);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_GetAllCuponByUserId()
        {
            var userId = Guid.NewGuid();

            _userCuponServiceConfiguration.UserCuponRepositoryMock
                .Setup(rep => rep.GetAllCuponByUserId(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("error GetAllCuponByUserId"));

            var result = await _userCuponService.GetAllCuponByUserId(userId);

            Assert.False(result.IsSucess);
            Assert.Equal("error GetAllCuponByUserId", result.Message);
        }

        [Fact]
        public async Task Should_CreateAsync_Success()
        {
            var userCuponDTO = new UserCuponDTO(null, null, null, null, null);

            _userCuponServiceConfiguration.UserCuponRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<UserCupon>()))
                .ReturnsAsync(new UserCupon());

            var result = await _userCuponService.Create(userCuponDTO);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Give_Error_DTO_Is_Null()
        {
            var result = await _userCuponService.Create(null);

            Assert.False(result.IsSucess);
            Assert.Equal("DTO Is Null", result.Message);
        }

        [Fact]
        public async Task Should_Throw_Exception_CreateAsync()
        {
            var userCuponDTO = new UserCuponDTO(null, null, null, null, null);

            _userCuponServiceConfiguration.UserCuponRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<UserCupon>()))
                .ThrowsAsync(new Exception("error CreateAsync"));

            var result = await _userCuponService.Create(userCuponDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("error CreateAsync", result.Message);
        }
    }
}
