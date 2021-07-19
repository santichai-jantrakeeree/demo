using Core.Dtos;
using Core.Entities.Identities;

namespace Core.Interfaces.Repositories
{
    public interface IAppUserRepository : IDbRepository<AppUser, AppUserDto, int>
    {
        
    }
}