using System;
using System.Collections.Generic;
using System.ComponentModel;
using Core.Entities.Orders;
using Core.Interfaces;

namespace Core.Dtos
{
    public class OrderDto : IDto<int>
    {
        public int Id { get; set; }
        [DisplayName("Buyer Email")]
        public string BuyerEmail { get; set; }
        [DisplayName("Order Date")]
        public DateTimeOffset OrderDate { get; set; }
        public OrderAddress ShipToAddress { get; set; }
        [DisplayName("Delivery Method")]
        public int DeliveryMethodId { get; set; }
        [DisplayName("Delivery Method")]
        public string DeliveryMethodName { get; set; }
        [DisplayName("Shipping Price")]
        public decimal ShippingPrice { get; set; }
        public IList<OrderItemDto> OrderItems { get; set; }
        [DisplayName("Sub Total")]
        public decimal SubTotal { get; set; }
        public decimal Total
        {
            get
            {
                return SubTotal + ShippingPrice;
            }
        }
        public OrderStatus Status { get; set; }
        public int UserId { get; set; }
    }
}