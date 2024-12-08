using Microsoft.EntityFrameworkCore;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.Context;

namespace Shoope.Infra.Data.Repositories
{
    public class PromotionRepository : GenericRepository<Promotion>, IPromotionRepository
    {
        private readonly ApplicationDbContext _context;

        public PromotionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Promotion?> GetById(Guid promotionId)
        {
            var promotion = await _context.Promotion
                .Where(x => x.Id == promotionId)
                .FirstOrDefaultAsync();

            return promotion;
        }
    }
}
