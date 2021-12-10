using Domain.Common;
using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Customers
{
    public class CustomerConfiguration : EntityConfiguration<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            base.Configure(builder);
            builder.OwnsOne(c => c.Address, address =>
            {
                address.Property(a => a.Street).HasMaxLength(1_00).HasColumnName(nameof(Address.Street)).IsRequired();
                address.Property(a => a.Postalcode).HasMaxLength(10).HasColumnName(nameof(Address.Postalcode)).IsRequired();
                address.Property(a => a.City).HasMaxLength(1_00).HasColumnName(nameof(Address.City)).IsRequired();
                address.Property(a => a.Country).HasMaxLength(1_00).HasColumnName(nameof(Address.Country)).IsRequired();
            }).Navigation(c => c.Address).IsRequired();

            builder.OwnsOne(c => c.Name, name =>
            {
                name.Property(n => n.Firstname).HasMaxLength(1_00).HasColumnName(nameof(CustomerName.Firstname)).IsRequired();
                name.Property(n => n.Lastname).HasMaxLength(1_00).HasColumnName(nameof(CustomerName.Lastname)).IsRequired();
            }).Navigation(c => c.Name).IsRequired();
        }
    }
}
