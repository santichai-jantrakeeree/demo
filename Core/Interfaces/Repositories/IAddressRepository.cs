using Core.Dtos;
using Core.Entities.Identities;

namespace Core.Interfaces.Repositories
{
    public interface IAddressRepository : IDbRepository<Address, AddressDto, int>
    {
        
    }
}