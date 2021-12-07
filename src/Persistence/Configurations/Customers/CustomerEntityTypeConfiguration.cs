using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Customers
{
    internal class CustomerConfiguration : EntityConfiguration<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            base.Configure(builder);

            builder.OwnsOne(c => c.Address, address =>
            {
                address.Property(a => a.Street).HasColumnName("Street").HasMaxLength(1_00).IsRequired();
                address.Property(a => a.Postalcode).HasColumnName("Postalcode").HasMaxLength(10).IsRequired();
                address.Property(a => a.City).HasColumnName("City").HasMaxLength(1_00).IsRequired();
                address.Property(a => a.Country).HasColumnName("Country").HasMaxLength(1_00).IsRequired();
            }).Navigation(c => c.Address).IsRequired();

            builder.OwnsOne(c => c.Name, name =>
            {
                name.Property(n => n.Firstname).HasColumnName("Firstname").HasMaxLength(1_00).IsRequired();
                name.Property(n => n.Lastname).HasColumnName("Lastname").HasMaxLength(1_00).IsRequired();
            }).Navigation(c => c.Name).IsRequired();
        }
    }
}
