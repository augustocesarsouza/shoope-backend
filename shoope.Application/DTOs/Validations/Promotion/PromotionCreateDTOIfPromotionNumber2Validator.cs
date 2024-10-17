using FluentValidation;
using FluentValidation.Results;
using Shoope.Application.DTOs.Validations.Interfaces;

namespace Shoope.Application.DTOs.Validations.Promotion
{
    public class PromotionCreateDTOIfPromotionNumber2Validator : AbstractValidator<PromotionDTO>, IPromotionCreateDTOIfPromotionNumber2Validator
    {
        public PromotionCreateDTOIfPromotionNumber2Validator()
        {
            RuleFor(x => x.AltImgInnerFirst)
                .NotNull()
                .WithMessage("Must be informed AltImgInnerFirst")
                .NotNull()
                .WithMessage("AltImgInnerFirst can't be null");

            RuleFor(x => x.ImgInnerSecond)
                .NotNull()
                .WithMessage("Must be informed ImgInnerSecond")
                .NotNull()
                .WithMessage("ImgInnerSecond can't be null");

            RuleFor(x => x.AltImgInnerThird)
                .NotNull()
                .WithMessage("Must be informed AltImgInnerThird")
                .NotNull()
                .WithMessage("AltImgInnerThird can't be null");
        }

        public ValidationResult ValidateDTO(PromotionDTO promotionDTO)
        {
            return Validate(promotionDTO);
        }
    }
}
