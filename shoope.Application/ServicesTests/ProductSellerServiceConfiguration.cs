using AutoMapper;
using Moq;
using Shoope.Domain.Repositories;

namespace Shoope.Application.ServicesTests
{
    public class ProductSellerServiceConfiguration
    {
        public Mock<IProductSellerRepository> ProductSellerRepositoryMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }

        public ProductSellerServiceConfiguration()
        {
            ProductSellerRepositoryMock = new();
            MapperMock = new();
            UnitOfWorkMock = new();
        }
    }
}
