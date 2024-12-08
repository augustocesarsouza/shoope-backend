namespace Shoope.Application.DTOs.Validations.Interfaces
{
    public interface IProductDiscoveriesOfDayCreateDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(ProductDiscoveriesOfDayDTO productDiscoveriesOfDayDTO);
    }
}
