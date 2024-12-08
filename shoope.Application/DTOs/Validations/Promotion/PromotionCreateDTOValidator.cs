using FluentValidation;
using FluentValidation.Results;
using Shoope.Application.DTOs.Validations.Interfaces;

namespace Shoope.Application.DTOs.Validations.AddressValidator
{
    public class PromotionCreateDTOValidator : AbstractValidator<PromotionDTO>, IPromotionCreateDTOValidator
    {
        public PromotionCreateDTOValidator()
        {
            RuleFor(x => x.WhatIsThePromotion)
                .NotNull()
                .WithMessage("Must be informed WhatIsThePromotion")
                .GreaterThan(0)
                .WithMessage("Must be greater than 0 WhatIsThePromotion");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title can't be empty")
                .NotNull()
                .WithMessage("Title can't be null");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description can't be empty")
                .NotNull()
                .WithMessage("Description can't be null");

            RuleFor(x => x.Img)
                .NotEmpty()
                .WithMessage("Img can't be empty")
                .NotNull()
                .WithMessage("Img can't be null");

            // CRIAR OS Validation
        }

        public ValidationResult ValidateDTO(PromotionDTO promotionDTO)
        {
            return Validate(promotionDTO);
        }
    }
}
