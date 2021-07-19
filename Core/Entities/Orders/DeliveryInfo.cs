using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Orders
{
    public class DeliveryInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
