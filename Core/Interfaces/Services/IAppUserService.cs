using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities.Identities;
using Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Core.Interfaces.Services
{
    public interface IAppUserService : IDbService<int, AppUser, AppUserDto, IAppUserRepository>
    {
        Task<bool> CheckEmailExistsAsync(string email);
        Task<AddressDto> GetUserAddress(int userId);
        Task<IdentityResult> CreateCustomerAsync(AppUser user, string password);
        Task<IdentityResult> CreateAdminAsync(AppUser user, string password);
        Task<AppUser> FindByEmailAsync(string email);
        //void UpdateAddress(AddressDto address);
    }
}