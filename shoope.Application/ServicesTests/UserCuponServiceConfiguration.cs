using AutoMapper;
using Moq;
using Shoope.Domain.Repositories;

namespace Shoope.Application.ServicesTests
{
    public class UserCuponServiceConfiguration
    {
        public Mock<IUserCuponRepository> UserCuponRepositoryMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }

        public UserCuponServiceConfiguration()
        {
            UserCuponRepositoryMock = new();
            MapperMock = new();
            UnitOfWorkMock = new();
        }
    }
}
