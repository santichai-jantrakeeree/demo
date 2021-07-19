using Core.Data;
using Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IDbService : IService
    {

    }

    public interface IDbService<TKey, TEntity, TDto, TRepository> : IDbService
        where TKey: IEquatable<TKey>
        where TEntity: class, IEntity<TKey>, new()
        where TDto: class, IDto<TKey>, new()
        where TRepository: IDbRepository<TEntity, TDto, TKey>
    {
        /// <summary>
        /// find and delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> Delete(TKey id);
        /// <summary>
        /// delete only without find
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        Task<TEntity> Find(TKey id);
        Task<IList<TEntity>> List();
        Task<DataResult<TEntity>> List(DataSource dataSource);
    }
}