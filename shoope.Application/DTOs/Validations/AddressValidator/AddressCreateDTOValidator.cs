using FluentValidation;
using FluentValidation.Results;
using Shoope.Application.DTOs.Validations.Interfaces;

namespace Shoope.Application.DTOs.Validations.AddressValidator
{
    public class AddressCreateDTOValidator : AbstractValidator<AddressDTO>, IAddressCreateDTOValidator
    {
        public AddressCreateDTOValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Must be informed FullName");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .NotNull()
                .Matches(@"\(\+\d{2}\) \d{2}\ \d{5} \d{4}")
                .WithMessage("NumberHome must follow the format (+99) 99 99999 9999")
                .Length(19)
                .WithMessage("NumberHome must have 15 characters, including spaces and parentheses");

            RuleFor(x => x.Cep)
                .NotEmpty()
                .NotNull()
                .Matches(@"\d{5}\-\d{3}")
                .WithMessage("CEP must follow the format 99999-999")
                .Length(9)
                .WithMessage("CEP must have 9 characters, including the dash");

            RuleFor(x => x.Neighborhood)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2)
                .WithMessage("Neighborhood must have at least 2 characters");

            RuleFor(x => x.Street)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2)
                .WithMessage("Street must have at least 2 characters");

            RuleFor(x => x.NumberHome)
                .NotEmpty()
                .NotNull()
                .MinimumLength(1)
                .WithMessage("NumberHome must have at least 1 characters");
        }

        public ValidationResult ValidateDTO(AddressDTO addressDTO)
        {
            return Validate(addressDTO);
        }
    }
}
