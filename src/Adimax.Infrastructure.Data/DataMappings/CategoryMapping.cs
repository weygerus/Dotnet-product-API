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
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("CATEGORIA")
                .HasMany(p => p.ProductCategories)
                .WithOne(n => n.CategoryIn);

            builder.Property(prop => prop.Id)
                .HasColumnName("ID")
                .UseIdentityColumn();

            builder.Property(prop => prop.Name)
                .HasColumnName("Name")
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Property(prop => prop.Description)
                .HasColumnName("DESCRICAO")
                .HasColumnType("varchar")
                .HasMaxLength(400);

            builder.Property(prop => prop.CreatedAt)
                .HasColumnName("CREATED_AT")
                .HasColumnType("datetime")
                .HasMaxLength(100);

            builder.Property(prop => prop.UpdateAt)
                .HasColumnName("UPDATE_AT")
                .HasColumnType("datetime")
                .HasMaxLength(100);
        }
        
    }
}
