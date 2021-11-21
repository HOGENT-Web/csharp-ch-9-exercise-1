using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.OwnsOne(c => c.Address, address =>
            {
                address.Property(a => a.Street).HasColumnName("Street").IsRequired();
                address.Property(a => a.Postalcode).HasColumnName("Postalcode").IsRequired();
                address.Property(a => a.City).HasColumnName("City").IsRequired();
                address.Property(a => a.Country).HasColumnName("Country").IsRequired();
            }).Navigation(c => c.Address).IsRequired();

            builder.OwnsOne(c => c.Name, name =>
            {
                name.Property(n => n.Firstname).HasColumnName("Firstname").IsRequired();
                name.Property(n => n.Lastname).HasColumnName("Lastname").IsRequired();
            }).Navigation(c => c.Name).IsRequired();
        }
    }
}
