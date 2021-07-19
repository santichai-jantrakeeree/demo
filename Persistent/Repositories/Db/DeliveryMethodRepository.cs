using Core.Dtos;
using Core.Entities;
using Core.Interfaces.Repositories;
using Persistent.Repositories.Main;

namespace Persistent.Repositories.Db
{
    public class DeliveryMethodRepository : BaseDbRepository<DeliveryMethod, DeliveryMethodDto, int>, IDeliveryMethodRepository
    {
        public DeliveryMethodRepository(DemoContext context) : base(context)
        {
        }
    }
}