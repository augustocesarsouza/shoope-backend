namespace Shoope.Domain.Entities
{
    public class PromotionUser
    {
        public Guid? Id { get; private set; }

        public Guid? PromotionId { get; private set; }
        public Promotion? Promotion { get; private set; }

        public Guid? UserId { get; private set; }
        public User? User { get; private set; }

        public PromotionUser()
        {
        }

        public PromotionUser(Guid? id, Guid? promotionId, Promotion? promotion, Guid? userId, User? user)
        {
            Id = id;
            PromotionId = promotionId;
            Promotion = promotion;
            UserId = userId;
            User = user;
        }

        public PromotionUser(Guid? id, Promotion? promotion, User? user)
        {
            Id = id;
            Promotion = promotion;
            User = user;
        }
    }
}
