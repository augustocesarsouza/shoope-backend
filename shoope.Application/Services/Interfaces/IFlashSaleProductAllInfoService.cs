using Shoope.Application.DTOs;

namespace Shoope.Application.Services.Interfaces
{
    public interface IFlashSaleProductAllInfoService
    {
        public Task<ResultService<FlashSaleProductAllInfoDTO>> GetFlashSaleProductByProductFlashSaleId(Guid productFlashSaleId);
        public Task<ResultService<FlashSaleProductAllInfoDTO>> CreateAsync(FlashSaleProductAllInfoDTO? flashSaleProductAllInfoDTO);
    }
}
