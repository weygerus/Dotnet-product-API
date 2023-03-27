using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adimax.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ajust : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PRODUCT_LOG_PRODUTO_PRODUCT_ID",
                table: "PRODUCT_LOG");

            migrationBuilder.DropIndex(
                name: "IX_PRODUCT_LOG_PRODUCT_ID",
                table: "PRODUCT_LOG");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PRODUCT_LOG",
                newName: "ID");

            migrationBuilder.AlterColumn<string>(
                name: "PRODUCT_JSON",
                table: "PRODUCT_LOG",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5000)",
                oldMaxLength: 5000);

            migrationBuilder.AddColumn<DateTime>(
                name: "UPDATED_AT",
                table: "PRODUCT_LOG",
                type: "datetime",
                maxLength: 100,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UPDATED_AT",
                table: "PRODUCT_LOG");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "PRODUCT_LOG",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "PRODUCT_JSON",
                table: "PRODUCT_LOG",
                type: "varchar(5000)",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000);

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_LOG_PRODUCT_ID",
                table: "PRODUCT_LOG",
                column: "PRODUCT_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUCT_LOG_PRODUTO_PRODUCT_ID",
                table: "PRODUCT_LOG",
                column: "PRODUCT_ID",
                principalTable: "PRODUTO",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
