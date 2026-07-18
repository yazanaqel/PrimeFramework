using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prime.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Order_Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_ProductId",
                schema: "Business",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProductId",
                schema: "Business",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "Business",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                schema: "Business",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId",
                schema: "Business",
                table: "Orders",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_ProductId",
                schema: "Business",
                table: "Orders",
                column: "ProductId",
                principalSchema: "Business",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
