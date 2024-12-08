namespace Shoope.Application.DTOs
{
    public class ProductFlashSaleReviewsDTO
    {
        public Guid? Id { get; set; }
        public string? Message { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? CostBenefit { get; set; }
        public string? SimilarToAd { get; set; }
        public int? StarQuantity { get; set; }
        public Guid? ProductsOfferFlashId { get; set; }
        public Guid? UserId { get; set; }
        public UserDTO? UserDTO { get; set; }
        public List<string>? ImgAndVideoReviewsProductElements { get; set; }
        public List<string>? ImgAndVideoReviewsProduct { get; set; }
        public string? Variation { get; set; }

        public ProductFlashSaleReviewsDTO(Guid? id, string? message, DateTime? creationDate, string? costBenefit, string? similarToAd, 
            int? starQuantity, Guid? productsOfferFlashId, Guid? userId, UserDTO? userDTO, List<string>? imgAndVideoReviewsProduct, string? variation)
        {
            Id = id;
            Message = message;
            CreationDate = creationDate;
            CostBenefit = costBenefit;
            SimilarToAd = similarToAd;
            StarQuantity = starQuantity;
            ProductsOfferFlashId = productsOfferFlashId;
            UserId = userId;
            UserDTO = userDTO;
            ImgAndVideoReviewsProduct = imgAndVideoReviewsProduct;
            Variation = variation;
        }

        public ProductFlashSaleReviewsDTO()
        {
        }

        public void SetId(Guid? id)
        {
            Id = id;
        }

        public void SetCreationDate(DateTime? creationDate)
        {
            CreationDate = creationDate;
        }

        public void SetImgAndVideoReviewsProduct(List<string>? imgAndVideoReviewsProduct)
        {
            ImgAndVideoReviewsProduct = imgAndVideoReviewsProduct;
        }

        public void SetImgAndVideoReviewsProductElements(List<string>? imgAndVideoReviewsProductElements)
        {
            ImgAndVideoReviewsProductElements = imgAndVideoReviewsProductElements;
        }
    }
}
