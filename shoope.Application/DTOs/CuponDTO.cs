namespace Shoope.Application.DTOs
{
    public class CuponDTO
    {
        public Guid? Id { get; set; }
        public string? FirstText { get; set; }
        public string? SecondText { get; set; }
        public string? ThirdText { get; set; }
        public DateTime? DateValidateCupon { get; set; }
        public string? DateValidateCuponString { get; set; }
        public int? QuantityCupons { get; set; }
        public int? WhatCuponNumber { get; set; }
        public string? SecondImg { get; set; }
        public string? SecondImgAlt { get; set; }

        public CuponDTO()
        {
        }

        public CuponDTO(Guid? id, string? firstText, string? secondText, string? thirdText, DateTime? dateValidateCupon, 
            int? quantityCupons, int? whatCuponNumber, string? secondImg, string? secondImgAlt)
        {
            Id = id;
            FirstText = firstText;
            SecondText = secondText;
            ThirdText = thirdText;
            DateValidateCupon = dateValidateCupon;
            QuantityCupons = quantityCupons;
            WhatCuponNumber = whatCuponNumber;
            SecondImg = secondImg;
            SecondImgAlt = secondImgAlt;
        }

        public CuponDTO(Guid? id, string? firstText, string? secondText, string? thirdText, DateTime? dateValidateCupon, string? dateValidateCuponString, 
            int? quantityCupons, int? whatCuponNumber, string? secondImg, string? secondImgAlt)
        {
            Id = id;
            FirstText = firstText;
            SecondText = secondText;
            ThirdText = thirdText;
            DateValidateCupon = dateValidateCupon;
            DateValidateCuponString = dateValidateCuponString;
            QuantityCupons = quantityCupons;
            WhatCuponNumber = whatCuponNumber;
            SecondImg = secondImg;
            SecondImgAlt = secondImgAlt;
        }

        public void SetCuponId(Guid cuponId)
        {
            Id = cuponId;
        }

        public void SetValueDateValidateCupon(DateTime? dateValidateCupon)
        {
            DateValidateCupon = dateValidateCupon;
        }
    }
}
