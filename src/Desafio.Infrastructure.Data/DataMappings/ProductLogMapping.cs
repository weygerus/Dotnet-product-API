using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Desafio.Domain;

namespace Desafio.Infrastructure.Data.DataMappings
{
    public class ProductLogMapping : IEntityTypeConfiguration<ProductLog>
    {
        public void Configure(EntityTypeBuilder<ProductLog> builder)
        {
            builder.ToTable("PRODUCT_LOG")
                   .HasKey(prop => prop.Id);

            builder.Property(prop => prop.Id)
                .HasColumnName("ID")
                .UseIdentityColumn();

            builder.Property(prop => prop.ProductId)
                .HasColumnName("PRODUCT_ID")
                .HasColumnType("int")
                .HasMaxLength(10);

            builder.Property(prop => prop.UpdatedAt)
                .HasColumnName("UPDATED_AT")
                .HasColumnType("datetime")
                .HasMaxLength(100);

            builder.Property(prop => prop.ProductJson)
                .HasColumnName("PRODUCT_JSON")
                .HasMaxLength(5000);
        }

    }
}