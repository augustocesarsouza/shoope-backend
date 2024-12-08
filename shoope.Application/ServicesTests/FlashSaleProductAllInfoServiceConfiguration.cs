using AutoMapper;
using Moq;
using Shoope.Domain.Repositories;

namespace Shoope.Application.ServicesTests
{
    public class FlashSaleProductAllInfoServiceConfiguration
    {
        public Mock<IFlashSaleProductAllInfoRepository> FlashSaleProductAllInfoRepositoryMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }

        public FlashSaleProductAllInfoServiceConfiguration()
        {
            FlashSaleProductAllInfoRepositoryMock = new();
            MapperMock = new();
            UnitOfWorkMock = new();
        }
    }
}
