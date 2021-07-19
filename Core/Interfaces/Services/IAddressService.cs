using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities.Identities;
using Core.Interfaces.Repositories;

namespace Core.Interfaces.Services
{
    public interface IAddressService : IDbService<int, Address, AddressDto, IAddressRepository>
    {
        Task<AddressDto> FindByUserId(int userId);
    }
}