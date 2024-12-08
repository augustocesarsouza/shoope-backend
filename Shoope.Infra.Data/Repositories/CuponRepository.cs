using Microsoft.EntityFrameworkCore;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.Context;
using System;

namespace Shoope.Infra.Data.Repositories
{
    public class CuponRepository : GenericRepository<Cupon>, ICuponRepository
    {
        private readonly ApplicationDbContext _context;

        public CuponRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Cupon?> GetCuponById(Guid cuponId)
        {
            var cupon = await _context
                .Cupons
                .Where(x => x.Id == cuponId)
                .FirstOrDefaultAsync();

            return cupon;
        }
    }
}
