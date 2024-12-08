namespace Shoope.Domain.Entities
{
    public class ProductDetail
    {
        public Guid? Id { get; private set; }
        public int? PromotionalStock { get; private set; }
        public int? TotalStock { get; private set; }
        public string? SendingOf { get; private set; }
        public string? Mark { get; private set; }
        public string? Gender { get; private set; }
        public string? WarrantlyDuration { get; private set; }
        public string? WarrantlyType { get; private set; }
        public string? ProductWeight { get; private set; }
        public string? EnergyConsumption { get; private set; }
        public string? Amount { get; private set; }
        public string? Material { get; private set; }
        public Guid? ProductId { get; private set; }

        public ProductDetail(Guid? id, int? promotionalStock, int? totalStock, string? mark, string? gender, string? warrantlyDuration, 
            string? warrantlyType, string? productWeight, string? energyConsumption, string? sendingOf, string? amount, string? material, Guid? productId)
        {
            Id = id;
            PromotionalStock = promotionalStock;
            TotalStock = totalStock;
            Mark = mark;
            Gender = gender;
            WarrantlyDuration = warrantlyDuration;
            WarrantlyType = warrantlyType;
            ProductWeight = productWeight;
            EnergyConsumption = energyConsumption;
            SendingOf = sendingOf;
            Amount = amount;
            Material = material;
            ProductId = productId;
        }

        public ProductDetail()
        {
        }
    }
}
