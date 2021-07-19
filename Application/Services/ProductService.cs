using System;
using System.Threading.Tasks;
using AutoMapper;
using Core.Data;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Application.Services
{
    public class ProductService : DbService<int, Product, ProductDto, IProductRepository>, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<DataResult<Product>> List(DataSource dataSource, int brandId, int typeId, string search)
        {
            return _repository.ListAsync(dataSource,
                o => (brandId == 0 || o.ProductBrandId == brandId)
                && (typeId == 0 || o.ProductTypeId == typeId)
                && (string.IsNullOrEmpty(search) || o.Name.Contains(search))
            );
        }

        public async Task<IReadOnlyList<Product>> List(int[] ids)
        {
            return await _repository.ListAsync(o => ids.Contains(o.Id));
        }
    }
}