using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaSoftware.Migrations
{
    /// <inheritdoc />
    public partial class adjustServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_downloads_users_User_id",
                schema: "dbo",
                table: "user_downloads");

            migrationBuilder.DropIndex(
                name: "IX_user_downloads_User_id",
                schema: "dbo",
                table: "user_downloads");

            migrationBuilder.DropColumn(
                name: "User_id",
                schema: "dbo",
                table: "user_downloads");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "User_id",
                schema: "dbo",
                table: "user_downloads",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_downloads_User_id",
                schema: "dbo",
                table: "user_downloads",
                column: "User_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_downloads_users_User_id",
                schema: "dbo",
                table: "user_downloads",
                column: "User_id",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
