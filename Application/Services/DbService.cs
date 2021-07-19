using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.Data;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Application.Services
{
    public class DbService<TKey, TEntity, TDto, TRepository> : IDbService<TKey, TEntity, TDto, TRepository>
        where TEntity : class, IEntity<TKey>, new()
        where TDto : class, IDto<TKey>, new()
        where TRepository : class, IDbRepository<TEntity, TDto, TKey>
        where TKey : IEquatable<TKey>

    {
        //protected readonly IMapper _mapper;
        protected readonly TRepository _repository;
        protected readonly IUnitOfWork _unitOfWork;        

        public DbService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository(typeof(TRepository)) as TRepository;
        }

        public virtual TEntity Add(TEntity entity)
        {
            //TEntity entity = _mapper.Map<TDto, TEntity>(dto);
            _repository.Add(entity);
            return entity;
        }

        public virtual async Task<TEntity> Delete(TKey id)
        {
            var entity = await _repository.FindAsync(id);
            this.Delete(entity);
            return entity;
        }

        public virtual void Delete(TEntity entity)
        {
            _repository.Delete(entity);
        }

        public virtual async Task<TEntity> Find(TKey id)
        {
            //var entity = await _repository.FindAsync(id);
            //return _mapper.Map<TEntity, TDto>(entity);
            return await _repository.FindAsync(id);
        }

        public virtual async Task<IList<TEntity>> List()
        {
            //var entities = await _repository.ListAsync();
            //return _mapper.Map<IReadOnlyList<TEntity>, IReadOnlyList<TDto>>(entities);
            return await _repository.ListAsync();
        }

        public virtual async Task<DataResult<TEntity>> List(DataSource dataSource)
        {
            return await _repository.ListAsync(dataSource);
        }

        //public virtual async Task<TEntity> Update(TEntity dto)
        //{
        //    //var entity = await _repository.FindAsync(dto.Id);
        //    //var target = _mapper.Map<TDto, TEntity>(dto, entity);
        //    //_repository.Update(target);
        //    //return entity;
        //}

        public virtual TEntity Update(TEntity entity)
        {
            _repository.Update(entity);
            return entity;
        }
    }
}