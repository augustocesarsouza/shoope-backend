namespace Shoope.Application.DTOs.Validations.Interfaces
{
    public interface IPromotionCreateDTOIfPromotionNumber2Validator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(PromotionDTO promotionDTO);
    }
}
