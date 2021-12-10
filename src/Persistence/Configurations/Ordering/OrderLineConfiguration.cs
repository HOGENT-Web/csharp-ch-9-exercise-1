using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Ordering
{
    public class OrderLineConfiguration : EntityConfiguration<OrderLine>
    {
        public override void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            base.Configure(builder);
            builder.OwnsOne(o => o.Price, money =>
                {
                    money.Property(m => m.Value)
                        .HasPrecision(18, 2)
                        .HasColumnName(nameof(OrderLine.Price))
                        .IsRequired();
                }).Navigation(o => o.Price).IsRequired();
            builder.HasOne(o => o.Product).WithMany().IsRequired();
            builder.Property(o => o.Quantity).IsRequired();
        }
    }
}
