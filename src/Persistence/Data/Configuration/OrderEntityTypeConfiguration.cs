using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.OrderDate);
            builder.Property(o => o.HasGiftWrapping);

            builder.OwnsOne(o => o.DeliveryDate, date =>
            {
                date.Property(d => d.Date).HasColumnName("DeliveryDate");
            });
            builder.OwnsOne(o => o.ShippingAddress, address =>
            {
                address.Property(a => a.Street).HasColumnName("ShipsToStreet").IsRequired();
                address.Property(a => a.Postalcode).HasColumnName("ShipsToPostalcode").IsRequired();
                address.Property(a => a.City).HasColumnName("ShipsToCity").IsRequired();
                address.Property(a => a.Country).HasColumnName("ShipsToCountry").IsRequired();
            }).Navigation(o => o.ShippingAddress).IsRequired();
            builder.HasMany(o => o.Items).WithOne().IsRequired();
            builder.HasOne(o => o.Customer).WithMany(c => c.Orders).IsRequired();
            builder.Ignore(o => o.Total);
        }
    }
}
