using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WA.Pizza.Infrastructure.Migrations
{
    public partial class AddedIsBlockedPropertyToAdsClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisings_AdsClients_AdsClientId",
                table: "Advertisings");

            migrationBuilder.AlterColumn<string>(
                name: "WebSite",
                table: "Advertisings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Advertisings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "AdsClients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_AdsClients_ApiKey",
                table: "AdsClients",
                column: "ApiKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisings_AdsClients_AdsClientId",
                table: "Advertisings",
                column: "AdsClientId",
                principalTable: "AdsClients",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisings_AdsClients_AdsClientId",
                table: "Advertisings");

            migrationBuilder.DropIndex(
                name: "IX_AdsClients_ApiKey",
                table: "AdsClients");

            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "AdsClients");

            migrationBuilder.AlterColumn<string>(
                name: "WebSite",
                table: "Advertisings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Advertisings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisings_AdsClients_AdsClientId",
                table: "Advertisings",
                column: "AdsClientId",
                principalTable: "AdsClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
