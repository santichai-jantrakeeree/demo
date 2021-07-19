using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Data;

namespace Core.Interfaces.Repositories
{
    public interface IDbRepository : IRepository
    {
        
    }

    public interface IDbRepository<TEntity, TDto, TKey> : IDbRepository
        where TEntity : class, IEntity<TKey>, new()
        where TDto : class, IDto<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        IQueryable<TEntity> GetQuery();
        Task<TEntity> FindAsync(TKey id);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> criteria);
        Task<IList<TEntity>> ListAsync();
        Task<IReadOnlyList<TEntity>> ListAsync(Expression<Func<TEntity, bool>> criteria);
        Task<DataResult<TEntity>> ListAsync(DataSource dataSource);
        Task<DataResult<TEntity>> ListAsync(DataSource dataSource, Expression<Func<TEntity, bool>> criteria);
        //Task<TEntity> FindAsync(ISpecification<TEntity> scec);
        //Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> scec);
        //Task<int> CountAsync(ISpecification<TEntity> spec);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Add(IList<TEntity> entities);
        void Update(IList<TEntity> entities);
        void Delete(IList<TEntity> entities);
        Task Delete(TKey id);
    }
}