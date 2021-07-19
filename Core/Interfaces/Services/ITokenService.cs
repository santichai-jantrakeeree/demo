using Core.Dtos;
using Core.Entities.Identities;

namespace Core.Interfaces.Services
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
        string CreateToken(AppUserDto user);
    }
}