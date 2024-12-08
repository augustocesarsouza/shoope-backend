using Shoope.Domain.Entities;

namespace Shoope.Application.DTOs
{
    public class ProductOptionImageDTO
    {
        public Guid? Id { get; set; }
        public string? OptionType { get; set; }
        public string? ImageUrlBase64 { get; set; }
        public string? ImageUrl { get; set; }
        public string? PublicId { get; set; }
        public string? ImgAlt { get; set; }
        public string? TitleOptionType { get; set; }

        public Guid? ProductsOfferFlashId { get; set; }
        //public FlashSaleProductImageAll? FlashSaleProductImageAll { get; private set; }

        public ProductOptionImageDTO(Guid? id, string? optionType, string? imageUrl, string? publicId, Guid? productsOfferFlashId, string? titleOptionType)
        {
            Id = id;
            OptionType = optionType;
            ImageUrl = imageUrl;
            PublicId = publicId;
            ProductsOfferFlashId = productsOfferFlashId;
            TitleOptionType = titleOptionType;
        }

        public ProductOptionImageDTO()
        {
        }

        public void SetId(Guid? id)
        {
            Id = id;
        }

        public void SetImageUrl(string? imageUrl)
        {
            ImageUrl = imageUrl;
        }

        public void SetPublicId(string? publicId)
        {
            PublicId = publicId;
        }

        public void SetImageUrlBase64(string? imageUrlBase64)
        {
            ImageUrlBase64 = imageUrlBase64;
        }
    }
}
