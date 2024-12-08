using AutoMapper;
using Moq;
using Shoope.Application.DTOs.Validations.Interfaces;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.UtilityExternal.Interface;

namespace Shoope.Application.ServicesTests
{
    public class ProductFlashSaleReviewsServiceConfiguration
    {
        public Mock<IProductFlashSaleReviewsRepository> ProductFlashSaleReviewsRepositoryMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }
        public Mock<IProductFlashSaleReviewsCreateDTOValidator> ProductFlashSaleReviewsCreateDTOValidatorMock { get; }
        public Mock<ICloudinaryUti> CloudinaryUtiMock { get; }

        public ProductFlashSaleReviewsServiceConfiguration()
        {
            ProductFlashSaleReviewsRepositoryMock = new();
            MapperMock = new();
            UnitOfWorkMock = new();
            ProductFlashSaleReviewsCreateDTOValidatorMock = new();
            CloudinaryUtiMock = new();
        }
    }
}
