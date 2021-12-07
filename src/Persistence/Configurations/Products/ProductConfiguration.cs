using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Products
{
    internal class ProductConfiguration : AuditableEntityConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);
            builder.HasDiscriminator<ProductType>("Type")
                   .HasValue<Product>(ProductType.Article)
                   .HasValue<Subscription>(ProductType.Subscription);

            builder.Property(p => p.Name).HasMaxLength(1_00).IsRequired();
            builder.Property(p => p.ImageUrl).HasMaxLength(1_000).IsRequired();
            builder.OwnsOne(p => p.Price, money =>
            {
                money.Property(m => m.Value)
                    .HasPrecision(18, 2)
                    .HasColumnName("Price")
                    .IsRequired();
            }).Navigation(p => p.Price).IsRequired();
            builder.HasOne(p => p.Category).WithMany().IsRequired();
        }
    }
}
