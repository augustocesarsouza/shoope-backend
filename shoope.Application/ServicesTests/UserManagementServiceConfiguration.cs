using AutoMapper;
using CloudinaryDotNet;
using Moq;
using Shoope.Application.DTOs.Validations.Interfaces;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.UtilityExternal.Interface;

namespace Shoope.Application.ServicesTests
{
    public class UserManagementServiceConfiguration
    {
        public Mock<IUserRepository> UserRepositoryMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }
        public Mock<IUserCreateDTOValidator> UserCreateDTOValidatorMock { get; }
        public Mock<IUserCreateAccountFunction> UserCreateAccountFunctionMock { get; }
        public Mock<ICloudinaryUti> CloudinaryUtiMock { get; }

        public UserManagementServiceConfiguration()
        {
            UserRepositoryMock = new();
            MapperMock = new();
            UnitOfWorkMock = new();
            UserCreateDTOValidatorMock = new();
            UserCreateAccountFunctionMock = new();
            CloudinaryUtiMock = new();
        }
    }
}
