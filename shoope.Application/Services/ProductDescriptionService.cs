using AutoMapper;
using Shoope.Application.DTOs;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;

namespace Shoope.Application.Services
{
    public class ProductDescriptionService : IProductDescriptionService
    {
        private readonly IProductDescriptionRepository _productDescriptionRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductDescriptionService(IProductDescriptionRepository productDescriptionRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _productDescriptionRepository = productDescriptionRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultService<ProductDescriptionDTO>> GetProductDescriptionByProductId(Guid productId)
        {
            try
            {
                var product = await _productDescriptionRepository.GetProductDescriptionByProductId(productId);

                return ResultService.Ok(_mapper.Map<ProductDescriptionDTO>(product));

            }
            catch (Exception ex)
            {
                return ResultService.Fail<ProductDescriptionDTO>(ex.Message);
            }
        }

        public async Task<ResultService<ProductDescriptionDTO>> CreateAsync(ProductDescriptionDTO? productDescriptionDTO)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                if (productDescriptionDTO == null)
                    return ResultService.Fail<ProductDescriptionDTO>("error DTO is null");

                //var validatorDTO = _productDetailCreateDTOValidator.ValidateDTO(productDetailDTO);

                //if (!validatorDTO.IsValid)
                //    return ResultService.RequestError<ProductDetailDTO>("validation error check the information", validatorDTO);

                var id = Guid.NewGuid();
                productDescriptionDTO.SetId(id);

                var productCreate = await _productDescriptionRepository.CreateAsync(_mapper.Map<ProductDescription>(productDescriptionDTO));

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<ProductDescriptionDTO>(productCreate));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<ProductDescriptionDTO>(ex.Message);
            }
        }
    }
}
