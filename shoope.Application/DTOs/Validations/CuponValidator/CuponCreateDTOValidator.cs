using FluentValidation;
using FluentValidation.Results;
using Shoope.Application.DTOs.Validations.Interfaces;

namespace Shoope.Application.DTOs.Validations.CuponValidator
{
    public class CuponCreateDTOValidator : AbstractValidator<CuponDTO>, ICuponCreateDTOValidator
    {
        public CuponCreateDTOValidator()
        {
            RuleFor(x => x.FirstText)
                .NotNull()
                .WithMessage("Must be informed FirstText")
                .NotEmpty()
                .WithMessage("Can't be Empty FirstText");

            RuleFor(x => x.SecondText)
                .NotNull()
                .WithMessage("Must be informed SecondText")
                .NotEmpty()
                .WithMessage("Can't be Empty SecondText");

            RuleFor(x => x.ThirdText)
                .NotNull()
                .WithMessage("Must be informed ThirdText")
                .NotEmpty()
                .WithMessage("Can't be Empty ThirdText");

            RuleFor(x => x.DateValidateCuponString)
                .NotNull()
                .WithMessage("Must be informed DateValidateCuponString")
                .NotEmpty()
                .WithMessage("Can't be Empty DateValidateCuponString");

            RuleFor(x => x.QuantityCupons)
                .GreaterThan(0)
                .WithMessage("Must be Greather Than 0 QuantityCupons");

            RuleFor(x => x.WhatCuponNumber)
                .InclusiveBetween(2, 6)
                .WithMessage("Must be between 2 and 6 WhatCuponNumber");
        }

        public ValidationResult ValidateDTO(CuponDTO cuponDTO)
        {
            return Validate(cuponDTO);
        }
    }
}

//public Guid? Id { get; set; }
//public string? FirstText { get; set; }
//public string? SecondText { get; set; }
//public string? ThirdText { get; set; }
//public DateTime? DateValidateCupon { get; set; }
//public string DateValidateCuponString { get; set; } = String.Empty;
//public int? QuantityCupons { get; set; }
//public int? WhatCuponNumber { get; set; }
//public string? SecondImg { get; set; }
//public string? SecondImgAlt { get; set; }