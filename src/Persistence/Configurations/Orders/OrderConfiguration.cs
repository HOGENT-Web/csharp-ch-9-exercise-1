using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Orders
{
    internal class OrderConfiguration : EntityConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.OrderDate); // Since we don't have private setters
            builder.Property(o => o.HasGiftWrapping); // Since we don't have private setters

            builder.OwnsOne(o => o.DeliveryDate, date =>
            {
                date.Property(d => d.Date).HasColumnName("DeliveryDate");
            });
            builder.OwnsOne(o => o.ShippingAddress, address =>
            {
                address.Property(a => a.Street).HasColumnName("ShipsToStreet").HasMaxLength(1_00).IsRequired();
                address.Property(a => a.Postalcode).HasColumnName("ShipsToPostalcode").HasMaxLength(10).IsRequired();
                address.Property(a => a.City).HasColumnName("ShipsToCity").HasMaxLength(1_00).IsRequired();
                address.Property(a => a.Country).HasColumnName("ShipsToCountry").HasMaxLength(1_00).IsRequired();
            }).Navigation(o => o.ShippingAddress).IsRequired();
            builder.HasMany(o => o.Items).WithOne().IsRequired();
            // TODO: customer not in Order
            builder.Ignore(o => o.Total);
        }
    }
}
