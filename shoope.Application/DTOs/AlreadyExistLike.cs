namespace Shoope.Application.DTOs
{
    public class AlreadyExistLike
    {
        public bool AlreadyExistLikeReview { get; set; } = false;

        public AlreadyExistLike(bool alreadyExistLikeReview)
        {
            AlreadyExistLikeReview = alreadyExistLikeReview;
        }

        public AlreadyExistLike()
        {
        }
    }
}
