using FluentValidation;
using FluentValidation.Results;
using Shoope.Application.DTOs.Validations.Interfaces;

namespace Shoope.Application.DTOs.Validations.ProductFlashSaleReviewsValidator
{
    public class ProductFlashSaleReviewsCreateDTOValidator : AbstractValidator<ProductFlashSaleReviewsDTO>, IProductFlashSaleReviewsCreateDTOValidator
    {
        public ProductFlashSaleReviewsCreateDTOValidator()
        {
            RuleFor(x => x.Message)
                .NotNull()
                .WithMessage("Must be informed Message")
                .NotEmpty()
                .WithMessage("Can't be Empty Message");

            RuleFor(x => x.CostBenefit)
                .NotNull()
                .WithMessage("Must be informed CostBenefit")
                .NotEmpty()
                .WithMessage("Can't be Empty CostBenefit");

            RuleFor(x => x.SimilarToAd)
                .NotNull()
                .WithMessage("Must be informed SimilarToAd")
                .NotEmpty()
                .WithMessage("Can't be Empty SimilarToAd");

            RuleFor(x => x.StarQuantity)
                .InclusiveBetween(0, 5)
                 .WithMessage("Must be between 0 and 5 StarQuantity");

            RuleFor(x => x.ProductsOfferFlashId)
                .NotNull()
                .WithMessage("Must be informed ProductsOfferFlashId")
                .NotEmpty()
                .WithMessage("Can't be Empty ProductsOfferFlashId");
        }

        public ValidationResult ValidateDTO(ProductFlashSaleReviewsDTO productFlashSaleReviewsDTO)
        {
            return Validate(productFlashSaleReviewsDTO);
        }
    }
}