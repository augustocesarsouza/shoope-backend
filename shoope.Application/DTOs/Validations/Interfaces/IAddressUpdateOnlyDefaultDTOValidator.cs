namespace Shoope.Application.DTOs.Validations.Interfaces
{
    public interface IAddressUpdateOnlyDefaultDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(AddressDTO addressDTO);
    }
}
