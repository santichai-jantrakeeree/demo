using Core.Dtos;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Core.Interfaces.Services
{
    public interface IProductBrandService : IDbService<int, ProductBrand, ProductBrandDto, IProductBrandRepository>
    {
        
    }
}