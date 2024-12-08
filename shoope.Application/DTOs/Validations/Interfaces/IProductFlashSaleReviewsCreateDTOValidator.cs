using FluentValidation.Results;

namespace Shoope.Application.DTOs.Validations.Interfaces
{
    public interface IProductFlashSaleReviewsCreateDTOValidator
    {
        public ValidationResult ValidateDTO(ProductFlashSaleReviewsDTO productFlashSaleReviewsDTO);
    }
}
