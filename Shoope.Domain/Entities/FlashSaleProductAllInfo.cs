namespace Shoope.Domain.Entities
{
    public class FlashSaleProductAllInfo
    {
        public Guid? Id { get; private set; }
        //public Guid? ProductsOfferFlashId { get; private set; }
        public Guid? ProductsOfferFlashId { get; private set; }
        public ProductsOfferFlash? ProductsOfferFlash { get; private set; }
        public double? ProductReviewsRate { get; private set; }
        public int? QuantitySold { get; private set; }
        public double? FavoriteQuantity { get; private set; }
        public double? QuantityAvaliation { get; private set; }
        public int? Coins { get; private set; }
        public string? CreditCard { get; private set; }
        public string? Voltage { get; private set; }
        public int? QuantityPiece { get; private set; }
        public string? Size { get; private set; } // size exemplo "Camisa - 'P, M, G, GG', Piscina - '140cm*100cm*51cm', '200cm*140cm*51cm' "
        public bool? ProductHaveInsurance { get; private set; }

        public FlashSaleProductAllInfo()
        {
        }

        public FlashSaleProductAllInfo(Guid? id, Guid? productsOfferFlashId, double? productReviewsRate, 
            int? quantitySold, ProductsOfferFlash? productsOfferFlash, double? favoriteQuantity , double? quantityAvaliation, int? coins,
            string? creditCard, string? voltage, int? quantityPiece, string? size, bool? productHaveInsurance)
        {
            Id = id;
            ProductsOfferFlashId = productsOfferFlashId;
            ProductReviewsRate = productReviewsRate;
            QuantitySold = quantitySold;
            ProductsOfferFlash = productsOfferFlash;
            FavoriteQuantity = favoriteQuantity;
            QuantityAvaliation = quantityAvaliation;
            Coins = coins;
            CreditCard = creditCard;
            //Color = color;
            Voltage = voltage;
            QuantityPiece = quantityPiece;
            Size = size;
            ProductHaveInsurance = productHaveInsurance;
        }
    }
}
