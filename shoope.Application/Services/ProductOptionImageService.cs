using AutoMapper;
using Shoope.Application.DTOs;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.CloudinaryConfigClass;
using Shoope.Infra.Data.UtilityExternal.Interface;

namespace Shoope.Application.Services
{
    public class ProductOptionImageService : IProductOptionImageService
    {
        private readonly IProductOptionImageRepository _productOptionImageRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryUti _cloudinaryUti;

        public ProductOptionImageService(IProductOptionImageRepository productOptionImageRepository, IMapper mapper, IUnitOfWork unitOfWork,
            ICloudinaryUti cloudinaryUti)
        {
            _productOptionImageRepository = productOptionImageRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cloudinaryUti = cloudinaryUti;
        }

        public async Task<ResultService<List<ProductOptionImageDTO>>> GetByListFlashSaleProductImageAllId(Guid productsOfferFlashId)
        {
            try
            {
                var resultGet = await _productOptionImageRepository.GetByListFlashSaleProductImageAllId(productsOfferFlashId);

                return ResultService.Ok(_mapper.Map<List<ProductOptionImageDTO>>(resultGet));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<List<ProductOptionImageDTO>>(ex.Message);
            }
        }

        public async Task<ResultService<ProductOptionImageDTO>> Create(ProductOptionImageDTO? productOptionImageDTO)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                if (productOptionImageDTO == null)
                    return ResultService.Fail<ProductOptionImageDTO>("error DTO informed is Null");

                var productOptionImageId = Guid.NewGuid();
                CloudinaryCreate result = new();

                productOptionImageDTO.SetId(productOptionImageId);

                if (productOptionImageDTO.ImageUrlBase64 == null)
                    return ResultService.Fail<ProductOptionImageDTO>("error ImageUrlBase64 is null");

                result = await _cloudinaryUti.CreateMedia(productOptionImageDTO.ImageUrlBase64, "product-option-image", 450, 450);

                if (result.ImgUrl == null || result.PublicId == null)
                    return ResultService.Fail<ProductOptionImageDTO>("error when create ImageUrlBase");

                productOptionImageDTO.SetImageUrl(result.ImgUrl);
                productOptionImageDTO.SetPublicId(result.PublicId);

                var productOptionImageDTOCreate = await _productOptionImageRepository.CreateAsync(_mapper.Map<ProductOptionImage>(productOptionImageDTO));

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<ProductOptionImageDTO>(productOptionImageDTOCreate));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<ProductOptionImageDTO>(ex.Message);
            }
        }

        public async Task<ResultService<string>> DeleteAllByProductsOfferFlashId(Guid productsOfferFlashId)
        {
            try
            {
                var resultGet = await _productOptionImageRepository.GetAllByProductsOfferFlashId(productsOfferFlashId);

                if (resultGet.Count <= 0)
                    return ResultService.Ok<string>("nothing was found");

                foreach (var item in resultGet)
                {
                    await _productOptionImageRepository.DeleteAsync(item);
                }

                return ResultService.Ok<string>("delete successfully");
            }
            catch (Exception ex)
            {
                return ResultService.Fail<string>(ex.Message);
            }
        }
    }
}
