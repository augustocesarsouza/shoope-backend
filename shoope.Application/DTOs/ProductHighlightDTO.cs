namespace Shoope.Application.DTOs
{
    public class ProductHighlightDTO
    {
        public Guid? Id { get; set; }
        public string? Title { get; set; }
        public string? ImgProduct { get; set; }
        public string? ImgProductPublicId { get; set; }

        public string? ImgTop { get; set; }
        public double? QuantitySold { get; set; }

        public ProductHighlightDTO()
        {
        }

        public ProductHighlightDTO(Guid? id, string? title, string? imgProduct, string? imgProductPublicId, string? imgTop, double? quantitySold)
        {
            Id = id;
            Title = title;
            ImgProduct = imgProduct;
            ImgProductPublicId = imgProductPublicId;
            ImgTop = imgTop;
            QuantitySold = quantitySold;
        }

        public void SetImgProduct(string imgProduct)
        {
            ImgProduct = imgProduct;
        }

        public void SetImgProductPublicId(string imgProductPublicId)
        {
            ImgProductPublicId = imgProductPublicId;
        }

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
