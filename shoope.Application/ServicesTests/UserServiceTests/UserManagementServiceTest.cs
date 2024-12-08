using Shoope.Application.Services;
using Shoope.Domain.Entities;
using Xunit;
using Moq;
using Shoope.Application.DTOs;
using FluentValidation.Results;

namespace Shoope.Application.ServicesTests.UserServiceTests
{
    public class UserManagementServiceTest
    {
        private readonly UserManagementServiceConfiguration _userManagementServiceConfiguration;
        private readonly UserManagementService _userManagementService;

        public UserManagementServiceTest()
        {
            _userManagementServiceConfiguration = new();
            var userManagementService = new UserManagementService(_userManagementServiceConfiguration.UserRepositoryMock.Object,
                _userManagementServiceConfiguration.MapperMock.Object, _userManagementServiceConfiguration.UnitOfWorkMock.Object,
                _userManagementServiceConfiguration.UserCreateDTOValidatorMock.Object,
                _userManagementServiceConfiguration.UserCreateAccountFunctionMock.Object, _userManagementServiceConfiguration.CloudinaryUtiMock.Object);
            _userManagementService = userManagementService;
        }

        [Fact]
        public async Task Should_GetAddressById_Success()
        {
            string phone = "";

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserByPhone(It.IsAny<string>()))
                .ReturnsAsync(new User());

            var result = await _userManagementService.CheckEmailAlreadyExists(phone);

            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_CreateAsync_Success()
        {
            UserDTO userDTO = new UserDTO("augusto", null, "password123");

            _userManagementServiceConfiguration.UserCreateDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<UserDTO>()))
                .Returns(new ValidationResult());

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(rep => rep.CreateAsync(It.IsAny<User>()))
                .ReturnsAsync(new User());

            var result = await _userManagementService.Create(userDTO);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_UpdateUserAll_Success()
        {
            var userId = Guid.NewGuid();
            var userUpdateAllDTO = new UserUpdateAllDTO(userId.ToString(), "augusto", "augusto@gmail.com", "M", "(+55) 23 32323 3223", 
                "23232322323", "", null);

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(valid => valid.GetUserById(It.IsAny<Guid>()))
                .ReturnsAsync(new User());

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(rep => rep.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync(new User());

            var result = await _userManagementService.UpdateUserAll(userUpdateAllDTO);

            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_UpdateUser_Success()
        {
            var userId = Guid.NewGuid();
            var userUpdateAllDTO = new UserUpdateFillDTO(userId.ToString(), "23232322323", "05/10/1999");

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(valid => valid.GetUserById(It.IsAny<Guid>()))
                .ReturnsAsync(new User());

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(rep => rep.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync(new User());

            var result = await _userManagementService.UpdateUser(userUpdateAllDTO);

            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Throw_Error_Exception_GetAddressById()
        {
            string phone = "";

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserByPhone(It.IsAny<string>()))
                .ThrowsAsync(new Exception("Error get GetUserByPhone by userId"));

            var result = await _userManagementService.CheckEmailAlreadyExists(phone);

            Assert.False(result.IsSucess);
            Assert.Equal("Error get GetUserByPhone by userId", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_DTO_Is_Null_CreateAsync()
        {
            var result = await _userManagementService.Create(null);

            Assert.False(result.IsSucess);
            Assert.Equal("userDTO is null", result.Message);
        }

        [Fact]
        public async Task Should_Return_Password_Is_Null_CreateAsync()
        {
            UserDTO userDTO = new UserDTO("augusto", null, null);

            var result = await _userManagementService.Create(userDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("Password informed is null", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_Validate_DTO_CreateAsync()
        {
            UserDTO userDTO = new UserDTO("augusto", null, "password123");

            _userManagementServiceConfiguration.UserCreateDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<UserDTO>()))
                .Returns(new ValidationResult(new List<ValidationFailure>
                {
                new ValidationFailure("PropertyName", "Error message 1"),
                }));

            var result = await _userManagementService.Create(userDTO);

            Assert.False(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_Create_User_CreateAsync()
        {
            UserDTO userDTO = new UserDTO("augusto", null, "password123");

            _userManagementServiceConfiguration.UserCreateDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<UserDTO>()))
                .Returns(new ValidationResult());

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(rep => rep.CreateAsync(It.IsAny<User>()))
                .ReturnsAsync((User?)null);

            var result = await _userManagementService.Create(userDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("error when create user null value", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_DTO_Null_UpdateUserAll()
        {
            var result = await _userManagementService.UpdateUserAll(null);

            Assert.False(result.IsSucess);
            Assert.Equal("Error DTO it is null", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_UserId_ItIs_Null_UpdateUserAll()
        {
            var userId = Guid.NewGuid();
            var userUpdateAllDTO = new UserUpdateAllDTO(null, "augusto", "augusto@gmail.com", "M", "(+55) 23 32323 3223",
                "23232322323", "", null);

            var result = await _userManagementService.UpdateUserAll(userUpdateAllDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("Error UserId it is null", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_User_Not_Found_UpdateUserAll()
        {
            var userId = Guid.NewGuid();
            var userUpdateAllDTO = new UserUpdateAllDTO(userId.ToString(), "augusto", "augusto@gmail.com", "M", "(+55) 23 32323 3223",
                "23232322323", "", null);

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(valid => valid.GetUserById(It.IsAny<Guid>()))
                .ReturnsAsync((User?)null);

            var result = await _userManagementService.UpdateUserAll(userUpdateAllDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("Error user Not Found", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_UserUpdate_ItIs_Null_UpdateUserAll()
        {
            var userId = Guid.NewGuid();
            var userUpdateAllDTO = new UserUpdateAllDTO(userId.ToString(), "augusto", "augusto@gmail.com", "M", "(+55) 23 32323 3223",
                "23232322323", "", null);

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(valid => valid.GetUserById(It.IsAny<Guid>()))
                .ReturnsAsync(new User());

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(rep => rep.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync((User?) null);

            var result = await _userManagementService.UpdateUserAll(userUpdateAllDTO);
            
            Assert.False(result.IsSucess);
            Assert.Equal("Error userUpdate it is null", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_DTO_ItIs_Null_UpdateUser()
        {
            var result = await _userManagementService.UpdateUser(null);

            Assert.False(result.IsSucess);
            Assert.Equal("Error DTO it is null", result.Message);
        }

        //if (userUpdateFillDTO.Cpf == null)
        //        return ResultService.Fail<UserDTO>("Error Cpf null");

        //    if (userUpdateFillDTO.UserId == null)
        //        return ResultService.Fail<UserDTO>("Error UserId null");

        //    if (userUpdateFillDTO.BirthDate == null)
        //        return ResultService.Fail<UserDTO>("Error BirthDate null");

        [Fact]
        public async Task Should_Return_Error_Cpf_ItIs_Null_UpdateUser()
        {
            var userId = Guid.NewGuid();
            var userUpdateAllDTO = new UserUpdateFillDTO(userId.ToString(), null, "05/10/1999");

            var result = await _userManagementService.UpdateUser(userUpdateAllDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("Error Cpf null", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_UserId_ItIs_Null_UpdateUser()
        {
            var userUpdateAllDTO = new UserUpdateFillDTO(null, "23232322323", "05/10/1999");

            var result = await _userManagementService.UpdateUser(userUpdateAllDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("Error UserId null", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_BirthDate_ItIs_Null_UpdateUser()
        {
            var userId = Guid.NewGuid();
            var userUpdateAllDTO = new UserUpdateFillDTO(userId.ToString(), "23232322323", null);

            var result = await _userManagementService.UpdateUser(userUpdateAllDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("Error BirthDate null", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_BirthDate__Length_ItIs_Wrong_UpdateUser()
        {
            var userId = Guid.NewGuid();
            var userUpdateAllDTO = new UserUpdateFillDTO(userId.ToString(), "23232322", "05/10/1999");

            var result = await _userManagementService.UpdateUser(userUpdateAllDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("Is not a Cpf Valid", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_GetUserById_Not_Found_User_UpdateUser()
        {
            var userId = Guid.NewGuid();
            var userUpdateAllDTO = new UserUpdateFillDTO(userId.ToString(), "23232322323", "05/10/1999");

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(valid => valid.GetUserById(It.IsAny<Guid>()))
                .ReturnsAsync((User?)null);

            var result = await _userManagementService.UpdateUser(userUpdateAllDTO);
            
            Assert.False(result.IsSucess);
            Assert.Equal("Error user Not Found", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_UpdateUser_ItIs_Null_UpdateUser()
        {
            var userId = Guid.NewGuid();
            var userUpdateAllDTO = new UserUpdateFillDTO(userId.ToString(), "23232322323", "05/10/1999");

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(valid => valid.GetUserById(It.IsAny<Guid>()))
                .ReturnsAsync(new User());

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(rep => rep.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync((User?)null);

            var result = await _userManagementService.UpdateUser(userUpdateAllDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("Error userUpdate it is null", result.Message);
        }
    }
}
