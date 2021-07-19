using Core.Dtos;
using Core.Entities.Identities;
using Core.Interfaces.Repositories;
using Persistent.Repositories.Main;

namespace Persistent.Repositories.Db
{
    public class AppUserRepository : BaseDbRepository<AppUser, AppUserDto, int>, IAppUserRepository
    {
        public AppUserRepository(DemoContext context) : base(context)
        {
        }
    }
}