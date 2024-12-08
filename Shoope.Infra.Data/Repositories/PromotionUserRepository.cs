using Microsoft.EntityFrameworkCore;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.Context;

namespace Shoope.Infra.Data.Repositories
{
    public class PromotionUserRepository : GenericRepository<PromotionUser>, IPromotionUserRepository
    {
        private readonly ApplicationDbContext _context;

        public PromotionUserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<PromotionUser>> GetById(Guid guidId)
        {
            var promotionUser = await _context
                .PromotionUser
                .Where(x => x.UserId == guidId && x.Promotion != null && x.Promotion.WhatIsThePromotion == 1)
                .Select(s => new PromotionUser(null,
                    s.Promotion != null
                    ? new Promotion(null, s.Promotion.WhatIsThePromotion, s.Promotion.Title, s.Promotion.Description,
                    s.Promotion.Date, s.Promotion.Img, string.Empty)
                    : null, null))
                .ToListAsync();

            return promotionUser;
        }

        public async Task<List<PromotionUser>> GetByUserIdAll(Guid userId)
        {
            var promotionUser = await _context
                .PromotionUser
                .Where(x => x.UserId == userId)
                .Select(s => new PromotionUser(s.Id,
                    s.Promotion != null
                    ? new Promotion(null, s.Promotion.WhatIsThePromotion, s.Promotion.Title, s.Promotion.Description,
                    s.Promotion.Date, s.Promotion.Img, null, s.Promotion.ImgInnerFirst, s.Promotion.AltImgInnerFirst,
                    s.Promotion.ImgInnerSecond, s.Promotion.AltImgInnerSecond, s.Promotion.ImgInnerThird, s.Promotion.AltImgInnerThird,
                    null, null, null)
                    : null, null))
                .ToListAsync();

            return promotionUser;
        }

        public async Task<List<PromotionUser>> GetPromotionUserByPromotionId(Guid promotionId)
        {
            var promotionUser = await _context
                .PromotionUser
                .Where(x => x.PromotionId == promotionId)
                .ToListAsync();

            return promotionUser;
        }
    }
}
