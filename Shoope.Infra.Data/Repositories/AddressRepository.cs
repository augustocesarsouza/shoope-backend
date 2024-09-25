using CloudinaryDotNet.Core;
using Microsoft.EntityFrameworkCore;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.Context;

namespace Shoope.Infra.Data.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Address?> GetAddressById(Guid addressId)
        {
            var addressGet = await _context
                .Addresses
                .Where(a => a.Id == addressId)
                .FirstOrDefaultAsync();

            return addressGet;
        }

        public async Task<Address?> GetAddressByUserId(Guid userId)
        {
            var addressGet = await _context
                .Addresses
                .Where(a => a.UserId == userId)
                .Select(x => new Address(x.Id, x.FullName, x.PhoneNumber, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, Guid.Empty))
                .FirstOrDefaultAsync();

            return addressGet;
        }

        public async Task<Address?> CreateAsync(Address address)
        {
            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();

            return address;

        }

        public async Task<Address?> Delete(Address address)
        {
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public async Task<Address?> UpdateUser(Address address)
        {
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();

            return address;
        }
    }
}
