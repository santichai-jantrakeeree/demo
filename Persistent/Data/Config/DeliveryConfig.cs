using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistent.Data.Config
{
    public class DeliveryMethodConfig : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(256);
            builder.Property(p => p.DeliveryTime).IsRequired().HasMaxLength(256);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(512);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();
        }
    }
}