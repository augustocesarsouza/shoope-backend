using Microsoft.EntityFrameworkCore;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.Context;

namespace Shoope.Infra.Data.Repositories
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly ApplicationDbContext _context;

        public PromotionRepository(ApplicationDbContext context)
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

        public async Task<Promotion?> Create(Promotion promotion)
        {
            await _context.Promotion.AddAsync(promotion);
            await _context.SaveChangesAsync();

            return promotion;
        }

        public async Task<Promotion> Delete(Promotion promotion)
        {
            _context.Promotion.Remove(promotion);
            await _context.SaveChangesAsync();
            return promotion;
        }
    }
}
