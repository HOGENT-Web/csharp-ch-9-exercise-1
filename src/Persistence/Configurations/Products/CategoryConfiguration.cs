using Domain.Products;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Products
{
    internal class CategoryConfiguration : EntityConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(1_000);
        }
    }
}
