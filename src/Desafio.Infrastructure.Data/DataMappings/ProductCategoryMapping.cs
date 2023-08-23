using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Desafio.Domain;

namespace Desafio.Infrastructure.Data.DataMappings
{
    public class ProductCategoryMapping : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("PRODUCT_CATEGORY")
                   .HasKey(pc => new {pc.ProductId, pc.CategoryId});

            builder.Property("ProductCategoryId")
                .HasColumnName("PRODUCT_CATEGORY_ID")
                .UseIdentityColumn();

            builder.Property(p => p.ProductId)
                   .HasColumnName("PRODUCT_ID")
                   .HasMaxLength(100);

            builder.Property(c => c.CategoryId)
                   .HasColumnName("CATEGORY_ID")
                   .HasMaxLength(100);
        }
    }
}
