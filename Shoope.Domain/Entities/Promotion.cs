namespace Shoope.Domain.Entities
{
    public class Promotion
    {
        public Guid? Id { get; private set; }
        public int WhatIsThePromotion { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public DateTime Date { get; private set; }
        public string Img { get; private set; } = string.Empty;
        public string? PublicIdImg { get; private set; }
        public string? ImgInnerFirst { get; private set; }
        public string? AltImgInnerFirst { get; private set; }
        public string? ImgInnerSecond { get; private set; }
        public string? AltImgInnerSecond { get; private set; }
        public string? ImgInnerThird { get; private set; }
        public string? AltImgInnerThird { get; private set; }
        public string? ImgInnerFirstPublicId { get; private set; }
        public string? ImgInnerSecondPublicId { get; private set; }
        public string? ImgInnerThirdPublicId { get; private set; }
        
        public Promotion()
        {
        }

        public Promotion(Guid? id, int whatIsThePromotion, string title, string description, DateTime date, string img, string? publicIdImg)
        {
            Id = id;
            WhatIsThePromotion = whatIsThePromotion;
            Title = title;
            Description = description;
            Date = date;
            Img = img;
            PublicIdImg = publicIdImg;
        }

        public Promotion(Guid? id, int whatIsThePromotion, string title, string description, DateTime date, string img, string? publicIdImg,
            string? imgInnerFirst, string? altImgInnerFirst, string? imgInnerSecond, string? altImgInnerSecond, string? 
            imgInnerThird, string? altImgInnerThird, string? imgInnerFirstPublicId, string? imgInnerSecondPublicId, string? imgInnerThirdPublicId)
        {
            Id = id;
            WhatIsThePromotion = whatIsThePromotion;
            Title = title;
            Description = description;
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

//var birthDate = new DateTime(int.Parse(ano), int.Parse(mes), int.Parse(dia), int.Parse(hora), int.Parse(min), 0);
//var birthDateUtc = DateTime.SpecifyKind(birthDate, DateTimeKind.Utc);

// Suponha que `birthDateUtc` é a data recuperada do banco em UTC
//DateTime birthDateUtc = userFromDb.BirthDate.Value;

// Especificar o fuso horário do Brasil (ou o fuso horário do seu país)
//TimeZoneInfo localZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

// Converter o DateTime de UTC para o fuso horário local
//DateTime birthDateLocal = TimeZoneInfo.ConvertTimeFromUtc(birthDateUtc, localZone);