using AutoMapper;
using Moq;
using Shoope.Domain.Repositories;

namespace Shoope.Application.ServicesTests
{
    public class LikeReviewServiceConfiguration
    {
        public Mock<ILikeReviewRepository> LikeReviewRepositoryMock { get; } = new();
        public Mock<IMapper> MapperMock { get; } = new();
        public Mock<IUnitOfWork> UnitOfWorkMock { get; } = new();

        public LikeReviewServiceConfiguration()
        {
        }
    }
}
