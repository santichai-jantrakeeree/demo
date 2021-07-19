using System;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IRedisRepository : IRepository
    {
        
    }

    public interface IRedisRepository<TEntity> : IRedisRepository
        where TEntity: class, IEntity<string>, new()
    {
        Task<TEntity> FindAsync(string id);
        Task<bool> Delete(string id);
        Task<TEntity> Upsert(TEntity entity);
    }
}