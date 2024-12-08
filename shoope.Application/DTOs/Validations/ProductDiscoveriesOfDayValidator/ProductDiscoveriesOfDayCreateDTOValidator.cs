using FluentValidation;
using FluentValidation.Results;
using Shoope.Application.DTOs.Validations.Interfaces;

namespace Shoope.Application.DTOs.Validations.ProductDiscoveriesOfDayValidator
{
    public class ProductDiscoveriesOfDayCreateDTOValidator : AbstractValidator<ProductDiscoveriesOfDayDTO>, IProductDiscoveriesOfDayCreateDTOValidator
    {
        public ProductDiscoveriesOfDayCreateDTOValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .WithMessage("Must be informed Title, cannot be null ")
                .NotEmpty()
                .WithMessage("cannot be empty Title");

            RuleFor(x => x.ImgProduct)
                .NotEmpty()
                .WithMessage("ImgProduct can't be empty")
                .NotNull()
                .WithMessage("ImgProduct can't be null");

            //RuleFor(x => x.ImgPartBottom)
            //    .NotEmpty()
            //    .WithMessage("ImgPartBottom can't be empty")
            //    .NotNull()
            //    .WithMessage("ImgPartBottom can't be null");

            RuleFor(x => x.IsAd)
                .NotEmpty()
                .WithMessage("IsAd can't be empty")
                .NotNull()
                .WithMessage("IsAd can't be null");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Must be Greather Than 0 Price");

            // CRIAR OS Validation
        }

        public ValidationResult ValidateDTO(ProductDiscoveriesOfDayDTO productDiscoveriesOfDayDTO)
        {
            return Validate(productDiscoveriesOfDayDTO);
        }
    }
}

//public Guid? Id { get; private set; }
//public string? Title { get; private set; }
//public string? ImgProduct { get; private set; }
//public string? ImgProductPublicId { get; private set; }
//public string? ImgPartBottom { get; private set; }
//public int? DiscountPercentage { get; private set; }
//public bool? IsAd { get; private set; }
//public double? Price { get; private set; }
//public double? QuantitySold { get; private set; }