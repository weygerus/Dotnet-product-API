using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adimax.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CATEGORIA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DESCRICAO = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PRODUTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DESCRICAO = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HasPendingLogUpdate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryProduct",
                columns: table => new
                {
                    CategorysId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProduct", x => new { x.CategorysId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_CategoryProduct_CATEGORIA_CategorysId",
                        column: x => x.CategorysId,
                        principalTable: "CATEGORIA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProduct_PRODUTO_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "PRODUTO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_ProductsId",
                table: "CategoryProduct",
                column: "ProductsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryProduct");

            migrationBuilder.DropTable(
                name: "CATEGORIA");

            migrationBuilder.DropTable(
                name: "PRODUTO");
        }
    }
}
