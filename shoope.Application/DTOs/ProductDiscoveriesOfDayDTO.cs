namespace Shoope.Application.DTOs
{
    public class ProductDiscoveriesOfDayDTO
    {
        public Guid? Id { get; set; }
        public string? Title { get; set; }
        public string ImgProduct { get; set; } = string.Empty;
        public string? ImgProductPublicId { get; set; }
        public string? ImgPartBottom { get; set; }
        public int? DiscountPercentage { get; set; }
        public bool? IsAd { get; set; }
        public double? Price { get; set; }
        public double? QuantitySold { get; set; }

        public ProductDiscoveriesOfDayDTO()
        {
        }

        public ProductDiscoveriesOfDayDTO(Guid? id, string? title, string imgProduct, string? imgProductPublicId, string? imgPartBottom,
            int? discountPercentage, bool? isAd, double? price, double? quantitySold)
        {
            Id = id;
            Title = title;
            ImgProduct = imgProduct;
            ImgProductPublicId = imgProductPublicId;
            ImgPartBottom = imgPartBottom;
            DiscountPercentage = discountPercentage;
            IsAd = isAd;
            Price = price;
            QuantitySold = quantitySold;
        }

        public void SetImgProduct(string imgProduct)
        {
            ImgProduct = imgProduct;
        }

        public void SetImgProductPublicId(string imgProductPublicId)
        {
            ImgProductPublicId = imgProductPublicId;
        }

        public void SetId(Guid? id)
        {
            Id = id;
        }
    }
}
