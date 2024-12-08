namespace Shoope.Application.DTOs.Validations.Interfaces
{
    public interface ICuponCreateDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(CuponDTO cuponDTO);
    }
}
