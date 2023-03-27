﻿using Adimax.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adimax.Infrastructure.Data.DataMappings
{
    public class ProductLogMapping : IEntityTypeConfiguration<ProductLog>
    {
        public void Configure(EntityTypeBuilder<ProductLog> builder)
        {
            builder.ToTable("PRODUCT_LOG")
                   .HasNoKey();

            builder.Property(prop => prop.Id)
                .HasColumnName("ID")
                .IsRequired();

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