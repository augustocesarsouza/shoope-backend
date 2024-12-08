using AutoMapper;
using Shoope.Application.DTOs;
using Shoope.Application.DTOs.Validations.Interfaces;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Shoope.Domain.Enums;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.CloudinaryConfigClass;
using Shoope.Infra.Data.UtilityExternal.Interface;
using System.ComponentModel;
using System.Reflection;

namespace Shoope.Application.Services
{
    public class ProductsOfferFlashService : IProductsOfferFlashService
    {
        private readonly IProductsOfferFlashRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryUti _cloudinaryUti;
        private readonly IProductsOfferFlashDTOValidator _productsOfferFlashDTOValidator;

        public ProductsOfferFlashService(IProductsOfferFlashRepository productRepository, IMapper mapper, IUnitOfWork unitOfWork,
            ICloudinaryUti cloudinaryUti, IProductsOfferFlashDTOValidator productsOfferFlashDTOValidator)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cloudinaryUti = cloudinaryUti;
            _productsOfferFlashDTOValidator = productsOfferFlashDTOValidator;
        }

        public async Task<ResultService<List<ProductsOfferFlashDTO>>> GetAllProduct()
        {
            try
            {
                var listProductDTO = await _productRepository.GetAllProduct();

                return ResultService.Ok(_mapper.Map<List<ProductsOfferFlashDTO>>(listProductDTO));

            }
            catch (Exception ex)
            {
                return ResultService.Fail<List<ProductsOfferFlashDTO>>(ex.Message);
            }
        }

        public async Task<ResultService<List<ProductsOfferFlashDTO>>> GetAllByTagProduct(string hourFlashOffer, string tagProduct, int pageNumber, int pageSize)
        {
            try
            {
                var listProductDTO = await _productRepository.GetAllByTagProduct(hourFlashOffer, tagProduct, pageNumber, pageSize);

                return ResultService.Ok(_mapper.Map<List<ProductsOfferFlashDTO>>(listProductDTO));

            }
            catch (Exception ex)
            {
                return ResultService.Fail<List<ProductsOfferFlashDTO>>(ex.Message);
            }
        }

        public async Task<ResultService<ProductsOfferFlashDTO>> CreateAsync(ProductsOfferFlashDTO? productDTO)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                if (productDTO == null)
                    return ResultService.Fail<ProductsOfferFlashDTO>("error DTO is null");

                var resultValidate = _productsOfferFlashDTOValidator.ValidateDTO(productDTO);

                if (!resultValidate.IsValid)
                    return ResultService.RequestError<ProductsOfferFlashDTO>("validation error check the information", resultValidate);

                if (productDTO.TagProduct == null)
                    return ResultService.Fail<ProductsOfferFlashDTO>("error tag_product is null");

                string tag = productDTO.TagProduct;
                bool isValidTag = IsValidTagProduct(tag);

                if (!isValidTag)
                    return ResultService.Fail<ProductsOfferFlashDTO>("provided type is not valid");

                ProductOfferFlashType type;

                type = (ProductOfferFlashType)Enum.Parse(typeof(ProductOfferFlashType), tag, true);

                CloudinaryCreate result = new();
                //CloudinaryCreate resultImgPartBottom = new();

                if (productDTO.ImgProduct == null)
                    return ResultService.Fail<ProductsOfferFlashDTO>("Error img product must be informed");

                result = await _cloudinaryUti.CreateMedia(productDTO.ImgProduct, "img-flash-deals", 320, 320);

                if (result.ImgUrl == null || result.PublicId == null)
                    return ResultService.Fail<ProductsOfferFlashDTO>("error when create ImgProduct");

                productDTO.SetImgProduct(result.ImgUrl);
                productDTO.SetImgProductPublicId(result.PublicId);

                var id = Guid.NewGuid();

                string description = GetEnumDescription(type);
                productDTO.SetTagProduct(description);
                productDTO.SetId(id);

                var productCreate = await _productRepository.CreateAsync(_mapper.Map<ProductsOfferFlash>(productDTO));

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<ProductsOfferFlashDTO>(productCreate));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<ProductsOfferFlashDTO>(ex.Message);
            }
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo? field = value.GetType().GetField(value.ToString());

            if (field != null)
            {
                DescriptionAttribute? attribute = field.GetCustomAttribute<DescriptionAttribute>();
                return attribute?.Description ?? value.ToString();
            }

            return value.ToString();
        }

        public bool IsValidTagProduct(string tagProduct)
        {
            var value = Enum.TryParse(typeof(ProductOfferFlashType), tagProduct, true, out _);
            return value;
        }
    }
}
