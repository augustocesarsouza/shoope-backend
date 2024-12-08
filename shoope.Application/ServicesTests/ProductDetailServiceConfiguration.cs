using AutoMapper;
using Moq;
using Shoope.Application.DTOs.Validations.Interfaces;
using Shoope.Domain.Repositories;

namespace Shoope.Application.ServicesTests
{
    public class ProductDetailServiceConfiguration
    {
        public Mock<IProductDetailRepository> ProductDetailRepositoryMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }
        public Mock<IProductDetailCreateDTOValidator> ProductDetailCreateDTOValidatorMock { get; }

        public ProductDetailServiceConfiguration()
        {
            ProductDetailRepositoryMock = new();
            MapperMock = new();
            UnitOfWorkMock = new();
            ProductDetailCreateDTOValidatorMock = new();
        }
    }
}
