using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities.Orders;
using Core.Interfaces.Repositories;

namespace Core.Interfaces.Services
{
    public interface IOrderService : IDbService<int, Order, OrderDto, IOrderRepository>
    {
        Task<OrderDto> CreateOrder(OrderDto dto);
        //Task<IReadOnlyList<OrderDto>> GetOrderedByUser(int userId);
    }
}