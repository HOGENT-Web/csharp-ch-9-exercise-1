using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Ordering
{
    public class OrderConfiguration : EntityConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.OrderDate);
            builder.Property(o => o.HasGiftWrapping);

            builder.OwnsOne(o => o.DeliveryDate, date =>
            {
                date.Property(d => d.Date).HasColumnName(nameof(DeliveryDate));
            });
            builder.OwnsOne(o => o.ShippingAddress, address =>
            {
                address.Property(a => a.Street).HasMaxLength(1_00).HasColumnName("ShipsToStreet").IsRequired();
                address.Property(a => a.Postalcode).HasMaxLength(10).HasColumnName("ShipsToPostalcode").IsRequired();
                address.Property(a => a.City).HasMaxLength(1_00).HasColumnName("ShipsToCity").IsRequired();
                address.Property(a => a.Country).HasMaxLength(1_00).HasColumnName("ShipsToCountry").IsRequired();
            }).Navigation(o => o.ShippingAddress).IsRequired();
            builder.HasMany(o => o.Items).WithOne().IsRequired();
            builder.HasOne(o => o.Customer).WithMany(c => c.Orders).IsRequired();
        }
    }
}
