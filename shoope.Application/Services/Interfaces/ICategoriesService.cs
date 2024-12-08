using Shoope.Application.DTOs;

namespace Shoope.Application.Services.Interfaces
{
    public interface ICategoriesService
    {
        public Task<ResultService<CategoriesDTO>> GetCategoriesById(Guid categorieId);
        public Task<ResultService<List<CategoriesDTO>>> GetAllCategories();
        public Task<ResultService<CategoriesDTO>> CreateAsync(CategoriesDTO? categoriesDTO);
    }
}
