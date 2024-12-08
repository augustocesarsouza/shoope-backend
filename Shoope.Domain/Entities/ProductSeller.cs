namespace Shoope.Domain.Entities
{
    public class ProductSeller
    {
        public Guid? Id { get; private set; }

        public Guid? UserSellerProductId { get; private set; }
        public UserSellerProduct? UserSellerProduct { get; private set; }

        public Guid? ProductId { get; private set; }

        public ProductSeller(Guid? id, Guid? userSellerProductId, UserSellerProduct? userSellerProduct, Guid? productId)
        {
            Id = id;
            UserSellerProductId = userSellerProductId;
            UserSellerProduct = userSellerProduct;
            ProductId = productId;
        }

        public ProductSeller()
        {
        }
    }
}
