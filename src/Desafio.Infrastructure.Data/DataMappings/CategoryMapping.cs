using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Desafio.Domain;

namespace Desafio.Infrastructure.Data.DataMappings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("CATEGORY")
                .HasMany(p => p.ProductCategories)
                .WithOne(n => n.CategoryIn)
                .HasForeignKey(pc => pc.CategoryId);

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