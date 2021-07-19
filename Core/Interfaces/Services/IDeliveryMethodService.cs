using Core.Dtos;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Core.Interfaces.Services
{
    public interface IDeliveryMethodService : IDbService<int, DeliveryMethod, DeliveryMethodDto, IDeliveryMethodRepository>
    {
        
    }
}