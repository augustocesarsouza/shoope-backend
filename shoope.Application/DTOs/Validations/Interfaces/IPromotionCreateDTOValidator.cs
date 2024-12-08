namespace Shoope.Application.DTOs.Validations.Interfaces
{
    public interface IPromotionCreateDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(PromotionDTO promotionDTO);
    }
}
