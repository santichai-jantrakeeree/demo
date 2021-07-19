using Core.Dtos;
using Core.Entities.Orders;

namespace Core.Interfaces.Repositories
{
    public interface IOrderRepository : IDbRepository<Order, OrderDto, int>
    {
        
    }
}