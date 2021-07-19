using System;
using Core.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistent.Data.Config
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.Property(p => p.BuyerEmail).IsRequired().HasMaxLength(256);
            builder.Property(p => p.OrderDate).IsRequired();
            builder.Property(p => p.SubTotal).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.BuyerEmail).IsRequired().HasMaxLength(256);

            builder.Property(s => s.Status)
                .IsRequired()
                .HasConversion(
                    o => o.ToString(),
                    o => (OrderStatus) Enum.Parse(typeof(OrderStatus), o)
                );

            builder.OwnsOne(o => o.ShipToAddress, a => {
                a.WithOwner();
                a.Property(p => p.FirstName).IsRequired().HasMaxLength(64);
                a.Property(p => p.LastName).IsRequired().HasMaxLength(64);
                a.Property(p => p.Street).IsRequired().HasMaxLength(64);
                a.Property(p => p.City).IsRequired().HasMaxLength(64);
                a.Property(p => p.State).IsRequired().HasMaxLength(64);
                a.Property(p => p.Zipcode).IsRequired().HasMaxLength(16);
            });
            
            builder.OwnsOne(o => o.DeliveryInfo, a => {
                a.WithOwner();
                a.Property(p => p.Name).IsRequired().HasMaxLength(256);
                a.Property(p => p.DeliveryTime).IsRequired().HasMaxLength(256);
                a.Property(p => p.Description).IsRequired().HasMaxLength(512);
                a.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();
            });
            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}