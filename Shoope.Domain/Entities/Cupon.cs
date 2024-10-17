namespace Shoope.Domain.Entities
{
    public class Cupon
    {
        public Guid? Id { get; private set; }
        public string? FirstText { get; private set; }
        public string? SecondText { get; private set; }
        public string? ThirdText { get; private set; }
        public DateTime? DateValidateCupon { get; private set; }
        public int? QuantityCupons { get; private set; }
        public int? WhatCuponNumber { get; private set; }
        public string? SecondImg { get; private set; }
        public string? SecondImgAlt { get; private set; }

        public Cupon()
        {
        }

        public Cupon(Guid? id, string? firstText, string? secondText, string? thirdText, DateTime? dateValidateCupon, 
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
    }
}

//const obj1 = {
//      id: 'e3a9bb90-6c7f-4bb5-bfd2-367e62184d7e',
//      spanOne: 'Para você',
//      headerOne: 'Frete Grátis',
//      spanTwo: 'Sem valor mínimo',
//      spanThree: 'Termina em: 1 dia',
//      quantityCupons: 1,
//      whatCuponNumber: 2,
//      secondImg: null,
//      secondImgAlt: 'second-img-1',
//    };