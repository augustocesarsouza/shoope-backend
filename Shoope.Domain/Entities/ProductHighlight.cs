namespace Shoope.Domain.Entities
{
    public class ProductHighlight
    {
        public Guid? Id { get; private set; }
        public string? Title { get; private set; }
        public string? ImgProduct { get; private set; }
        public string? ImgProductPublicId { get; private set; }

        public string? ImgTop { get; private set; }
        public double? QuantitySold { get; private set; }

        public ProductHighlight()
        {
        }

        // Posso ter outra tabela que quando eu clicar no Product
        // Posso buscar os restos das informações como "avaliações, Vendidos" e o resto depois que clica para não ficar na mesma tablea "Product"
        // Nome desse tabela "AdicionalInformaçãoProduto"

        public ProductHighlight(Guid? id, string? title, string? imgProduct, string? imgProductPublicId, string? imgTop, double? quantitySold)
        {
            Id = id;
            Title = title;
            ImgProduct = imgProduct;
            ImgProductPublicId = imgProductPublicId;
            ImgTop = imgTop;
            QuantitySold = quantitySold;
        }

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
