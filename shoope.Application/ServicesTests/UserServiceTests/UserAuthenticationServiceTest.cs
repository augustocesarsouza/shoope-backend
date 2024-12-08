using FluentValidation.Results;
using Moq;
using Shoope.Application.DTOs;
using Shoope.Application.Services;
using Shoope.Domain.Authentication;
using Shoope.Domain.Entities;
using Shoope.Domain.InfoErrors;
using Xunit;

namespace Shoope.Application.ServicesTests.UserServiceTests
{
    public class UserAuthenticationServiceTest
    {
        private readonly UserAuthenticationServiceConfiguration _userAuthenticationServiceConfiguration;
        private readonly UserAuthenticationService _userAuthenticationService;

        public UserAuthenticationServiceTest()
        {
            _userAuthenticationServiceConfiguration = new();
            var userAuthenticationService = new UserAuthenticationService(_userAuthenticationServiceConfiguration.UserRepositoryMock.Object,
                _userAuthenticationServiceConfiguration.MapperMock.Object, _userAuthenticationServiceConfiguration.UnitOfWorkMock.Object,
                _userAuthenticationServiceConfiguration.TokenGeneratorUserMock.Object, _userAuthenticationServiceConfiguration.UserCreateAccountFunctionMock.Object,
                _userAuthenticationServiceConfiguration.SendEmailUserMock.Object, _userAuthenticationServiceConfiguration.UserSendCodeEmailDTOValidatorMock.Object);

            _userAuthenticationService = userAuthenticationService;
        }

        [Fact]
        public async Task Should_GetByIdInfoUser_Success()
        {
            string userId = "b914b9e4-5cf8-486d-be48-866254f21274";
            
            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserByIdInfoUser(It.IsAny<Guid>()))
            .ReturnsAsync(new User());

            var result = await _userAuthenticationService.GetByIdInfoUser(userId);

            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Login_Success()
        {
            string phone = "(+55) 23 32323 3223";
            string password = "password123";

            string storedHashedPassword = "ascascascascascasc";

            var tokenOutValue = new TokenOutValue();
            tokenOutValue.ValidateToken("ascascascascscaascasc", DateTime.Now);

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserInfoToLogin(It.IsAny<string>()))
            .ReturnsAsync(new User(storedHashedPassword));

            _userAuthenticationServiceConfiguration.UserCreateAccountFunctionMock
                .Setup(rep => rep.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(storedHashedPassword);

            _userAuthenticationServiceConfiguration.TokenGeneratorUserMock
                .Setup(rep => rep.Generator(It.IsAny<User>())).Returns(InfoErrors.Ok(tokenOutValue));

            var result = await _userAuthenticationService.Login(phone, password);

            Assert.True(result.IsSucess);
            Assert.True(result.Data?.PasswordIsCorrect);
        }

        [Fact]
        public async Task Should_ChangePasswordUser_Success_User_Already_Exist()
        {
            var userChangePasswordDTO = new UserChangePasswordDTO("(+55) 23 32323 3223", "password123");

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserByPhoneInfoUpdate(It.IsAny<string>()))
            .ReturnsAsync(new User());

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.UpdateAsync(It.IsAny<User>()))
            .ReturnsAsync(new User());

            var result = await _userAuthenticationService.ChangePasswordUser(userChangePasswordDTO);

            Assert.True(result.IsSucess);
            Assert.True(result.Data?.PasswordUpdateSuccessfully);
        }

        [Fact]
        public async Task Should_SendCodeEmail_Success_User_Already_Exist()
        {
            var userId = Guid.NewGuid();
            var userUpdateAllDTO = new UserDTO("ascasc", "ascasc@gmail.com", "password123");

            _userAuthenticationServiceConfiguration.UserSendCodeEmailDTOValidatorMock.Setup(rep => rep.ValidateDTO(It.IsAny<UserDTO>()))
                .Returns(new ValidationResult());

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserByName(It.IsAny<string>()))
                .ReturnsAsync(new User());

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetIfUserExistEmail(It.IsAny<string>()))
                .ReturnsAsync(new User());

            //_userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetIfUserExistEmail(It.IsAny<string>()))
            //    .ReturnsAsync((User?)null);

            var result = await _userAuthenticationService.SendCodeEmail(userUpdateAllDTO);
            CodeSendEmailUserDTO? data = result.Data;

            Assert.True(result.IsSucess);
            Assert.True(data?.UserAlreadyExist);
        }

        [Fact]
        public async Task Should_SendCodeEmail_Success_User_Not_Exist()
        {
            var userId = Guid.NewGuid();
            var userUpdateAllDTO = new UserDTO("ascasc", "ascasc@gmail.com", "password123");

            _userAuthenticationServiceConfiguration.UserSendCodeEmailDTOValidatorMock.Setup(rep => rep.ValidateDTO(It.IsAny<UserDTO>()))
                .Returns(new ValidationResult());

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserByName(It.IsAny<string>()))
                .ReturnsAsync(new User());

            //_userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetIfUserExistEmail(It.IsAny<string>()))
            //    .ReturnsAsync(new User());

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetIfUserExistEmail(It.IsAny<string>()))
                .ReturnsAsync((User?)null);

            var result = await _userAuthenticationService.SendCodeEmail(userUpdateAllDTO);
            CodeSendEmailUserDTO? data = result.Data;

            Assert.True(result.IsSucess);
            Assert.False(data?.UserAlreadyExist);
            Assert.True(data?.CodeSendToEmailSuccessfully);
        }

        [Fact]
        public async Task Should_Verfic_Success()
        {
            var userUpdateAllDTO1 = new UserDTO("ascasc", "ascasc@gmail.com", "password123");
            var userReturnGet = new User();

            var userId = Guid.NewGuid();

            userReturnGet.SetGuidId(userId);

            _userAuthenticationServiceConfiguration.UserSendCodeEmailDTOValidatorMock.Setup(rep => rep.ValidateDTO(It.IsAny<UserDTO>()))
                .Returns(new ValidationResult());

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserByName(It.IsAny<string>()))
                .ReturnsAsync(userReturnGet);

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetIfUserExistEmail(It.IsAny<string>()))
                .ReturnsAsync((User?)null);

            var result1 = await _userAuthenticationService.SendCodeEmail(userUpdateAllDTO1);
            var codeSend = result1.Data;

            var userConfirmCodeEmailDTO = new UserConfirmCodeEmailDTO(codeSend?.Code, userId.ToString(), "ascas@gmail.com");

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserById(It.IsAny<Guid>()))
                .ReturnsAsync(new User());

            var result = await _userAuthenticationService.Verfic(userConfirmCodeEmailDTO);

            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Throw_Error_GuidId_GetByIdInfoUser()
        {
            string userId = "";

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserByIdInfoUser(It.IsAny<Guid>()))
            .ThrowsAsync(new Exception("Unrecognized Guid format."));

            var result = await _userAuthenticationService.GetByIdInfoUser(userId);

            Assert.False(result.IsSucess);
            Assert.Equal("Unrecognized Guid format.", result.Message);
        }

        [Fact]
        public async Task Should_Thow_Error_User_Not_Exist_Login()
        {
            string phone = "(+55) 23 32323 3223";
            string password = "password123";

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserInfoToLogin(It.IsAny<string>()))
            .ReturnsAsync((User?)null);

            var result = await _userAuthenticationService.Login(phone, password);

            Assert.False(result.IsSucess);
            Assert.Equal("Error user null", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_Login_Password_False()
        {
            string phone = "(+55) 23 32323 3223";
            string password = "password123";

            string storedHashedPassword = "ascascascascascasc";

            var tokenOutValue = new TokenOutValue();
            tokenOutValue.ValidateToken("ascascascascscaascasc", DateTime.Now);

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserInfoToLogin(It.IsAny<string>()))
            .ReturnsAsync(new User(storedHashedPassword));

            _userAuthenticationServiceConfiguration.UserCreateAccountFunctionMock
                .Setup(rep => rep.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>())).Returns("seila");

            var result = await _userAuthenticationService.Login(phone, password);

            Assert.False(result.IsSucess);
            Assert.False(result.Data?.PasswordIsCorrect);
        }

        [Fact]
        public async Task Should_Token_Error_Login()
        {
            string phone = "(+55) 23 32323 3223";
            string password = "password123";

            string storedHashedPassword = "ascascascascascasc";

            var tokenOutValue = new TokenOutValue();
            tokenOutValue.ValidateToken(null, DateTime.Now);

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserInfoToLogin(It.IsAny<string>()))
            .ReturnsAsync(new User(storedHashedPassword));

            _userAuthenticationServiceConfiguration.UserCreateAccountFunctionMock
                .Setup(rep => rep.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(storedHashedPassword);

            _userAuthenticationServiceConfiguration.TokenGeneratorUserMock
                .Setup(rep => rep.Generator(It.IsAny<User>())).Returns(InfoErrors.Fail(tokenOutValue, "error token"));

            var result = await _userAuthenticationService.Login(phone, password);

            Assert.False(result.IsSucess);
            Assert.Equal("error token", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_User_Not_Found_ChangePasswordUser()
        {
            var userChangePasswordDTO = new UserChangePasswordDTO("(+55) 23 32323 3223", "password123");

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserByPhoneInfoUpdate(It.IsAny<string>()))
            .ReturnsAsync((User?)null);

            var result = await _userAuthenticationService.ChangePasswordUser(userChangePasswordDTO);

            Assert.False(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_Update_User_ChangePasswordUser()
        {
            var userChangePasswordDTO = new UserChangePasswordDTO("(+55) 23 32323 3223", "password123");

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserByPhoneInfoUpdate(It.IsAny<string>()))
            .ReturnsAsync(new User());

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.UpdateAsync(It.IsAny<User>()))
            .ReturnsAsync((User?)null);

            var result = await _userAuthenticationService.ChangePasswordUser(userChangePasswordDTO);

            Assert.False(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_DTO_Null_SendCodeEmail()
        {
            var result = await _userAuthenticationService.SendCodeEmail(null);
            CodeSendEmailUserDTO? data = result.Data;

            Assert.False(result.IsSucess);
            Assert.Equal("Error DTO user null", result.Message);
        }

        [Fact]
        public async Task Should_Return_Validate_DTO_SendCodeEmail()
        {
            var userId = Guid.NewGuid();
            var userUpdateAllDTO = new UserDTO("ascasc", "ascasc@gmail.com", "password123");

            _userAuthenticationServiceConfiguration.UserSendCodeEmailDTOValidatorMock.Setup(rep => rep.ValidateDTO(It.IsAny<UserDTO>()))
               .Returns(new ValidationResult(new List<ValidationFailure>
                {
                new ValidationFailure("PropertyName", "Error message 1"),
                }));

            var result = await _userAuthenticationService.SendCodeEmail(userUpdateAllDTO);

            Assert.False(result.IsSucess);
        }

        [Fact]
        public async Task Should_Error_Get_User_Null_SendCodeEmail()
        {
            var userId = Guid.NewGuid();
            var userUpdateAllDTO = new UserDTO("ascasc", "ascasc@gmail.com", "password123");

            _userAuthenticationServiceConfiguration.UserSendCodeEmailDTOValidatorMock.Setup(rep => rep.ValidateDTO(It.IsAny<UserDTO>()))
                .Returns(new ValidationResult());

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserByName(It.IsAny<string>()))
                .ReturnsAsync((User?)null);

            var result = await _userAuthenticationService.SendCodeEmail(userUpdateAllDTO);
            CodeSendEmailUserDTO? data = result.Data;

            Assert.False(result.IsSucess);
            Assert.Equal("Error User it is null", result.Message);
        }

        [Fact]
        public async Task Should_Error_Email_Is_Null_SendCodeEmail()
        {
            var userId = Guid.NewGuid();
            var userUpdateAllDTO = new UserDTO("ascasc", null, "password123");

            _userAuthenticationServiceConfiguration.UserSendCodeEmailDTOValidatorMock.Setup(rep => rep.ValidateDTO(It.IsAny<UserDTO>()))
                .Returns(new ValidationResult());

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserByName(It.IsAny<string>()))
                .ReturnsAsync(new User());

            var result = await _userAuthenticationService.SendCodeEmail(userUpdateAllDTO);
            CodeSendEmailUserDTO? data = result.Data;

            Assert.False(result.IsSucess);
            Assert.Equal("User Not Provided Email", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_Code_Not_Found_Verfic()
        {
            var userConfirmCodeEmailDTO = new UserConfirmCodeEmailDTO("233456", Guid.NewGuid().ToString(), "ascas@gmail.com");

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(rep => rep.GetUserById(It.IsAny<Guid>()))
                .ReturnsAsync(new User());

            var result = await _userAuthenticationService.Verfic(userConfirmCodeEmailDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("Error Code Not Found", result.Message);
        }
    }
}
