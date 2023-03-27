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
                .HasMany(n => n.Products)
                .WithMany(n => n.Categorys);

            builder.Property(prop => prop.Id)
                .HasColumnName("ID")
                .UseIdentityColumn()
                .IsRequired();

            builder.Property(prop => prop.Description)
                .HasColumnName("DESCRICAO")
                .HasColumnType("varchar")
                .HasMaxLength(400);
        }
        
    }
}
