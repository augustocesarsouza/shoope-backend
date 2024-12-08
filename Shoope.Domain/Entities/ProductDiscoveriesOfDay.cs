namespace Shoope.Domain.Entities
{
    public class ProductDiscoveriesOfDay
    {
        public Guid? Id { get; private set; }
        public string? Title { get; private set; }
        public string? ImgProduct { get; private set; }
        public string? ImgProductPublicId { get; private set; }
        public string? ImgPartBottom { get; private set; }
        public int? DiscountPercentage { get; private set; }
        public bool? IsAd { get; private set; }
        public double? Price { get; private set; }
        public double? QuantitySold { get; private set; }

        public ProductDiscoveriesOfDay()
        {
        }

        public ProductDiscoveriesOfDay(Guid? id, string? title, string? imgProduct, string? imgProductPublicId, string? imgPartBottom, int? discountPercentage, 
            bool? isAd, double? price, double? quantitySold)
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

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
