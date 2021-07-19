using Core.Dtos;
using Core.Entities;
using Core.Interfaces.Repositories;
using Persistent.Repositories.Main;

namespace Persistent.Repositories.Db
{
    public class ProductTypeRepository : BaseDbRepository<ProductType, ProductTypeDto, int>, IProductTypeRepository
    {
        public ProductTypeRepository(DemoContext context) : base(context)//base(context, mapper)
        {
        }
    }
}