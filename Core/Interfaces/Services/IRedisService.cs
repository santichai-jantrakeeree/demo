using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Interfaces.Repositories;

namespace Core.Interfaces.Services
{
    public interface IRedisService : IService
    {
        
    }

    public interface IRedisService<TEntity, TDto, TRepository> : IRedisService
        where TEntity: class, IEntity<string>, new()
        where TDto: IDto<string>
        where TRepository: IRedisRepository<TEntity>
    {
        Task<bool> Delete(string id);
        Task<TDto> Upsert(TDto dto);
        Task<TDto> Find(string id);
    }
}