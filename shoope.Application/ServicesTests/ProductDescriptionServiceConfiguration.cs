using AutoMapper;
using Moq;
using Shoope.Domain.Repositories;

namespace Shoope.Application.ServicesTests
{
    public class ProductDescriptionServiceConfiguration
    {
        public Mock<IProductDescriptionRepository> ProductDescriptionRepositoryMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }

        public ProductDescriptionServiceConfiguration()
        {
            ProductDescriptionRepositoryMock = new();
            MapperMock = new();
            UnitOfWorkMock = new();
        }
    }
}
