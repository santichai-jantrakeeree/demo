using Core.Entities.Identities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistent.Data.Config
{
    public class AddressConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(64);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(64);
            builder.Property(p => p.Street).IsRequired().HasMaxLength(64);
            builder.Property(p => p.City).IsRequired().HasMaxLength(64);
            builder.Property(p => p.State).IsRequired().HasMaxLength(64);
            builder.Property(p => p.Zipcode).IsRequired().HasMaxLength(16);
        }
    }
}