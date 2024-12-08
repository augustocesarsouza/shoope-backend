namespace Shoope.Application.DTOs
{
    public class LikeReviewDTO
    {
        public Guid? Id { get; set; }
        public Guid? ProductFlashSaleReviewsId { get; set; }
        public Guid? UserId { get; set; }
        public bool AlreadyExistLikeReview { get; set; } = false;

        public LikeReviewDTO(Guid? id, Guid? productFlashSaleReviewsId, Guid? userId)
        {
            Id = id;
            ProductFlashSaleReviewsId = productFlashSaleReviewsId;
            UserId = userId;
        }

        public LikeReviewDTO(Guid? id, Guid? productFlashSaleReviewsId, Guid? userId, bool alreadyExistLikeReview) : this(id, productFlashSaleReviewsId, userId)
        {
            AlreadyExistLikeReview = alreadyExistLikeReview;
        }

        public LikeReviewDTO()
        {
        }
    }
}
