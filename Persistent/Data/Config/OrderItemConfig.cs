using Core.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistent.Data.Config
{
    public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(p => p.Quantity).IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(s => s.ProductName).IsRequired().HasMaxLength(256);
            builder.Property(s => s.PictureUrl).IsRequired().HasMaxLength(256);
            builder.Property(s => s.ProductId).IsRequired();
        }
    }
}