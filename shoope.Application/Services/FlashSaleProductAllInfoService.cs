using AutoMapper;
using Shoope.Application.DTOs;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;

namespace Shoope.Application.Services
{
    public class FlashSaleProductAllInfoService : IFlashSaleProductAllInfoService
    {
        private readonly IFlashSaleProductAllInfoRepository _flashSaleProductAllInfoRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FlashSaleProductAllInfoService(IFlashSaleProductAllInfoRepository flashSaleProductAllInfoRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _flashSaleProductAllInfoRepository = flashSaleProductAllInfoRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultService<FlashSaleProductAllInfoDTO>> GetFlashSaleProductByProductFlashSaleId(Guid productFlashSaleId)
        {
            try
            {
                var productFlashSale = await _flashSaleProductAllInfoRepository.GetFlashSaleProductByProductFlashSaleId(productFlashSaleId);

                return ResultService.Ok(_mapper.Map<FlashSaleProductAllInfoDTO>(productFlashSale));

            }
            catch (Exception ex)
            {
                return ResultService.Fail<FlashSaleProductAllInfoDTO>(ex.Message);
            }
        }

        public async Task<ResultService<FlashSaleProductAllInfoDTO>> CreateAsync(FlashSaleProductAllInfoDTO? flashSaleProductAllInfoDTO)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                if (flashSaleProductAllInfoDTO == null)
                    return ResultService.Fail<FlashSaleProductAllInfoDTO>("error DTO is null");

                var id = Guid.NewGuid();
                flashSaleProductAllInfoDTO.SetId(id);

                var productCreate = await _flashSaleProductAllInfoRepository.CreateAsync(_mapper.Map<FlashSaleProductAllInfo>(flashSaleProductAllInfoDTO));

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<FlashSaleProductAllInfoDTO>(productCreate));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<FlashSaleProductAllInfoDTO>(ex.Message);
            }
        }
    }
}
