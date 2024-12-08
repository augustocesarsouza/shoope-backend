using FluentValidation.Results;
using Moq;
using Shoope.Application.DTOs;
using Shoope.Application.Services;
using Shoope.Domain.Entities;
using Xunit;

namespace Shoope.Application.ServicesTests.AddressServiceTest
{
    public class AddressServiceTest
    {
        private readonly AddressServiceConfiguration _addressServiceConfiguration;
        private readonly AddressService _addressService;

        public AddressServiceTest()
        {
            _addressServiceConfiguration = new();
            var addressService = new AddressService(_addressServiceConfiguration.AddressRepositoryMock.Object,
                _addressServiceConfiguration.MapperMock.Object, _addressServiceConfiguration.UnitOfWorkMock.Object,
                _addressServiceConfiguration.AddressCreateDTOValidatorMock.Object, _addressServiceConfiguration.AddressUpdateDTOValidatorMock.Object,
                _addressServiceConfiguration.AddressUpdateOnlyDefaultDTOValidatorMock.Object);

            _addressService = addressService;
        }

        [Fact]
        public async Task Should_GetAddressById_Success()
        {
            Guid addressId = Guid.NewGuid();

            _addressServiceConfiguration.AddressRepositoryMock.Setup(rep => rep.GetAddressById(It.IsAny<Guid>()))
                .ReturnsAsync(new Address());

            var result = await _addressService.GetAddressById(addressId);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_GetAddressByUserId_Success()
        {
            Guid userId = Guid.NewGuid();

            var listAddress = new List<Address>();

            _addressServiceConfiguration.AddressRepositoryMock.Setup(rep => rep.GetAddressByUserId(It.IsAny<Guid>()))
                .ReturnsAsync(listAddress);

            var result = await _addressService.GetAddressByUserId(userId);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_CreateAsync_Success()
        {
            var addressId = Guid.NewGuid();
            AddressDTO addressDTO = new AddressDTO(addressId, "fullName", "phoneNumber", "cep", "stateCity", "neighborhood", "street", "numberHome", "complement",
                Guid.NewGuid(), null);

            //_movieServiceConfiguration.MovieDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<MovieDTO>())).Returns(new ValidationResult());

            _addressServiceConfiguration.AddressCreateDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<AddressDTO>()))
                .Returns(new ValidationResult());

            _addressServiceConfiguration.AddressRepositoryMock.Setup(rep => rep.VerifyIfUserAlreadyHaveAddress(It.IsAny<Guid>()))
                .ReturnsAsync(new Address());

            _addressServiceConfiguration.AddressRepositoryMock.Setup(rep => rep.CreateAsync(It.IsAny<Address>()))
                .ReturnsAsync(new Address());

            var result = await _addressService.CreateAsync(addressDTO);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_UpdateAddressUser_Success()
        {
            var addressId = Guid.NewGuid();
            AddressDTO addressDTO = new AddressDTO(addressId, "fullName", "phoneNumber", "cep", "stateCity", "neighborhood", "street", "numberHome", "complement",
                Guid.NewGuid(), null);

            //_movieServiceConfiguration.MovieDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<MovieDTO>())).Returns(new ValidationResult());

            _addressServiceConfiguration.AddressUpdateDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<AddressDTO>()))
                .Returns(new ValidationResult());

            _addressServiceConfiguration.AddressRepositoryMock.Setup(rep => rep.GetAddressById(It.IsAny<Guid>()))
                .ReturnsAsync(new Address());

            _addressServiceConfiguration.AddressRepositoryMock.Setup(rep => rep.UpdateAsync(It.IsAny<Address>()))
                .ReturnsAsync(new Address());

            var result = await _addressService.UpdateAddressUser(addressDTO);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_UpdateAsyncOnlyDefaultAddress_Success()
        {
            var addressId = Guid.NewGuid();
            AddressDTO addressDTO = new AddressDTO(addressId, "fullName", "phoneNumber", "cep", "stateCity", "neighborhood", "street", "numberHome", "complement",
                Guid.NewGuid(), null);

            //_movieServiceConfiguration.MovieDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<MovieDTO>())).Returns(new ValidationResult());

            _addressServiceConfiguration.AddressUpdateOnlyDefaultDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<AddressDTO>()))
                .Returns(new ValidationResult());

            _addressServiceConfiguration.AddressRepositoryMock.Setup(rep => rep.GetAddressById(It.IsAny<Guid>()))
                .ReturnsAsync(new Address());

            _addressServiceConfiguration.AddressRepositoryMock.Setup(rep => rep.GetAddressDefault())
                .ReturnsAsync(new Address());

            _addressServiceConfiguration.AddressRepositoryMock.Setup(rep => rep.UpdateAsync(It.IsAny<Address>()))
                .ReturnsAsync(new Address());

            var result = await _addressService.UpdateAsyncOnlyDefaultAddress(addressDTO);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Delete_Success()
        {
            var addressId = Guid.NewGuid();

            _addressServiceConfiguration.AddressUpdateOnlyDefaultDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<AddressDTO>()))
                .Returns(new ValidationResult());

            _addressServiceConfiguration.AddressRepositoryMock.Setup(rep => rep.GetAddressById(It.IsAny<Guid>()))
                .ReturnsAsync(new Address());

            _addressServiceConfiguration.AddressRepositoryMock.Setup(rep => rep.DeleteAsync(It.IsAny<Address>()))
                .ReturnsAsync(new Address());

            var result = await _addressService.Delete(addressId);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_GetAddressById()
        {
            Guid addressId = Guid.NewGuid();

            _addressServiceConfiguration.AddressRepositoryMock.Setup(rep => rep.GetAddressById(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("Erro ao buscar endereço"));

            var result = await _addressService.GetAddressById(addressId);

            Assert.False(result.IsSucess);
            Assert.Equal("Erro ao buscar endereço", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_GetAddressByUserId()
        {
            Guid userId = Guid.NewGuid();

            var listAddress = new List<Address>();

            _addressServiceConfiguration.AddressRepositoryMock.Setup(rep => rep.GetAddressByUserId(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("Error get address by userId"));

            var result = await _addressService.GetAddressByUserId(userId);

            Assert.False(result.IsSucess);
            Assert.Equal("Error get address by userId", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_CreateAsync_AddressDTO_Is_Null()
        {
            var result = await _addressService.CreateAsync(null);

            Assert.False(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_CreateAsync_Validate_AddressDTO()
        {
            var addressId = Guid.NewGuid();
            AddressDTO addressDTO = new AddressDTO(addressId, "fullName", "phoneNumber", "cep", "stateCity", "neighborhood", "street", "numberHome", "complement",
                Guid.NewGuid(), null);

            _addressServiceConfiguration.AddressCreateDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<AddressDTO>()))
                .Returns(new ValidationResult(new List<ValidationFailure>
                {
                new ValidationFailure("PropertyName", "Error message 1"),
                }));

            var result = await _addressService.CreateAsync(addressDTO);

            Assert.False(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_UpdateAddressUser_AddressDTO_Is_Null()
        {
            var result = await _addressService.UpdateAddressUser(null);

            Assert.False(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_UpdateAddressUser_Error_Validate_AddressDTO()
        {
            var addressId = Guid.NewGuid();
            AddressDTO addressDTO = new AddressDTO();

            _addressServiceConfiguration.AddressUpdateDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<AddressDTO>()))
                .Returns(new ValidationResult(new List<ValidationFailure>
                {
                new ValidationFailure("PropertyName", "Error message 1"),
                }));

            var result = await _addressService.UpdateAddressUser(addressDTO);

            Assert.False(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_UpdateAddressUser_GetAddressById_Is_Null()
        {
            var addressId = Guid.NewGuid();
            AddressDTO addressDTO = new AddressDTO(addressId, "fullName", "phoneNumber", "cep", "stateCity", "neighborhood", "street", "numberHome", "complement",
                Guid.NewGuid(), null);

            _addressServiceConfiguration.AddressUpdateDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<AddressDTO>()))
                .Returns(new ValidationResult());

            _addressServiceConfiguration.AddressRepositoryMock.Setup(rep => rep.GetAddressById(It.IsAny<Guid>()))
                .ReturnsAsync((Address?) null);

            var result = await _addressService.UpdateAddressUser(addressDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("error not found address", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_UpdateAsyncOnlyDefaultAddress_AddressDTO_Is_Null()
        {
            var result = await _addressService.UpdateAsyncOnlyDefaultAddress(null);

            Assert.False(result.IsSucess);
            Assert.Equal("Error DTO Null", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_UpdateAsyncOnlyDefaultAddressValidate_AddressDTO()
        {
            var addressId = Guid.NewGuid();
            AddressDTO addressDTO = new AddressDTO();

            _addressServiceConfiguration.AddressUpdateDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<AddressDTO>()))
                .Returns(new ValidationResult(new List<ValidationFailure>
                {
                new ValidationFailure("PropertyName", "Error message 1"),
                }));

            var result = await _addressService.UpdateAddressUser(addressDTO);

            Assert.False(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_UpdateAsyncOnlyDefaultAddress_GetAddressById_Is_Null()
        {
            var addressId = Guid.NewGuid();
            AddressDTO addressDTO = new AddressDTO(addressId, "fullName", "phoneNumber", "cep", "stateCity", "neighborhood", "street", "numberHome", "complement",
                Guid.NewGuid(), null);

            _addressServiceConfiguration.AddressUpdateDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<AddressDTO>()))
                .Returns(new ValidationResult());

            _addressServiceConfiguration.AddressRepositoryMock.Setup(rep => rep.GetAddressById(It.IsAny<Guid>()))
                .ReturnsAsync((Address?)null);

            var result = await _addressService.UpdateAddressUser(addressDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("error not found address", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_UpdateAsyncOnlyDefaultAddress_GetAddressDefault_Is_Null()
        {
            var addressId = Guid.NewGuid();
            AddressDTO addressDTO = new AddressDTO(addressId, "fullName", "phoneNumber", "cep", "stateCity", "neighborhood", "street", "numberHome", "complement",
                Guid.NewGuid(), null);

            _addressServiceConfiguration.AddressUpdateOnlyDefaultDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<AddressDTO>()))
                .Returns(new ValidationResult());

            _addressServiceConfiguration.AddressRepositoryMock.Setup(rep => rep.GetAddressById(It.IsAny<Guid>()))
                .ReturnsAsync(new Address());
            
            _addressServiceConfiguration.AddressRepositoryMock.Setup(rep => rep.GetAddressDefault())
                .ReturnsAsync((Address?)null);

            var result = await _addressService.UpdateAsyncOnlyDefaultAddress(addressDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("error it was not possible found address default", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_Delete()
        {
            var addressId = Guid.NewGuid();

            _addressServiceConfiguration.AddressRepositoryMock.Setup(rep => rep.GetAddressById(It.IsAny<Guid>()))
                .ReturnsAsync((Address?)null);

            var result = await _addressService.Delete(addressId);

            Assert.False(result.IsSucess);
            Assert.Equal("error not found address", result.Message);
        }
    }
}
