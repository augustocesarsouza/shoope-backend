namespace Shoope.Application.DTOs
{
    public class ProductSellerDTO
    {
        public Guid? Id { get; set; }

        public Guid? UserSellerProductId { get; set; }
        public UserSellerProductDTO? UserSellerProductDTO { get; set; }

        public Guid? ProductId { get; set; }

        public ProductSellerDTO(Guid? id, Guid? userSellerProductId, UserSellerProductDTO? userSellerProductDTO, Guid? productId)
        {
            Id = id;
            UserSellerProductId = userSellerProductId;
            UserSellerProductDTO = userSellerProductDTO;
            ProductId = productId;
        }

        public ProductSellerDTO()
        {
        }

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
