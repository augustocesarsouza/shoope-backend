using Shoope.Domain.Repositories;
using Shoope.Infra.Data.Context;
using Shoope.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Shoope.Infra.Data.Repositories
{
    public class UserSellerProductRepository : GenericRepository<UserSellerProduct>, IUserSellerProductRepository
    {
        private readonly ApplicationDbContext _context;

        public UserSellerProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserSellerProduct?> GetById(Guid userSellerProductId)
        {
            var user = await _context.UserSellerProducts
                .Where(x => x.Id == userSellerProductId)
                .FirstOrDefaultAsync();

            return user;
        }
    }
}