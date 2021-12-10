using Domain.Products;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Products
{
    public class CategoryConfiguration : EntityConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);
            builder.Property(c => c.Name).HasMaxLength(1_00).IsRequired();
        }
    }
}
