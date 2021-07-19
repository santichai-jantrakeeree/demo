using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Data;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Core.Interfaces.Services
{
    public interface IProductService : IDbService<int, Product, ProductDto, IProductRepository>
    {
        Task<DataResult<Product>> List(DataSource dataSource, int brandId, int typeId, string search);
        Task<IReadOnlyList<Product>> List(int[] ids);
    }
}