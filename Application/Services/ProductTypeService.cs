using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Application.Services
{
    public class ProductTypeService : DbService<int, ProductType, ProductTypeDto, IProductTypeRepository>, IProductTypeService
    {
        public ProductTypeService(IMapper mapper, IUnitOfWork unitOfWork) : base(unitOfWork)//base(mapper, unitOfWork)
        {
        }
    }
}