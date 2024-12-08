using FluentValidation.Results;
using Moq;
using Shoope.Application.DTOs;
using Shoope.Application.Services;
using Shoope.Domain.Entities;
using Xunit;
//.ReturnsAsync((Movie?)null);

namespace Shoope.Application.ServicesTests.CuponTests
{
    public class CuponServiceTest
    {
        private readonly CuponServiceConfiguration _cuponServiceConfiguration;
        private readonly CuponService _cuponService;

        public CuponServiceTest()
        {
            _cuponServiceConfiguration = new();
            var cuponService = new CuponService(_cuponServiceConfiguration.CuponRepositoryMock.Object,
                _cuponServiceConfiguration.MapperMock.Object, 
                _cuponServiceConfiguration.UnitOfWorkMock.Object,
                _cuponServiceConfiguration.CuponCreateDTOValidatorMock.Object);

            _cuponService = cuponService;
        }

        [Fact]
        public async Task Should_GetCuponById_Success()
        {
            Guid cuponId = Guid.NewGuid();

            _cuponServiceConfiguration.CuponRepositoryMock
                .Setup(rep => rep.GetCuponById(It.IsAny<Guid>()))
                .ReturnsAsync(new Cupon());

            var result = await _cuponService.GetCuponById(cuponId);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_GetCuponById()
        {
            Guid cuponId = Guid.NewGuid();

            _cuponServiceConfiguration.CuponRepositoryMock
                .Setup(rep => rep.GetCuponById(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("error get cupon by id"));

            var result = await _cuponService.GetCuponById(cuponId);

            Assert.False(result.IsSucess);
            Assert.Equal("error get cupon by id", result.Message);
        }

        [Fact]
        public async Task Should_CreateAsync_Success()
        {
            var cuponId = Guid.NewGuid();
            CuponDTO cuponDTO = new CuponDTO(cuponId, "Para você", "Frete Grátis", "Sem valor mínimo",
                null, "05/10/1999", 2, 2, "sdvcdsv", "ascascsaAlt");

            _cuponServiceConfiguration.CuponCreateDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<CuponDTO>()))
                .Returns(new ValidationResult());

            _cuponServiceConfiguration.CuponRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<Cupon>()))
                .ReturnsAsync(new Cupon());

            var result = await _cuponService.CreateAsync(cuponDTO);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Throw_Error_DTO_Is_Null()
        {
            var result = await _cuponService.CreateAsync(null);

            Assert.False(result.IsSucess);
            Assert.Equal("error dto is null", result.Message);
        }

        [Fact]
        public async Task Should_Throw_Error_When_Validate_DTO()
        {
            var cuponId = Guid.NewGuid();
            CuponDTO cuponDTO = new CuponDTO(cuponId, "Para você", "Frete Grátis", "Sem valor mínimo",
                null, "05/10/1999", 2, 2, "sdvcdsv", "ascascsaAlt");

            _cuponServiceConfiguration.CuponCreateDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<CuponDTO>()))
                            .Returns(new ValidationResult(new List<ValidationFailure>
                            {
                            new ValidationFailure("PropertyName", "Error message 1"),
                            }));

            var result = await _cuponService.CreateAsync(cuponDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("validation error check the information", result.Message);
        }

        [Fact]
        public async Task Should_Throw_Error_DateValidateCuponString_Is_Null()
        {
            var cuponId = Guid.NewGuid();
            CuponDTO cuponDTO = new CuponDTO(cuponId, "Para você", "Frete Grátis", "Sem valor mínimo",
                null, null, 2, 2, "sdvcdsv", "ascascsaAlt");

            _cuponServiceConfiguration.CuponCreateDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<CuponDTO>()))
                .Returns(new ValidationResult());

            var result = await _cuponService.CreateAsync(cuponDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("DateValidateCuponString is null", result.Message);
        }

        [Fact]
        public async Task Should_Throw_Error_DateValidateCuponString_Does_Not_Pass_Pattern_Validation()
        {
            var cuponId = Guid.NewGuid();
            CuponDTO cuponDTO = new CuponDTO(cuponId, "Para você", "Frete Grátis", "Sem valor mínimo",
                null, "05/10", 2, 2, "sdvcdsv", "ascascsaAlt");

            _cuponServiceConfiguration.CuponCreateDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<CuponDTO>()))
                .Returns(new ValidationResult());

            var result = await _cuponService.CreateAsync(cuponDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("Error date informed is invalid DD/MM/YYYY", result.Message);
        }
    }
}
