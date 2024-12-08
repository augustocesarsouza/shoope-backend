using Shoope.Domain.Enums;
using System.ComponentModel;
using System.Reflection;

namespace Shoope.Domain.Entities
{
    public class ProductsOfferFlash
    {
        // Product -> OFFER_FLASH
        public Guid? Id { get; private set; }
        public string? ImgProduct { get; private set; }
        public string? ImgProductPublicId { get; private set; }
        public string? AltValue { get; private set; }
        public string? ImgPartBottom { get; private set; }
        //public string? ImgPartBottomPublicId { get; private set; }
        public double? PriceProduct { get; private set; }
        public int? PopularityPercentage { get; private set; }
        public int? DiscountPercentage { get; private set; }
        public string? HourFlashOffer { get; private set; }
        public string? Title { get; private set; }
        public string? TagProduct { get; private set; }
        // TagProduct -> criar a migration para o banco de dados!

        public ProductsOfferFlash()
        {
        }

        public ProductsOfferFlash(Guid? id, string? imgProduct, string? imgProductPublicId, string? altValue, string? imgPartBottom,
            double? priceProduct, int? popularityPercentage, int? discountPercentage, string? hourFlashOffer, string? title, string? tagProduct)
        {
            Id = id;
            ImgProduct = imgProduct;
            ImgProductPublicId = imgProductPublicId;
            AltValue = altValue;
            ImgPartBottom = imgPartBottom;
            //ImgPartBottomPublicId = imgPartBottomPublicId;
            PriceProduct = priceProduct;
            PopularityPercentage = popularityPercentage;
            DiscountPercentage = discountPercentage;
            HourFlashOffer = hourFlashOffer;
            Title = title;
            TagProduct = tagProduct;
        }

        public ProductsOfferFlash(Guid? id, string? imgProduct, string? imgProductPublicId, string? altValue, double? priceProduct,
            int? popularityPercentage, int? discountPercentage)
        {
            Id = id;
            ImgProduct = imgProduct;
            ImgProductPublicId = imgProductPublicId;
            AltValue = altValue;
            PriceProduct = priceProduct;
            PopularityPercentage = popularityPercentage;
            DiscountPercentage = discountPercentage;
        }

        public void SetId(Guid userId)
        {
            Id = userId;
        }

        public void SetTagProduct(string tagProduct)
        {
            TagProduct = tagProduct;
        }

        public static string GetEnumDescription(ProductOfferFlashType value)
        {
            FieldInfo? field = value.GetType().GetField(value.ToString());

            if (field != null)
            {
                DescriptionAttribute? attribute = field.GetCustomAttribute<DescriptionAttribute>();
                return attribute?.Description ?? value.ToString();
            }

            return value.ToString();
        }
    }
}
