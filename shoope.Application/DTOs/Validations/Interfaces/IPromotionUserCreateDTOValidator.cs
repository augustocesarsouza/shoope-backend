namespace Shoope.Application.DTOs.Validations.Interfaces
{
    public interface IPromotionUserCreateDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(PromotionUserDTO promotionUserDTO);
    }
}
