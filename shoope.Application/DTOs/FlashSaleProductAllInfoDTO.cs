using Shoope.Domain.Entities;

namespace Shoope.Application.DTOs
{
    public class FlashSaleProductAllInfoDTO
    {
        public Guid? Id { get; set; }
        //public Guid? ProductFlashSaleId { get; set; }
        public Guid? ProductsOfferFlashId { get; set; }
        public ProductsOfferFlashDTO? ProductsOfferFlashDTO { get; set; }
        public double? ProductReviewsRate { get; set; }
        public int? QuantitySold { get; set; }
        public double? FavoriteQuantity { get; set; }
        public double? QuantityAvaliation { get; set; }
        //public int? Reviews { get; private set; }
        public int? Coins { get; set; }
        public string? CreditCard { get; set; }
        public string? Voltage { get; set; }
        public int? QuantityPiece { get; set; }
        public string? Size { get; set; } // size exemplo "Camisa - 'P, M, G, GG', Piscina - '140cm*100cm*51cm', '200cm*140cm*51cm' "
        public bool? ProductHaveInsurance { get; set; }

        public FlashSaleProductAllInfoDTO(Guid? id, Guid? productsOfferFlashId, double? productReviewsRate, 
            int? quantitySold, ProductsOfferFlashDTO? productsOfferFlashDTO, double? favoriteQuantity, double? quantityAvaliation,
            int? coins, string? creditCard, string? voltage, int? quantityPiece, string? size, bool? productHaveInsurance)
        {
            Id = id;
            ProductsOfferFlashId = productsOfferFlashId;
            ProductReviewsRate = productReviewsRate;
            QuantitySold = quantitySold;
            ProductsOfferFlashDTO = productsOfferFlashDTO;
            FavoriteQuantity = favoriteQuantity;
            QuantityAvaliation = quantityAvaliation;
            Coins = coins;
            CreditCard = creditCard;
            Voltage = voltage;
            QuantityPiece = quantityPiece;
            Size = size;
            ProductHaveInsurance = productHaveInsurance;
        }

        public FlashSaleProductAllInfoDTO()
        {
        }

        public void SetId(Guid userId)
        {
            Id = userId;
        }
    }
}
