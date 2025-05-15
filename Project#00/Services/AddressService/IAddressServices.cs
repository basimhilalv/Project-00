using Project_00.Dtos;
using Project_00.Models;

namespace Project_00.Services.AddressService
{
    public interface IAddressServices
    {
        public Task<Address> GetAddress(int id, Guid userId);
        public Task<IEnumerable<Address>> GetAllAddress(Guid userId);
        public Task<Address> AddAddress(AddressDto request, Guid userId);
        public Task<Address> UpdateAddress(int id, AddressDto request, Guid userId);
        public Task<Address> DeleteAddress(int id, Guid userId);
    }
}
