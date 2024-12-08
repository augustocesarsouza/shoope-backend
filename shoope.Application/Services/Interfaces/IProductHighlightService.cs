using Shoope.Application.DTOs;

namespace Shoope.Application.Services.Interfaces
{
    public interface IProductHighlightService
    {
        public Task<ResultService<ProductHighlightDTO>> GetProductHighlightById(Guid productHighlightId);
        public Task<ResultService<List<ProductHighlightDTO>>> GetAllProductHighlights();
        public Task<ResultService<ProductHighlightDTO>> CreateAsync(ProductHighlightDTO? productHighlightDTO);
    }
}
