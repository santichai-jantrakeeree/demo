using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface IProductTypeRepository : IDbRepository<ProductType, ProductTypeDto, int>
    {
        
    }
}