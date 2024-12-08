namespace Shoope.Application.DTOs
{
    public class ProductDescriptionDTO
    {
        public Guid? Id { get; set; }
        public string? Description { get; set; }
        public List<string>? Characteristics { get; set; }
        public Guid? ProductId { get; set; }

        public ProductDescriptionDTO(Guid? id, string? description, List<string>? characteristics, Guid? productId)
        {
            Id = id;
            Description = description;
            Characteristics = characteristics;
            ProductId = productId;
        }

        public ProductDescriptionDTO()
        {
        }

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
