namespace Shoope.Application.DTOs
{
    public class CategoriesDTO
    {
        public Guid? Id { get; set; }
        public string? ImgCategory { get; set; }
        public string? ImgCategoryPublicId { get; set; }
        public string? AltValue { get; set; }
        public string? Title { get; set; }

        public CategoriesDTO()
        {
        }

        public CategoriesDTO(Guid? id, string? imgCategory, string? imgCategoryPublicId, string? altValue, string? title)
        {
            Id = id;
            ImgCategory = imgCategory;
            ImgCategoryPublicId = imgCategoryPublicId;
            AltValue = altValue;
            Title = title;
        }

        public void SetImgCategory(string img)
        {
            ImgCategory = img;
        }

        public void SetImgCategoryPublicId(string publicId)
        {
            ImgCategoryPublicId = publicId;
        }

        public void SetAltValue(string altValue)
        {
            AltValue = altValue;
        }

        public void SetTitle(string title)
        {
            Title = title;
        }

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
