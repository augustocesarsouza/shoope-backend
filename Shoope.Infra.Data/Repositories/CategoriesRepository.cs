using Microsoft.EntityFrameworkCore;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.Context;

namespace Shoope.Infra.Data.Repositories
{
    public class CategoriesRepository : GenericRepository<Categories>, ICategoriesRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoriesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Categories?> GetCategoriesById(Guid categorieId)
        {
            var categorie = await _context
               .Categories
               .Where(x => x.Id == categorieId)
               .FirstOrDefaultAsync();

            return categorie;
        }

        public async Task<List<Categories>?> GetAllCategories()
        {
            var categories = await _context
               .Categories
               .Select(x => new Categories(x.Id, x.ImgCategory, null, x.AltValue, x.Title))
               .ToListAsync();

            return categories;
        }
    }
}
