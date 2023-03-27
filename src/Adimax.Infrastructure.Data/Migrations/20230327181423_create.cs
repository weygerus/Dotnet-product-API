using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adimax.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class create : Migration
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
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    DESCRICAO = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime", maxLength: 100, nullable: false),
                    UPDATE_AT = table.Column<DateTime>(type: "datetime", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PRODUTO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    DESCRIPTION = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false),
                    PRICE = table.Column<decimal>(type: "decimal(18,0)", maxLength: 20, nullable: false),
                    CREATED = table.Column<DateTime>(type: "datetime", maxLength: 20, nullable: false),
                    HasPendingLogUpdate = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT_CATEGORY",
                columns: table => new
                {
                    PRODUCT_ID = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    CATEGORY_ID = table.Column<int>(type: "int", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT_CATEGORY", x => new { x.PRODUCT_ID, x.CATEGORY_ID });
                    table.ForeignKey(
                        name: "FK_PRODUCT_CATEGORY_CATEGORIA_CATEGORY_ID",
                        column: x => x.CATEGORY_ID,
                        principalTable: "CATEGORIA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRODUCT_CATEGORY_PRODUTO_PRODUCT_ID",
                        column: x => x.PRODUCT_ID,
                        principalTable: "PRODUTO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT_LOG",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    PRODUCT_ID = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    PRODUCT_JSON = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_PRODUCT_LOG_PRODUTO_PRODUCT_ID",
                        column: x => x.PRODUCT_ID,
                        principalTable: "PRODUTO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_CATEGORY_CATEGORY_ID",
                table: "PRODUCT_CATEGORY",
                column: "CATEGORY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_LOG_PRODUCT_ID",
                table: "PRODUCT_LOG",
                column: "PRODUCT_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PRODUCT_CATEGORY");

            migrationBuilder.DropTable(
                name: "PRODUCT_LOG");

            migrationBuilder.DropTable(
                name: "CATEGORIA");

            migrationBuilder.DropTable(
                name: "PRODUTO");
        }
    }
}
