using AutoMapper;
using Moq;
using Shoope.Application.CodeRandomUser;
using Shoope.Application.DTOs.Validations.Interfaces;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Authentication;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.SendEmailUser.Interface;

namespace Shoope.Application.ServicesTests
{
    public class UserAuthenticationServiceConfiguration
    {
        public Mock<IUserRepository> UserRepositoryMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }
        public Mock<ITokenGeneratorUser> TokenGeneratorUserMock { get; }
        public Mock<IUserCreateAccountFunction> UserCreateAccountFunctionMock { get; }
        public Mock<ISendEmailUser> SendEmailUserMock { get; }
        public Mock<IUserSendCodeEmailDTOValidator> UserSendCodeEmailDTOValidatorMock { get; }
        public Mock<CodeRandomDictionary> CodeRandomDictionaryMock { get; }

        public UserAuthenticationServiceConfiguration()
        {
            UserRepositoryMock = new();
            MapperMock = new();
            UnitOfWorkMock = new();
            TokenGeneratorUserMock = new();
            UserCreateAccountFunctionMock = new();
            SendEmailUserMock = new();
            UserSendCodeEmailDTOValidatorMock = new();
            CodeRandomDictionaryMock = new();
        }
    }
}
