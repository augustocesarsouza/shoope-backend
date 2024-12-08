using Microsoft.EntityFrameworkCore;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.Context;

namespace Shoope.Infra.Data.Repositories
{
    public class UserCuponRepository : GenericRepository<UserCupon>, IUserCuponRepository
    {
        private readonly ApplicationDbContext _context;

        public UserCuponRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<UserCupon>> GetAllCuponByUserId(Guid userId)
        {
            var promotionUser = await _context
                .UserCupons
                .Where(x => x.UserId == userId)
                .Select(s => new UserCupon(null, null,
                    s.Cupon != null
                    ? new Cupon(s.Cupon.Id, s.Cupon.FirstText, s.Cupon.SecondText, s.Cupon.ThirdText, s.Cupon.DateValidateCupon,
                    s.Cupon.QuantityCupons, s.Cupon.WhatCuponNumber, s.Cupon.SecondImg, s.Cupon.SecondImgAlt)
                    : null, null, null))
                .ToListAsync();

            return promotionUser;
        }

        //public async Task<UserCupon> Create(UserCupon userCupon)
        //{
        //    await _context.UserCupons.AddAsync(userCupon);
        //    await _context.SaveChangesAsync();

        //    return userCupon;
        //}
    }
}