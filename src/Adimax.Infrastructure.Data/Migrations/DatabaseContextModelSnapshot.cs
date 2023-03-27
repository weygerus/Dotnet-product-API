﻿// <auto-generated />
using System;
using Adimax.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Adimax.Infrastructure.Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Adimax.Domain.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasMaxLength(100)
                        .HasColumnType("datetime")
                        .HasColumnName("CREATED_AT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("varchar")
                        .HasColumnName("DESCRICAO");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("Name");

                    b.Property<DateTime>("UpdateAt")
                        .HasMaxLength(100)
                        .HasColumnType("datetime")
                        .HasColumnName("UPDATE_AT");

                    b.HasKey("Id");

                    b.ToTable("CATEGORIA", (string)null);
                });

            modelBuilder.Entity("Adimax.Domain.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasMaxLength(20)
                        .HasColumnType("datetime")
                        .HasColumnName("CREATED");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("varchar")
                        .HasColumnName("DESCRIPTION");

                    b.Property<string>("HasPendingLogUpdate")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar")
                        .HasColumnName("HasPendingLogUpdate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("NAME");

                    b.Property<decimal>("Price")
                        .HasMaxLength(20)
                        .HasColumnType("decimal")
                        .HasColumnName("PRICE");

                    b.HasKey("Id");

                    b.ToTable("PRODUTO", (string)null);
                });

            modelBuilder.Entity("Adimax.Domain.ProductCategory", b =>
                {
                    b.Property<int>("ProductId")
                        .HasMaxLength(100)
                        .HasColumnType("int")
                        .HasColumnName("PRODUCT_ID");

                    b.Property<int>("CategoryId")
                        .HasMaxLength(100)
                        .HasColumnType("int")
                        .HasColumnName("CATEGORY_ID");

                    b.HasKey("ProductId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("PRODUCT_CATEGORY", (string)null);
                });

            modelBuilder.Entity("Adimax.Domain.ProductLog", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    b.Property<int>("ProductId")
                        .HasMaxLength(10)
                        .HasColumnType("int")
                        .HasColumnName("PRODUCT_ID");

                    b.Property<string>("ProductJson")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PRODUCT_JSON");

                    b.Property<DateTime>("UpdatedAt")
                        .HasMaxLength(100)
                        .HasColumnType("datetime")
                        .HasColumnName("UPDATED_AT");

                    b.ToTable("PRODUCT_LOG", (string)null);
                });

            modelBuilder.Entity("Adimax.Domain.ProductCategory", b =>
                {
                    b.HasOne("Adimax.Domain.Category", "CategoryIn")
                        .WithMany("ProductCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Adimax.Domain.Product", "ProductIn")
                        .WithMany("ProductCategories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryIn");

                    b.Navigation("ProductIn");
                });

            modelBuilder.Entity("Adimax.Domain.Category", b =>
                {
                    b.Navigation("ProductCategories");
                });

            modelBuilder.Entity("Adimax.Domain.Product", b =>
                {
                    b.Navigation("ProductCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
