using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Data;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistent.Repositories.Main
{
    public class BaseDbRepository<TEntity, TDto, TKey> : IDbRepository<TEntity, TDto, TKey>
        where TEntity : class, IEntity<TKey>, new()
        where TDto : class, IDto<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        protected readonly DemoContext _context;
        private bool _useDescToDefaultSort = false;
        
        public BaseDbRepository(DemoContext context)
        {
            _context = context;
        }

        protected virtual IList<Expression<Func<TEntity, object>>>  ApplyDefaultInclude()
        {
            return null;
        }

        protected virtual void SetDefaultSortDirection(bool useDescToDefaultSort = false)
        {
            _useDescToDefaultSort = useDescToDefaultSort;
        }

        protected virtual Expression<Func<TEntity, object>> ApplyDefaultSort()
        {
            return null;
        }

        public IQueryable<TEntity> GetQuery()
        {
            var query = _context.Set<TEntity>().AsQueryable();
            var includes = ApplyDefaultInclude();
            if(includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }
        
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Add(IList<TEntity> entities)
        {
            foreach(var entity in entities)
            {
                Add(entity);
            }
        }

        // public async Task<int> CountAsync(ISpecification<TEntity> spec)
        // {
        //      return await ApplySpecification(spec).CountAsync();
        // }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void Delete(IList<TEntity> entities)
        {
            foreach(var entity in entities)
            {
                Delete(entity);
            }
        }

        public async Task Delete(TKey id)
        {
            TEntity entity = await _context.Set<TEntity>().FindAsync(id);
            Delete(entity);
        }

        public async Task<TEntity> FindAsync(TKey id)
        {
            return await FindAsync(o => o.Id.Equals(id));
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> criteria)
        {
            //return await _context.Set<TEntity>().FindAsync(criteria);
            return await GetQuery().FirstOrDefaultAsync(criteria);
        }

        public async Task<IList<TEntity>> ListAsync()
        {
            return await GetQuery().ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> ListAsync(Expression<Func<TEntity, bool>> criteria)
        {
            return await GetQuery().Where(criteria).ToListAsync();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

        }

        public void Update(IList<TEntity> entities)
        {
            foreach(var entity in entities)
            {
                Update(entity);
            }
        }

        public Task<DataResult<TEntity>> ListAsync(DataSource dataSource)
        {
            return ListAsync(dataSource, o => true);
        }

        public async Task<DataResult<TEntity>> ListAsync(DataSource dataSource, Expression<Func<TEntity, bool>> criteria)
        {            
            if(dataSource == null)
                dataSource = new DataSource();

            var query = GetQuery();
            query = query.Where(criteria);
            var count = await query.CountAsync();

            query = ApplySort(query, dataSource);
            query = ApplyQuery(query, dataSource);

            var list = await query.ToListAsync();
            DataResult<TEntity> result = new DataResult<TEntity>
            {
                PageIndex = dataSource.PageIndex,
                Count = count,
                Data = list,
                PageSize = dataSource.PageSize
            };
            return result;
        }

        private IQueryable<TEntity> ApplyQuery(IQueryable<TEntity> query, DataSource dataSource)
        {
            var inputQuery = query;

            inputQuery = inputQuery.Skip(dataSource.PageSize * (dataSource.PageIndex - 1));
            inputQuery = inputQuery.Take(dataSource.PageSize);

            return inputQuery;
        }

        private IQueryable<TEntity> ApplySort(IQueryable<TEntity> query, DataSource dataSource)
        {
            var inputQuery = query;
            var orderBy = CreateOrderByQueryString(dataSource);
            if(!string.IsNullOrEmpty(orderBy))
            {
                inputQuery = inputQuery.OrderBy(orderBy);
            }
            else
            {
                var defaultSort = ApplyDefaultSort();
                if(defaultSort != null)
                {
                    inputQuery = _useDescToDefaultSort 
                        ? inputQuery.OrderByDescending(defaultSort) 
                        : inputQuery.OrderBy(defaultSort);
                }
            }

            return inputQuery;
        }

        private string CreateOrderByQueryString(DataSource dataSource)
        {
            if(string.IsNullOrWhiteSpace(dataSource.SortBy))
                return string.Empty;

            StringBuilder builder = new StringBuilder();

            var sortByArr = dataSource.SortBy.Split(',').Select(s => s.Trim()).ToArray();
            var sortDirArr = !(string.IsNullOrWhiteSpace(dataSource.SortDir))
                                ? dataSource.SortDir.Split(',').Select(s => s.Trim()).ToArray()
                                : new string[sortByArr.Length];
            
            var properties = typeof(TEntity).GetProperties();
            for(int i = 0; i < sortByArr.Length; i++)
            {
                var target = properties.FirstOrDefault(o => o.Name.ToLower() == sortByArr[i].ToLower());
                if(target != null)
                {
                    var dir = (sortDirArr.Length < i + 1 
                                ? string.Empty : 
                                (sortDirArr[i] != null && sortDirArr[i].ToLower() == "desc" ? "desc" : string.Empty ));
                    builder.Append($"{target.Name} {dir},");
                }
            }
            var result = builder.Length > 0 
                            ? builder.ToString(0, builder.Length - 1) 
                            : string.Empty;
            return result;            
        }

        // private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        // {
        //     return SpecificationEvaluator<TEntity, TKey>.GetQueryable(_context.Set<TEntity>().AsQueryable(), spec);
        // }
    }
}