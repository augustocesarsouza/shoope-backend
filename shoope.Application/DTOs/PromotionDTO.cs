namespace Shoope.Application.DTOs
{
    public class PromotionDTO
    {
        public Guid? Id { get; set; }
        public int WhatIsThePromotion { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? DateCreate { get; set; }
        public DateTime Date { get; set; }
        public string Img { get; set; } = string.Empty;
        public string? PublicIdImg { get; set; }
        public string? ImgInnerFirst { get; set; }
        public string? AltImgInnerFirst { get; set; }
        public string? ImgInnerSecond { get; set; }
        public string? AltImgInnerSecond { get; set; }
        public string? ImgInnerThird { get; set; }
        public string? AltImgInnerThird { get; set; }
        public string? ImgInnerFirstPublicId { get; set; }
        public string? ImgInnerSecondPublicId { get; set; }
        public string? ImgInnerThirdPublicId { get; set; }

        public PromotionDTO()
        {
        }

        public PromotionDTO(Guid? id, string title, string description, string? dateCreate, DateTime date, string img, string? publicIdImg)
        {
            Id = id;
            Title = title;
            Description = description;
            DateCreate = dateCreate;
            Date = date;
            Img = img;
            PublicIdImg = publicIdImg;
        }

        public PromotionDTO(Guid? id, int whatIsThePromotion, string title, string description, string? dateCreate, DateTime date, string img, string? publicIdImg,
            string? imgInnerFirst, string? altImgInnerFirst, string? imgInnerSecond, string? altImgInnerSecond, string? imgInnerThird, string? altImgInnerThird,
            string? imgInnerFirstPublicId, string? imgInnerSecondPublicId, string? imgInnerThirdPublicId)
        {
            Id = id;
            WhatIsThePromotion = whatIsThePromotion;
            Title = title;
            Description = description;
            DateCreate = dateCreate;
            Date = date;
            Img = img;
            PublicIdImg = publicIdImg;
            ImgInnerFirst = imgInnerFirst;
            AltImgInnerFirst = altImgInnerFirst;
            ImgInnerSecond = imgInnerSecond;
            AltImgInnerSecond = altImgInnerSecond;
            ImgInnerThird = imgInnerThird;
            AltImgInnerThird = altImgInnerThird;

            ImgInnerFirstPublicId = imgInnerFirstPublicId;
            ImgInnerSecondPublicId = imgInnerSecondPublicId;
            ImgInnerThirdPublicId = imgInnerThirdPublicId;
        }
    }
}
