using System;
using System.Collections.Generic;

namespace Core.Entities.Orders
{
    public class Order : BaseEntity<int>
    {
        public Order()
        {
        }

        public Order(IList<OrderItem> orderItems, string buyerEmail, OrderAddress shipToAddress, DeliveryInfo deliveryInfo, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryInfo = deliveryInfo;
            OrderItems = orderItems;
            SubTotal = subTotal; 
        }

        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public OrderAddress ShipToAddress { get; set; }
        public DeliveryInfo DeliveryInfo { get; set; }
        public IList<OrderItem> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public int UserId { get; set; }
        public decimal GetTotal()
        {
            return SubTotal + DeliveryInfo.Price;
        }
    }
}