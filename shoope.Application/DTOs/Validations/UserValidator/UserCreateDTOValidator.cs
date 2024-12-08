using FluentValidation;
using FluentValidation.Results;
using Shoope.Application.DTOs.Validations.Interfaces;

namespace Shoope.Application.DTOs.Validations.UserValidator
{
    public class UserCreateDTOValidator : AbstractValidator<UserDTO>, IUserCreateDTOValidator
    {
        public UserCreateDTOValidator() 
        {
            RuleFor(x => x.Phone)
                .NotEmpty()
                .NotNull()
                .WithMessage("Must be informed one Number Phone");

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .Length(8, 30)
                .WithMessage("Must be informed Password of the user");
        }

        public ValidationResult ValidateDTO(UserDTO userDTO) 
        {
            return Validate(userDTO);
        }
    }
}
