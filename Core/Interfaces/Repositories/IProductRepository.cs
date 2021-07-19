using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface IProductRepository : IDbRepository<Product, ProductDto, int>
    {
        
    }
}