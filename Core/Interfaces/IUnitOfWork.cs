using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Identities;
using Core.Interfaces.Repositories;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Commit();
        object GetRepository(Type repositoryType);
        object GetService(Type serviceType);
        int? CurrentUserId { get; }
        string CurrentUserEmail { get; }
        void SetCurrentUserId(int userId);
        void SetCurrentUserEmail(string email);
        Task<AppUser> GetCurrentUser();
        Task<IList<string>> GetUserRoles();
    }
}