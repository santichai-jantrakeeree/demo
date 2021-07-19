using System;
using System.Collections.Generic;
using Core.Entities.Orders;
using Core.Interfaces;

namespace Core.Dtos
{
    public class OrderListDto : IDto<int>
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string DeliveryMethodName { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total
        {
            get
            {
                return SubTotal + ShippingPrice;
            }
        }
        public OrderStatus Status { get; set; }
    }
}