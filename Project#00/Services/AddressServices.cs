using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project_00.Data;
using Project_00.Dtos;
using Project_00.Models;
using Project_00.Services.Interfaces;

namespace Project_00.Services
{
    public class AddressServices : IAddressServices
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        public AddressServices(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Address> AddAddress(AddressDto request, Guid userId)
        {
            try
            {
                var address = _mapper.Map<Address>(request);
                address.UserId = userId;
                _context.Addresses.Add(address);
                await _context.SaveChangesAsync();
                return address;
            }catch(Exception ex)
            {
                throw new Exception("Error occured while adding data", ex);
            }
        }

        public async Task<Address> DeleteAddress(int id, Guid userId)
        {
            try
            {
                var address = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
                if (address is null) return null;
                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync();
                return address;
            }catch(Exception ex)
            {
                throw new Exception("Error occured while deleting data", ex);
            }
        }

        public async Task<Address> GetAddress(int id, Guid userId)
        {
            try
            {
                var address = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
                if (address is null) return null;
                return address;
            }catch(Exception ex)
            {
                throw new Exception("Error occured while fetching data", ex);
            }
        }

        public async Task<IEnumerable<Address>> GetAllAddress(Guid userId)
        {
            try
            {
                var addresses = await _context.Addresses.Where(a => a.UserId == userId).ToListAsync();
                if (addresses is null) return null;
                return addresses;
            }catch(Exception ex)
            {
                throw new Exception("Error occured while fetching data", ex);
            }
        }

        public async Task<Address> UpdateAddress(int id, AddressDto request, Guid userId)
        {
            try
            {
                var address = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
                if (address is null) return null;
                address.Street = request.Street;
                address.State = request.State;
                address.City = request.City;
                address.Country = request.Country;
                address.ZipCode = request.ZipCode;
                await _context.SaveChangesAsync();
                return address;
            }catch(Exception ex)
            {
                throw new Exception("Error occured while fetching data", ex);
            }
        }
    }
}
