using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces.Repositories;
using Persistent.Repositories.Main;

namespace Persistent.Repositories.Db
{
    public class ProductRepository : BaseDbRepository<Product, ProductDto, int>, IProductRepository
    {
        public ProductRepository(DemoContext context) : base(context)
        {
        }

        protected override Expression<Func<Product, object>> ApplyDefaultSort()
        {
            return o => o.Name;
        }

        protected override IList<Expression<Func<Product, object>>>  ApplyDefaultInclude()
        {
            var includes = new List<Expression<Func<Product, object>>>()
            {
                { o => o.ProductBrand },
                { o => o.ProductType }
            };
            return includes;
        }
    }
}