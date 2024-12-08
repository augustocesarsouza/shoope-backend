using FluentValidation;
using FluentValidation.Results;
using Shoope.Application.DTOs.Validations.Interfaces;

namespace Shoope.Application.DTOs.Validations.ProductDetailValidator
{
    public class ProductDetailCreateDTOValidator : AbstractValidator<ProductDetailDTO>, IProductDetailCreateDTOValidator
    {
        public ProductDetailCreateDTOValidator()
        {
            RuleFor(x => x.PromotionalStock)
                .NotNull()
                .WithMessage("Must be informed PromotionalStock")
                .GreaterThan(0)
                .WithMessage("Must be Greather Than 0 PromotionalStock");

            RuleFor(x => x.TotalStock)
                .NotNull()
                .WithMessage("Must be informed TotalStock")
                .GreaterThan(0)
                .WithMessage("Must be Greather Than 0 TotalStock");

            RuleFor(x => x.SendingOf)
                .NotNull()
                .WithMessage("Must be informed SendingOf")
                .NotEmpty()
                .WithMessage("Can't be Empty SendingOf"); 
        }

        public ValidationResult ValidateDTO(ProductDetailDTO productDetailDTO)
        {
            return Validate(productDetailDTO);
        }
    }
}
