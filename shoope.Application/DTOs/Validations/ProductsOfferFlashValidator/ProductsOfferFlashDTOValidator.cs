using FluentValidation;
using FluentValidation.Results;
using Shoope.Application.DTOs.Validations.Interfaces;

namespace Shoope.Application.DTOs.Validations.ProductsOfferFlashValidator
{
    public class ProductsOfferFlashDTOValidator : AbstractValidator<ProductsOfferFlashDTO>, IProductsOfferFlashDTOValidator
    {
        public ProductsOfferFlashDTOValidator()
        {
            RuleFor(x => x.ImgProduct)
                .NotEmpty()
                .WithMessage("ImgProduct can't be empty")
                .NotNull()
                .WithMessage("ImgProduct can't be null");

            RuleFor(x => x.AltValue)
                .NotNull()
                .WithMessage("Must be informed AltValue, cannot be null ")
                .NotEmpty()
                .WithMessage("cannot be empty AltValue");

            RuleFor(x => x.PriceProduct)
                .NotEmpty()
                .WithMessage("PriceProduct can't be empty")
                .NotNull()
                .WithMessage("PriceProduct can't be null")
                .GreaterThanOrEqualTo(0)
                .WithMessage("PriceProduct must be greater than or equal to 0");

            RuleFor(x => x.PopularityPercentage)
                .NotEmpty()
                .WithMessage("PopularityPercentage can't be empty")
                .NotNull()
                .WithMessage("PopularityPercentage can't be null")
                .GreaterThanOrEqualTo(0)
                .WithMessage("PopularityPercentage must be greater than or equal to 0")
                .LessThanOrEqualTo(100)
                .WithMessage("PopularityPercentage must be less than or equal to 100");

            RuleFor(x => x.HourFlashOffer)
                .NotEmpty()
                .WithMessage("HourFlashOffer can't be empty")
                .NotNull()
                .WithMessage("HourFlashOffer can't be null");

            RuleFor(x => x.TagProduct)
                .NotEmpty()
                .WithMessage("TagProduct can't be empty")
                .NotNull()
                .WithMessage("TagProduct can't be null");
        }

        public ValidationResult ValidateDTO(ProductsOfferFlashDTO productsOfferFlashDTO)
        {
            return Validate(productsOfferFlashDTO);
        }
    }
}
