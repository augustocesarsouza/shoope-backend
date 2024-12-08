using AutoMapper;
using Shoope.Application.DTOs;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.CloudinaryConfigClass;
using Shoope.Infra.Data.UtilityExternal.Interface;

namespace Shoope.Application.Services
{
    public class ProductHighlightService : IProductHighlightService
    {
        private readonly IProductHighlightRepository _productHighlightRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryUti _cloudinaryUti;

        public ProductHighlightService(IProductHighlightRepository productHighlightRepository, IMapper mapper, IUnitOfWork unitOfWork, ICloudinaryUti cloudinaryUti)
        {
            _productHighlightRepository = productHighlightRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cloudinaryUti = cloudinaryUti;
        }

        public async Task<ResultService<ProductHighlightDTO>> GetProductHighlightById(Guid productHighlightId)
        {
            try
            {
                var productHighlight = await _productHighlightRepository.GetProductHighlightById(productHighlightId);

                return ResultService.Ok(_mapper.Map<ProductHighlightDTO>(productHighlight));

            }
            catch (Exception ex)
            {
                return ResultService.Fail<ProductHighlightDTO>(ex.Message);
            }
        }

        public async Task<ResultService<List<ProductHighlightDTO>>> GetAllProductHighlights()
        {
            try
            {
                var productHighlights = await _productHighlightRepository.GetAllProductHighlight();

                return ResultService.Ok(_mapper.Map<List<ProductHighlightDTO>>(productHighlights));

            }
            catch (Exception ex)
            {
                return ResultService.Fail<List<ProductHighlightDTO>>(ex.Message);
            }
        }

        public async Task<ResultService<ProductHighlightDTO>> CreateAsync(ProductHighlightDTO? productHighlightDTO)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                if (productHighlightDTO == null)
                    return ResultService.Fail<ProductHighlightDTO>("error DTO is null");

                CloudinaryCreate result = new();

                if (productHighlightDTO.ImgProduct == null)
                    return ResultService.Fail<ProductHighlightDTO>("Error ImgProduct must be informed");

                result = await _cloudinaryUti.CreateMedia(productHighlightDTO.ImgProduct, "product-highlights", 500, 500);

                if (result.ImgUrl == null || result.PublicId == null)
                    return ResultService.Fail<ProductHighlightDTO>("error when create ImgProduct");

                productHighlightDTO.SetImgProduct(result.ImgUrl);
                productHighlightDTO.SetImgProductPublicId(result.PublicId);

                var id = Guid.NewGuid();

                productHighlightDTO.SetId(id);

                var productCreate = await _productHighlightRepository.CreateAsync(_mapper.Map<ProductHighlight>(productHighlightDTO));

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<ProductHighlightDTO>(productCreate));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<ProductHighlightDTO>(ex.Message);
            }
        }
    }
}
