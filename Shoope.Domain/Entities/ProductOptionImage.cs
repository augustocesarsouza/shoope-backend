namespace Shoope.Domain.Entities
{
    public class ProductOptionImage
    {
        public Guid? Id { get; private set; }
        public string? OptionType { get; private set; }
        public string? ImgAlt { get; private set; }
        public string? ImageUrl { get; private set; }
        public string? PublicId { get; private set; }
        public string? TitleOptionType { get; private set; }

        public Guid? ProductsOfferFlashId { get; private set; }
        //public FlashSaleProductImageAll? FlashSaleProductImageAll { get; private set; }

        public ProductOptionImage(Guid? id, string? optionType,  string? imageUrl, string? publicId, Guid? productsOfferFlashId, string? imgAlt, string? titleOptionType)
        {
            Id = id;
            OptionType = optionType;
            //esse aqui pode ser o nome que fica lá na propriedade do "frontend" tipo "cor"
            ImageUrl = imageUrl;
            PublicId = publicId;
            ProductsOfferFlashId = productsOfferFlashId;
            ImgAlt = imgAlt;
            TitleOptionType = titleOptionType;
        }

        public ProductOptionImage()
        {
        }

        public void SetId(Guid? id)
        {
            Id = id;
        }
    }
}
