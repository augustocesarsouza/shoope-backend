namespace Shoope.Domain.Entities
{
    public class ProductFlashSaleReviews
    {
        public Guid? Id { get; private set; }
        public string? Message { get; private set; }
        public DateTime? CreationDate { get; private set; }
        public string? CostBenefit { get; private set; }
        public string? SimilarToAd { get; private set; }
        public int? StarQuantity { get; private set; }
        public Guid? ProductsOfferFlashId { get; private set; } 
        public Guid? UserId { get; private set; }
        public User? User { get; private set; }
        public List<string>? ImgAndVideoReviewsProduct { get; private set; }
        public string? Variation { get; private set; }
        //public ProductsOfferFlash? ProductsOfferFlash { get; private set; }
        
        public ProductFlashSaleReviews()
        {
        }

        public ProductFlashSaleReviews(Guid? id, string? message, DateTime? creationDate,
            string? costBenefit, string? similarToAd, int? starQuantity, Guid? productsOfferFlashId, Guid? userId, User? user,
            List<string>? imgAndVideoReviewsProduct, string? variation)
        {
            Id = id;
            Message = message;
            CreationDate = creationDate;
            CostBenefit = costBenefit;
            SimilarToAd = similarToAd;
            StarQuantity = starQuantity;
            ProductsOfferFlashId = productsOfferFlashId;
            UserId = userId;
            User = user;
            ImgAndVideoReviewsProduct = imgAndVideoReviewsProduct;
            Variation = variation;
        }
    }
}
