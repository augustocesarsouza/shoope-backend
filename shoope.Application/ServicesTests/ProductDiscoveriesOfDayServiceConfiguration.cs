using AutoMapper;
using Moq;
using Shoope.Application.DTOs.Validations.Interfaces;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.UtilityExternal.Interface;

namespace Shoope.Application.ServicesTests
{
    public class ProductDiscoveriesOfDayServiceConfiguration
    {
        public Mock<IProductDiscoveriesOfDayRepository> ProductDiscoveriesOfDayRepositoryMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }
        public Mock<ICloudinaryUti> CloudinaryUtiMock { get; }
        public Mock<IProductDiscoveriesOfDayCreateDTOValidator> ProductDiscoveriesOfDayCreateDTOValidatorMock { get; }

        public ProductDiscoveriesOfDayServiceConfiguration()
        {
            ProductDiscoveriesOfDayRepositoryMock = new();
            MapperMock = new();
            UnitOfWorkMock = new();
            CloudinaryUtiMock = new();
            ProductDiscoveriesOfDayCreateDTOValidatorMock = new();
        }
    }
}
