using System.Threading.Tasks;
using AutoMapper;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Application.Services
{
    public class RedisService<TEntity, TDto, TRepository> : IRedisService<TEntity, TDto, TRepository>
        where TEntity : class, IEntity<string>, new()
        where TDto : IDto<string>
        where TRepository : class, IRedisRepository<TEntity>
    {
        private readonly TRepository _repository;
        private readonly IMapper _mapper;
        public RedisService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = unitOfWork.GetRepository(typeof(TRepository)) as TRepository;

        }

        public async Task<bool> Delete(string id)
        {
            return await _repository.Delete(id);
        }

        public async Task<TDto> Find(string id)
        {
            var entity = await _repository.FindAsync(id);
            return _mapper.Map<TEntity, TDto>(entity);
        }

        public async Task<TDto> Upsert(TDto dto)
        {
            var entity = _mapper.Map<TDto, TEntity>(dto);
            var target = await _repository.Upsert(entity);
            return _mapper.Map<TEntity, TDto>(entity);
        }
    }
}