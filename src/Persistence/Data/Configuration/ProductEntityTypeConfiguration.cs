using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.ImageUrl).IsRequired();
            builder.OwnsOne(p => p.Price, money =>
            {
                money.Property(m => m.Value)
                    .HasPrecision(38, 10)
                    .HasColumnName("Price")
                    .IsRequired();
            }).Navigation(p => p.Price).IsRequired();
            builder.HasOne(p => p.Category).WithMany().IsRequired();
        }
    }
}
