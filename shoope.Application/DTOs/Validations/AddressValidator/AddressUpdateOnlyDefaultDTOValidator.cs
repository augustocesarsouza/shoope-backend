using FluentValidation;
using FluentValidation.Results;
using Shoope.Application.DTOs.Validations.Interfaces;

namespace Shoope.Application.DTOs.Validations.AddressValidator
{
    public class AddressUpdateOnlyDefaultDTOValidator : AbstractValidator<AddressDTO>, IAddressUpdateOnlyDefaultDTOValidator
    {
        public AddressUpdateOnlyDefaultDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("Must be informed Id");

            RuleFor(x => x.DefaultAddress)
                    .NotNull()
                    .WithMessage("DefaultAddress can't be null")
                    .InclusiveBetween((byte)0, (byte)1)
                    .WithMessage("DefaultAddress must be 0 or 1.");
        }

        public ValidationResult ValidateDTO(AddressDTO addressDTO)
        {
            return Validate(addressDTO);
        }
    }
}
