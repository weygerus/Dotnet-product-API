using Adimax.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adimax.Infrastructure.Data.DataMappings
{
    public class ProductCategoryMapping : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("PRODUCT_CATEGORY")
                   .HasKey(pc => new {pc.ProductId, pc.CategoryId});

            builder.Property(p => p.ProductId)
                   .HasColumnName("PRODUCT_ID")
                   .HasMaxLength(100);

            builder.Property(c => c.CategoryId)
                   .HasColumnName("CATEGORY_ID")
                   .HasMaxLength(100);
        }
    }
}
