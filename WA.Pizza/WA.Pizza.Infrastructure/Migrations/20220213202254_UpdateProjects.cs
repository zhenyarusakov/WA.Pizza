using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WA.Pizza.Infrastructure.Migrations
{
    public partial class UpdateProjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "BasketItems",
                type: "decimal(20,8)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,8)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "BasketItems",
                type: "decimal(20,8)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,8)");
        }
    }
}
