using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface IProductBrandRepository : IDbRepository<ProductBrand, ProductBrandDto, int>
    {
        
    }
}