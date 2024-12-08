using AutoMapper;
using Moq;
using Shoope.Application.DTOs.Validations.Interfaces;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.UtilityExternal.Interface;

namespace Shoope.Application.ServicesTests
{
    public class ProductsOfferFlashServiceConfiguration
    {
        public Mock<IProductsOfferFlashRepository> ProductsOfferFlashRepositoryMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }
        public Mock<ICloudinaryUti> CloudinaryUtiMock { get; }
        public Mock<IProductsOfferFlashDTOValidator> ProductsOfferFlashDTOValidatorMock { get; }

        public ProductsOfferFlashServiceConfiguration()
        {
            ProductsOfferFlashRepositoryMock = new();
            MapperMock = new();
            UnitOfWorkMock = new();
            CloudinaryUtiMock = new();
            ProductsOfferFlashDTOValidatorMock = new();
        }
    }
}
