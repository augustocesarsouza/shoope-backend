namespace Shoope.Domain.Entities
{
    public class LikeReview
    {
        public Guid? Id { get; private set; }
        public Guid? ProductFlashSaleReviewsId { get; private set; }
        public Guid? UserId { get; private set; }

        public LikeReview(Guid? id, Guid? productFlashSaleReviewsId, Guid? userId)
        {
            Id = id;
            ProductFlashSaleReviewsId = productFlashSaleReviewsId;
            UserId = userId;
        }

        public LikeReview()
        {
        }
    }
}
