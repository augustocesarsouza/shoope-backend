using AutoMapper;
using Moq;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.UtilityExternal.Interface;

namespace Shoope.Application.ServicesTests
{
    public class ProductOptionImageServiceConfiguration
    {
        public Mock<IProductOptionImageRepository> ProductOptionImageRepositoryMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }
        public Mock<ICloudinaryUti> CloudinaryUtiMock { get; }

        public ProductOptionImageServiceConfiguration()
        {
            ProductOptionImageRepositoryMock = new();
            MapperMock = new();
            UnitOfWorkMock = new();
            CloudinaryUtiMock = new();
        }
    }
}
