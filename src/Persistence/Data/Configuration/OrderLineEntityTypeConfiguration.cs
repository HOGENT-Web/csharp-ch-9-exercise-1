using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class OrderLineEntityTypeConfiguration : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.OwnsOne(o => o.Price, money =>
                {
                    money.Property(m => m.Value)
                        .HasPrecision(12, 10)
                        .HasColumnName("Price")
                        .IsRequired();
                }).Navigation(o => o.Price).IsRequired();
            builder.HasOne(o => o.Product).WithMany().IsRequired();
            builder.Property(o => o.Quantity).IsRequired();
        }
    }
}
