using Core.Dtos;
using Core.Entities.Orders;
using Core.Interfaces.Repositories;
using Persistent.Repositories.Main;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Persistent.Repositories.Db
{
    public class OrderRepository : BaseDbRepository<Order, OrderDto, int>, IOrderRepository
    {
        public OrderRepository(DemoContext context) : base(context)
        {
        }

        protected override IList<Expression<Func<Order, object>>> ApplyDefaultInclude()
        {
            var includes = new List<Expression<Func<Order, object>>>()
            {
                { o => o.OrderItems }
            };
            return includes;
        }
    }
}