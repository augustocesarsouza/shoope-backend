using Microsoft.EntityFrameworkCore;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.Context;
using System;

namespace Shoope.Infra.Data.Repositories
{
    public class CuponRepository : ICuponRepository
    {
        private readonly ApplicationDbContext _context;

        public CuponRepository(ApplicationDbContext context)
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

        public async Task<Cupon?> CreateAsync(Cupon cupon)
        {
            await _context.Cupons.AddAsync(cupon);
            await _context.SaveChangesAsync();

            return cupon;
        }
    }
}
