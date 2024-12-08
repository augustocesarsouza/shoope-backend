namespace Shoope.Application.DTOs
{
    public class ProductDetailDTO
    {
        public Guid? Id { get; set; }
        public int? PromotionalStock { get; set; }
        public int? TotalStock { get; set; }
        public string? SendingOf { get; set; }
        public string? Mark { get; set; }
        public string? Gender { get; set; }
        public string? WarrantlyDuration { get; set; }
        public string? WarrantlyType { get; set; }
        public string? ProductWeight { get; set; }
        public string? EnergyConsumption { get; set; }
        public string? Amount { get; private set; }
        public string? Material { get; private set; }
        public Guid? ProductId { get; set; }

        public ProductDetailDTO(Guid? id, int? promotionalStock, int? totalStock, string? sendingOf, string? mark, string? gender, 
            string? warrantlyDuration, string? warrantlyType, string? productWeight, string? energyConsumption, string? amount, string? material, Guid? productId)
        {
            Id = id;
            PromotionalStock = promotionalStock;
            TotalStock = totalStock;
            SendingOf = sendingOf;
            Mark = mark;
            Gender = gender;
            WarrantlyDuration = warrantlyDuration;
            WarrantlyType = warrantlyType;
            ProductWeight = productWeight;
            EnergyConsumption = energyConsumption;
            Amount = amount;
            Material= material;
            ProductId = productId;
        }

        public ProductDetailDTO()
        {
        }

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
