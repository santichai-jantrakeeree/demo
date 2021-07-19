using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface IDeliveryMethodRepository : IDbRepository<DeliveryMethod, DeliveryMethodDto, int>
    {
        
    }
}