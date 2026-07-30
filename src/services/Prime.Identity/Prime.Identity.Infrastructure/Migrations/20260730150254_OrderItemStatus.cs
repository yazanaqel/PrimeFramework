using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prime.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrderItemStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderItemStatus",
                schema: "Business",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderItemStatus",
                schema: "Business",
                table: "OrderItems");
        }
    }
}
