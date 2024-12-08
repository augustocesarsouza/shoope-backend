namespace Shoope.Application.DTOs
{
    public class ProductsOfferFlashDTO
    {
        public Guid? Id { get; set; }
        public string? ImgProduct { get; set; }
        public string? ImgProductPublicId { get; set; }
        public string? AltValue { get; set; }
        public string? ImgPartBottom { get; set; }
        //public string? ImgPartBottomPublicId { get; set; }
        public double? PriceProduct { get; set; }
        public int? PopularityPercentage { get; set; }
        public int? DiscountPercentage { get; set; }
        public string? HourFlashOffer { get; set; }
        public string? Title { get; set; }
        public string? TagProduct { get; set; }
        //public int? NumberProductYpe { get; set; }

        public ProductsOfferFlashDTO()
        {
        }

        public ProductsOfferFlashDTO(Guid? id, string? imgProduct, string? altValue, string? imgPartBottom, 
            double? priceProduct, int? popularityPercentage, int? discountPercentage, string? hourFlashOffer, string? title, string? tagProduct)
        {
            Id = id;
            ImgProduct = imgProduct;
            AltValue = altValue;
            ImgPartBottom = imgPartBottom;
            PriceProduct = priceProduct;
            PopularityPercentage = popularityPercentage;
            DiscountPercentage = discountPercentage;
            HourFlashOffer = hourFlashOffer;
            Title = title;
            TagProduct = tagProduct;
        }

        //public int? GetNumberProductYpe()
        //{
        //    return NumberProductYpe;
        //}

        public void SetImgProduct(string imgProduct)
        {
            ImgProduct = imgProduct;
        }

        public void SetImgProductPublicId(string imgProductPublicId)
        {
            ImgProductPublicId = imgProductPublicId;
        }

        public void SetImgPartBottom(string imgPartBottom)
        {
            ImgPartBottom = imgPartBottom;
        }

        public void SetTagProduct(string tagProduct)
        {
            TagProduct = tagProduct;
        }

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
