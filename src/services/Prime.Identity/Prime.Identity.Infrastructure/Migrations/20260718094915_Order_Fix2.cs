using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prime.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Order_Fix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderNumber",
                schema: "Business",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                schema: "Business",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Quantity",
                schema: "Business",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                schema: "Business",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                schema: "Business",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductName",
                schema: "Business",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                schema: "Business",
                table: "OrderItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OrderNumber",
                schema: "Business",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                schema: "Business",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                schema: "Business",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                schema: "Business",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                schema: "Business",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                schema: "Business",
                table: "OrderItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderNumber",
                schema: "Business",
                table: "Orders",
                column: "OrderNumber",
                unique: true);
        }
    }
}
