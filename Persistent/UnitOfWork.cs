using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Identities;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Persistent
{
    public class UnitOfWork : IUnitOfWork
    {
        private int? _currentUserId;
        private string _currentUserEmail;
        private AppUser _currentUser;
        private readonly DemoContext _context;
        private Hashtable _repositories;
        private Hashtable _services;
        private IServiceProvider _serviceProvider;
        private readonly UserManager<AppUser> _userManager;
        private bool _loadUserFlag;

        public UnitOfWork(DemoContext context, IServiceProvider serviceProvider, UserManager<AppUser> userManager)
        {
            _serviceProvider = serviceProvider;
            _userManager = userManager;
            _context = context;
            _repositories = new Hashtable();
            _services = new Hashtable();
        }

        //public UnitOfWork(IServiceProvider serviceProvider)
        //{
        //    _serviceProvider = serviceProvider;
        //    IServiceScope scope = _serviceProvider.CreateScope();
        //    _context = scope.ServiceProvider.GetRequiredService<DemoContext>();
        //    _repositories = new Hashtable();
        //    _services = new Hashtable();

        //}
        public int? CurrentUserId
        {
            get
            {
                return _currentUserId;
            }
        }

        public string CurrentUserEmail
        {
            get
            {
                return _currentUserEmail;
            }
        }

        public async Task<AppUser> GetCurrentUser()
        {
            if (_currentUser == null && _currentUserId != null && !_loadUserFlag)
            {
                var userService = this.GetService(typeof(IAppUserService)) as IAppUserService;
                _currentUser = await userService.Find(_currentUserId.Value);
            }
            _loadUserFlag = true;
            return _currentUser;
        }

        public async Task<IList<string>> GetUserRoles()
        {
            if(!_loadUserFlag)
            {
                _currentUser = await GetCurrentUser();
            }
            if(_currentUser == null)
            {
                return new List<string>();
            }
            else
            {
                return await _userManager.GetRolesAsync(_currentUser);
            }            
        }

        public void SetCurrentUserId(int userId)
        {
            _currentUserId = userId;
        }

        public void SetCurrentUserEmail(string email)
        {
            _currentUserEmail = email;
        }

        public async Task<int> Commit()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public object GetRepository(Type repositoryType)
        {
            if (!(typeof(IRepository).IsAssignableFrom(repositoryType)))
                throw new Exception("We only resolve sub type of IDbRepository in unit of work.");

            var typeName = repositoryType.Name;
            if (!_repositories.ContainsKey(typeName))
            {
                var repositoryInstance = _serviceProvider.GetService(repositoryType);

                _repositories.Add(typeName, repositoryInstance);
            }

            return _repositories[typeName];
        }

        public object GetService(Type serviceType)
        {
            if (!(typeof(IService).IsAssignableFrom(serviceType)))
                throw new Exception("We only resolve sub type of IService in unit of work.");

            var typeName = serviceType.Name;
            if (!_services.ContainsKey(typeName))
            {
                var serviceInstance = _serviceProvider.GetService(serviceType);
                _services.Add(typeName, serviceInstance);
            }

            return _services[typeName];
        }
    }
}