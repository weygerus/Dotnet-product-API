using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Adimax.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adimax.Infrastructure.Data.DataMappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("PRODUTO")
                .HasMany(p => p.ProductCategories)
                .WithOne(pc => pc.ProductIn)
                .HasForeignKey(pc => pc.ProductId);

            builder.Property(prop => prop.Id)
                .HasColumnName("ID")
                .UseIdentityColumn();

            builder.Property(prop => prop.Name)
                .HasColumnName("NAME")
                .HasColumnType("varchar")
                .HasMaxLength(100);

            builder.Property(prop => prop.Description)
                .HasColumnName("DESCRIPTION")
                .HasColumnType("varchar")
                .HasMaxLength(400);

            builder.Property(prop => prop.Price)
                .HasColumnName("PRICE")
                .HasColumnType("decimal")
                .HasMaxLength(20);

            builder.Property(prop => prop.CreatedAt)
                .HasColumnName("CREATED")
                .HasColumnType("datetime")
                .HasMaxLength(20);

            builder.Property(prop => prop.HasPendingLogUpdate)
                .HasColumnName("HasPendingLogUpdate")
                .HasColumnType("varchar")
                .HasMaxLength(20);
        }
    }
}
