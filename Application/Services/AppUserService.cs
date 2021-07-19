using System.Threading.Tasks;
using AutoMapper;
using Core.Dtos;
using Core.Entities.Identities;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class AppUserService : DbService<int, AppUser, AppUserDto, IAppUserRepository>, IAppUserService
    {
        private readonly UserManager<AppUser> _userManeger;
        private IAddressService _addressService;

        public AppUserService(IMapper mapper, IUnitOfWork unitOfWork, UserManager<AppUser> userManeger) 
            : base(unitOfWork)//base(mapper, unitOfWork)
        {
            _userManeger = userManeger;
        }

        public IAddressService AddressService 
        {
            get
            {
                if(_addressService == null)
                {
                    _addressService = _unitOfWork.GetService(typeof(IAddressService)) as IAddressService;
                }
                return _addressService;
            }
        }
        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _userManeger.FindByEmailAsync(email) != null;
        }

        public async Task<AddressDto> GetUserAddress(int userId)
        {
            return await AddressService.FindByUserId(userId);
        }

        public async Task<IdentityResult> CreateCustomerAsync(AppUser user, string password)
        {
            return await CreateAsync(user, password, "Customer");
        }

        public async Task<IdentityResult> CreateAdminAsync(AppUser user, string password)
        {
            return await CreateAsync(user, password, "Admin");
        }

        protected async Task<IdentityResult> CreateAsync(AppUser user, string password, string roleName)
        {
            var ret = await _userManeger.CreateAsync(user, password);
            await _userManeger.AddToRoleAsync(user, roleName);
            return ret;
        }

        public async Task<AppUser> FindByEmailAsync(string email)
        {
            return await _repository.FindAsync(o => o.Email == email);
        }

        //public void UpdateAddress(AddressDto address)
        //{
        //    AddressService.Update(address);
        //}
    }
}