using Shoope.Domain.Entities;

namespace Shoope.Domain.Repositories
{
    public interface ICategoriesRepository : IGenericRepository<Categories>
    {
        public Task<Categories?> GetCategoriesById(Guid categorieId);
        public Task<List<Categories>?> GetAllCategories();
    }
}
