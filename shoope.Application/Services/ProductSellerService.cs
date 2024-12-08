using AutoMapper;
using Shoope.Application.DTOs;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;

namespace Shoope.Application.Services
{
    public class ProductSellerService : IProductSellerService
    {
        private readonly IProductSellerRepository _productSellerRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductSellerService(IProductSellerRepository productSellerRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _productSellerRepository = productSellerRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultService<ProductSellerDTO>> GetById(Guid productId)
        {
            try
            {
                var resultGet = await _productSellerRepository.GetById(productId);

                return ResultService.Ok(_mapper.Map<ProductSellerDTO>(resultGet));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<ProductSellerDTO>(ex.Message);
            }
        }

        public async Task<ResultService<ProductSellerDTO>> Create(ProductSellerDTO? productSellerDTO)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                if (productSellerDTO == null)
                    return ResultService.Fail<ProductSellerDTO>("error DTO informed is Null");

                var productSellerDTOId = Guid.NewGuid();
                productSellerDTO.SetId(productSellerDTOId);

                var createproductSeller = await _productSellerRepository.CreateAsync(_mapper.Map<ProductSeller>(productSellerDTO));

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<ProductSellerDTO>(createproductSeller));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<ProductSellerDTO>(ex.Message);
            }
        }
    }
}
