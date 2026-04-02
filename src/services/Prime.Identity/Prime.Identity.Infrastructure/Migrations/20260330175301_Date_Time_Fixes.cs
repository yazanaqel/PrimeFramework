using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Date_Time_Fixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedOnUtc",
                schema: "Identity",
                table: "Users",
                newName: "ModifiedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                schema: "Identity",
                table: "Users",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedAt",
                schema: "Identity",
                table: "Users",
                newName: "ModifiedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "Identity",
                table: "Users",
                newName: "CreatedOnUtc");
        }
    }
}
