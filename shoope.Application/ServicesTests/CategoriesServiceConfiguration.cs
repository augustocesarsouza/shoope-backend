using AutoMapper;
using Moq;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.UtilityExternal.Interface;

namespace Shoope.Application.ServicesTests
{
    public class CategoriesServiceConfiguration
    {
        public Mock<ICategoriesRepository> CategoriesRepositoryMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }
        public Mock<ICloudinaryUti> CloudinaryUtiMock { get; }

        public CategoriesServiceConfiguration()
        {
            CategoriesRepositoryMock = new();
            MapperMock = new();
            UnitOfWorkMock = new();
            CloudinaryUtiMock = new();
        }
    }
}
