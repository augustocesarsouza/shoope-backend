using Shoope.Application.DTOs;
using Shoope.Application.Services;
using Shoope.Domain.Entities;
using Moq;
using Xunit;

namespace Shoope.Application.ServicesTests.LikeReviewServiceTest
{
    public class LikeReviewServiceTest
    {
        private readonly LikeReviewServiceConfiguration _likeReviewServiceConfiguration;
        private readonly LikeReviewService _likeReviewService;

        public LikeReviewServiceTest()
        {
            _likeReviewServiceConfiguration = new();
            var likeReviewService = new LikeReviewService(_likeReviewServiceConfiguration.LikeReviewRepositoryMock.Object,
                _likeReviewServiceConfiguration.MapperMock.Object, _likeReviewServiceConfiguration.UnitOfWorkMock.Object);

            _likeReviewService = likeReviewService;
        }

        [Fact]
        public async Task Should_GetByProductFlashSaleReviewsId_Success()
        {
            Guid productFlashSaleReviewsId = Guid.NewGuid();

            _likeReviewServiceConfiguration.LikeReviewRepositoryMock
                .Setup(rep => rep.GetByProductFlashSaleReviewsId(It.IsAny<Guid>()))
                .ReturnsAsync(new List<LikeReview>());

            var result = await _likeReviewService.GetByProductFlashSaleReviewsId(productFlashSaleReviewsId);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_GetCuponById()
        {
            Guid productFlashSaleReviewsId = Guid.NewGuid();

            _likeReviewServiceConfiguration.LikeReviewRepositoryMock
                .Setup(rep => rep.GetByProductFlashSaleReviewsId(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("error GetByProductFlashSaleReviewsId by id"));

            var result = await _likeReviewService.GetByProductFlashSaleReviewsId(productFlashSaleReviewsId);

            Assert.False(result.IsSucess);
            Assert.Equal("error GetByProductFlashSaleReviewsId by id", result.Message);
        }

        [Fact]
        public async Task Should_CreateAsync_Success()
        {
            LikeReviewDTO likeReviewDTO = new LikeReviewDTO(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            _likeReviewServiceConfiguration.LikeReviewRepositoryMock
                .Setup(rep => rep.AlreadyExistLike(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync((LikeReview?)null);

            _likeReviewServiceConfiguration.LikeReviewRepositoryMock.Setup(rep => rep.CreateAsync(It.IsAny<LikeReview>()))
                .ReturnsAsync(new LikeReview());

            var result = await _likeReviewService.CreateAsync(likeReviewDTO);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Throw_Error_When_Create_DTO_Is_Null()
        {
            var result = await _likeReviewService.CreateAsync(null);
            Assert.False(result.IsSucess);
            Assert.Equal("error dto null", result.Message);
        }

        [Fact]
        public async Task Should_Return_Null_AlreadyExistLike()
        {
            LikeReviewDTO likeReviewDTO = new LikeReviewDTO(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            _likeReviewServiceConfiguration.LikeReviewRepositoryMock
                .Setup(rep => rep.AlreadyExistLike(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new LikeReview());

            var result = await _likeReviewService.CreateAsync(likeReviewDTO);
            Assert.False(result.IsSucess);
            Assert.True(result.Data?.AlreadyExistLikeReview);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_CreateAsync()
        {
            LikeReviewDTO likeReviewDTO = new LikeReviewDTO(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            _likeReviewServiceConfiguration.LikeReviewRepositoryMock
                .Setup(rep => rep.AlreadyExistLike(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync((LikeReview?)null);

            _likeReviewServiceConfiguration.LikeReviewRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<LikeReview>()))
                .ThrowsAsync(new Exception("error create LikeReview"));

            var result = await _likeReviewService.CreateAsync(likeReviewDTO);

            Assert.False(result.IsSucess);
            Assert.Equal("error create LikeReview", result.Message);
        }

        [Fact]
        public async Task Should_DeleteAsync_Success()
        {
            LikeReviewDTO likeReviewDTO = new LikeReviewDTO(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            _likeReviewServiceConfiguration.LikeReviewRepositoryMock
                .Setup(rep => rep.AlreadyExistLike(It.IsAny<Guid>(), It.IsAny<Guid>()))
                    .ReturnsAsync(new LikeReview());

            _likeReviewServiceConfiguration.LikeReviewRepositoryMock
                .Setup(rep => rep.GetByUserId(It.IsAny<Guid>()))
                .ReturnsAsync(new LikeReview());

            _likeReviewServiceConfiguration.LikeReviewRepositoryMock
                .Setup(rep => rep.DeleteAsync(It.IsAny<LikeReview>()))
                .ReturnsAsync(new LikeReview());

            var result = await _likeReviewService.DeleteAsync(likeReviewDTO);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_DeleteAsync_Throw_Error_DTO_Is_Null()
        {
            var result = await _likeReviewService.DeleteAsync(null);

            Assert.False(result.IsSucess);
            Assert.Equal("error dto null", result.Message);
        }

        [Fact]
        public async Task Should_DeleteAsync_Is_AlreadyExistLike_Return_Null()
        {
            LikeReviewDTO likeReviewDTO = new LikeReviewDTO(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            _likeReviewServiceConfiguration.LikeReviewRepositoryMock
                .Setup(rep => rep.AlreadyExistLike(It.IsAny<Guid>(), It.IsAny<Guid>()))
                    .ReturnsAsync((LikeReview?)null);

            var result = await _likeReviewService.DeleteAsync(likeReviewDTO);
            Assert.False(result.IsSucess);
            Assert.True(result.Data?.AlreadyExistLikeReview);
        }
    }
}
