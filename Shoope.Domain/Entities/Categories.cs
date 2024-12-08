namespace Shoope.Domain.Entities
{
    public class Categories
    {
        public Guid? Id { get; private set; }
        public string? ImgCategory { get; private set; }
        public string? ImgCategoryPublicId { get; private set; }
        public string? AltValue { get; private set; }
        public string? Title { get; private set; }

        public Categories()
        {
        }

        public Categories(Guid? id, string? imgCategory, string? imgCategoryPublicId, string? altValue, string? title)
        {
            Id = id;
            ImgCategory = imgCategory;
            ImgCategoryPublicId = imgCategoryPublicId;
            AltValue = altValue;
            Title = title;
        }

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
