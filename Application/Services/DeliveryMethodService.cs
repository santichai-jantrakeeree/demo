using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Application.Services
{
    public class DeliveryMethodService : DbService<int, DeliveryMethod, DeliveryMethodDto, IDeliveryMethodRepository>, IDeliveryMethodService
    {
        public DeliveryMethodService(IMapper mapper, IUnitOfWork unitOfWork) : base(unitOfWork)//base(mapper, unitOfWork)
        {
        }
    }
}