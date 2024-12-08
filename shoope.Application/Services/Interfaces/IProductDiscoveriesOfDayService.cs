using Shoope.Application.DTOs;

namespace Shoope.Application.Services.Interfaces
{
    public interface IProductDiscoveriesOfDayService
    {
        public Task<ResultService<ProductDiscoveriesOfDayDTO>> GetProductDiscoveriesOfDayById(Guid productDiscoveriesOfDayId);
        public Task<ResultService<List<ProductDiscoveriesOfDayDTO>>> GetAllProductDiscoveriesOfDays();
        public Task<ResultService<ProductDiscoveriesOfDayDTO>> CreateAsync(ProductDiscoveriesOfDayDTO? productDiscoveriesOfDayDTO);
    }
}
