using Core.Dtos;
using Core.Entities.Identities;
using Core.Interfaces.Repositories;
using Persistent.Repositories.Main;

namespace Persistent.Repositories.Db
{
    public class AddressRepository : BaseDbRepository<Address, AddressDto, int>, IAddressRepository
    {
        public AddressRepository(DemoContext context) : base(context)
        {
        }
    }
}