namespace Shoope.Application.DTOs.Validations.Interfaces
{
    public interface IProductsOfferFlashDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(ProductsOfferFlashDTO productsOfferFlashDTO);
    }
}
