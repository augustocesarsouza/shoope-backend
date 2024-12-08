namespace Shoope.Domain.Entities
{
    public class ProductDescription
    {
        public Guid? Id { get; private set; }
        public string? Description { get; private set; }
        public List<string>? Characteristics { get; private set; }
        public Guid? ProductId { get; private set; }

        public ProductDescription(Guid? id, string? description, List<string>? characteristics, Guid? productId)
        {
            Id = id;
            Description = description;
            Characteristics = characteristics;
            ProductId = productId;
        }
        
        public ProductDescription()
        {
        }
    }
}
