using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WA.Pizza.Infrastructure.Migrations
{
    public partial class UpdateAdvertisements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisings_AdsClients_AdsClientId",
                table: "Advertisings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Advertisings",
                table: "Advertisings");

            migrationBuilder.RenameTable(
                name: "Advertisings",
                newName: "Advertisements");

            migrationBuilder.RenameIndex(
                name: "IX_Advertisings_AdsClientId",
                table: "Advertisements",
                newName: "IX_Advertisements_AdsClientId");

            migrationBuilder.AlterColumn<string>(
                name: "Img",
                table: "Advertisements",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Advertisements",
                table: "Advertisements",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_AdsClients_AdsClientId",
                table: "Advertisements",
                column: "AdsClientId",
                principalTable: "AdsClients",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_AdsClients_AdsClientId",
                table: "Advertisements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Advertisements",
                table: "Advertisements");

            migrationBuilder.RenameTable(
                name: "Advertisements",
                newName: "Advertisings");

            migrationBuilder.RenameIndex(
                name: "IX_Advertisements_AdsClientId",
                table: "Advertisings",
                newName: "IX_Advertisings_AdsClientId");

            migrationBuilder.AlterColumn<string>(
                name: "Img",
                table: "Advertisings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Advertisings",
                table: "Advertisings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisings_AdsClients_AdsClientId",
                table: "Advertisings",
                column: "AdsClientId",
                principalTable: "AdsClients",
                principalColumn: "Id");
        }
    }
}
