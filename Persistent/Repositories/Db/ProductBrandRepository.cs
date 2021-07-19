using Core.Dtos;
using Core.Entities;
using Core.Interfaces.Repositories;
using Persistent.Repositories.Main;

namespace Persistent.Repositories.Db
{
    public class ProductBrandRepository : BaseDbRepository<ProductBrand, ProductBrandDto, int>, IProductBrandRepository
    {
        public ProductBrandRepository(DemoContext context) : base(context)
        {
        }
    }
}