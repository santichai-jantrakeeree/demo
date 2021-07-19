using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Application.Services
{
    public class ProductBrandService : DbService<int, ProductBrand, ProductBrandDto, IProductBrandRepository>, IProductBrandService
    {
        public ProductBrandService(IMapper mapper, IUnitOfWork unitOfWork) : base(unitOfWork)//base(mapper, unitOfWork)
        {
        }
    }
}