using System.Threading.Tasks;
using AutoMapper;
using Core.Dtos;
using Core.Entities.Identities;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Application.Services
{
    public class AddressService : DbService<int, Address, AddressDto, IAddressRepository>, IAddressService
    {
        public AddressService(IMapper mapper, IUnitOfWork unitOfWork) : base(unitOfWork)//base(mapper, unitOfWork)
        {
        }

        public async Task<AddressDto> FindByUserId(int userId)
        {
            var entity = await _repository.FindAsync(o => o.UserId == userId);
            return null;// _mapper.Map<Address, AddressDto>(entity);
        }
    }
}