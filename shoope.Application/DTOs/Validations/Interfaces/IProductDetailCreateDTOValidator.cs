using FluentValidation.Results;

namespace Shoope.Application.DTOs.Validations.Interfaces
{
    public interface IProductDetailCreateDTOValidator
    {
        public ValidationResult ValidateDTO(ProductDetailDTO productDetailDTO);
    }
}
