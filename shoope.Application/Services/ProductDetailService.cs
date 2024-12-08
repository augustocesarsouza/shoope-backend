using AutoMapper;
using Shoope.Application.DTOs;
using Shoope.Application.DTOs.Validations.Interfaces;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;

namespace Shoope.Application.Services
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly IProductDetailRepository _productDetailRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductDetailCreateDTOValidator _productDetailCreateDTOValidator;

        public ProductDetailService(IProductDetailRepository productDetailRepository, IMapper mapper, IUnitOfWork unitOfWork,
            IProductDetailCreateDTOValidator productDetailCreateDTOValidator)
        {
            _productDetailRepository = productDetailRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productDetailCreateDTOValidator = productDetailCreateDTOValidator;
        }

        public async Task<ResultService<ProductDetailDTO>> GetProductDetailByProductId(Guid productId)
        {
            try
            {
                var productDetail = await _productDetailRepository.GetProductDetailByProductId(productId);

                return ResultService.Ok(_mapper.Map<ProductDetailDTO>(productDetail));

            }
            catch (Exception ex)
            {
                return ResultService.Fail<ProductDetailDTO>(ex.Message);
            }
        }

        public async Task<ResultService<ProductDetailDTO>> CreateAsync(ProductDetailDTO? productDetailDTO)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                if (productDetailDTO == null)
                    return ResultService.Fail<ProductDetailDTO>("error DTO is null");

                var validatorDTO = _productDetailCreateDTOValidator.ValidateDTO(productDetailDTO);

                if (!validatorDTO.IsValid)
                    return ResultService.RequestError<ProductDetailDTO>("validation error check the information", validatorDTO);

                var id = Guid.NewGuid();
                productDetailDTO.SetId(id);

                var productCreate = await _productDetailRepository.CreateAsync(_mapper.Map<ProductDetail>(productDetailDTO));

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<ProductDetailDTO>(productCreate));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<ProductDetailDTO>(ex.Message);
            }
        }
    }
}
