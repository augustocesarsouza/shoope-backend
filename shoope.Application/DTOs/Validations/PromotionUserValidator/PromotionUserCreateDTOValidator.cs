using FluentValidation;
using FluentValidation.Results;
using Shoope.Application.DTOs.Validations.Interfaces;

namespace Shoope.Application.DTOs.Validations.AddressValidator
{
    public class PromotionUserCreateDTOValidator : AbstractValidator<PromotionUserDTO>, IPromotionUserCreateDTOValidator
    {
        public PromotionUserCreateDTOValidator()
        {
            RuleFor(x => x.PromotionId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Must be informed PromotionId");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Must be informed UserId");
        }

        public ValidationResult ValidateDTO(PromotionUserDTO promotionUserDTO)
        {
            return Validate(promotionUserDTO);
        }
    }
}
