using AutoMapper;
using Shoope.Application.DTOs;
using Shoope.Application.DTOs.Validations.Interfaces;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.CloudinaryConfigClass;
using Shoope.Infra.Data.UtilityExternal.Interface;

namespace Shoope.Application.Services
{
    public class ProductDiscoveriesOfDayService : IProductDiscoveriesOfDayService
    {
        private readonly IProductDiscoveriesOfDayRepository _productDiscoveriesOfDayRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryUti _cloudinaryUti;
        private readonly IProductDiscoveriesOfDayCreateDTOValidator _productDiscoveriesOfDayCreateDTOValidator;

        public ProductDiscoveriesOfDayService(IProductDiscoveriesOfDayRepository productDiscoveriesOfDayRepository, IMapper mapper, IUnitOfWork unitOfWork, 
            ICloudinaryUti cloudinaryUti, IProductDiscoveriesOfDayCreateDTOValidator productDiscoveriesOfDayCreateDTOValidator)
        {
            _productDiscoveriesOfDayRepository = productDiscoveriesOfDayRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cloudinaryUti = cloudinaryUti;
            _productDiscoveriesOfDayCreateDTOValidator = productDiscoveriesOfDayCreateDTOValidator;
        }

        public async Task<ResultService<ProductDiscoveriesOfDayDTO>> GetProductDiscoveriesOfDayById(Guid productDiscoveriesOfDayId)
        {
            try
            {
                var productProductDiscoveriesOfDay = await _productDiscoveriesOfDayRepository.GetProductDiscoveriesOfDayById(productDiscoveriesOfDayId);

                return ResultService.Ok(_mapper.Map<ProductDiscoveriesOfDayDTO>(productProductDiscoveriesOfDay));

            }
            catch (Exception ex)
            {
                return ResultService.Fail<ProductDiscoveriesOfDayDTO>(ex.Message);
            }
        }

        public async Task<ResultService<List<ProductDiscoveriesOfDayDTO>>> GetAllProductDiscoveriesOfDays()
        {
            try
            {
                var productHighlights = await _productDiscoveriesOfDayRepository.GetAllProductDiscoveriesOfDay();

                return ResultService.Ok(_mapper.Map<List<ProductDiscoveriesOfDayDTO>>(productHighlights));

            }
            catch (Exception ex)
            {
                return ResultService.Fail<List<ProductDiscoveriesOfDayDTO>>(ex.Message);
            }
        }

        public async Task<ResultService<ProductDiscoveriesOfDayDTO>> CreateAsync(ProductDiscoveriesOfDayDTO? productDiscoveriesOfDayDTO)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                if (productDiscoveriesOfDayDTO == null)
                    return ResultService.Fail<ProductDiscoveriesOfDayDTO>("error DTO is null");

                var resultValidate = _productDiscoveriesOfDayCreateDTOValidator.ValidateDTO(productDiscoveriesOfDayDTO);

                if (!resultValidate.IsValid)
                    return ResultService.RequestError<ProductDiscoveriesOfDayDTO>("validation error check the information", resultValidate);

                CloudinaryCreate? result = new();

                result = await _cloudinaryUti.CreateMedia(productDiscoveriesOfDayDTO.ImgProduct, "product-discoveries-of-day", 320, 320);

                if (result.ImgUrl == null || result.PublicId == null)
                    return ResultService.Fail<ProductDiscoveriesOfDayDTO>("error when create ImgProduct");

                productDiscoveriesOfDayDTO.SetImgProduct(result.ImgUrl);
                productDiscoveriesOfDayDTO.SetImgProductPublicId(result.PublicId);

                var id = Guid.NewGuid();

                productDiscoveriesOfDayDTO.SetId(id);

                var productCreate = await _productDiscoveriesOfDayRepository.CreateAsync(_mapper.Map<ProductDiscoveriesOfDay>(productDiscoveriesOfDayDTO));

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<ProductDiscoveriesOfDayDTO>(productCreate));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<ProductDiscoveriesOfDayDTO>(ex.Message);
            }
        }
    }
}
