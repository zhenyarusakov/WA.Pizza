using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WA.Pizza.Infrastructure.Migrations
{
    public partial class CreateTokenModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TokenModel_AspNetUsers_ApplicationUserId",
                table: "TokenModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TokenModel",
                table: "TokenModel");

            migrationBuilder.RenameTable(
                name: "TokenModel",
                newName: "TokenModels");

            migrationBuilder.RenameIndex(
                name: "IX_TokenModel_ApplicationUserId",
                table: "TokenModels",
                newName: "IX_TokenModels_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TokenModels",
                table: "TokenModels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TokenModels_AspNetUsers_ApplicationUserId",
                table: "TokenModels",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TokenModels_AspNetUsers_ApplicationUserId",
                table: "TokenModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TokenModels",
                table: "TokenModels");

            migrationBuilder.RenameTable(
                name: "TokenModels",
                newName: "TokenModel");

            migrationBuilder.RenameIndex(
                name: "IX_TokenModels_ApplicationUserId",
                table: "TokenModel",
                newName: "IX_TokenModel_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TokenModel",
                table: "TokenModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TokenModel_AspNetUsers_ApplicationUserId",
                table: "TokenModel",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
