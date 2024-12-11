using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaSoftware.Migrations
{
    /// <inheritdoc />
    public partial class adjustdevs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_publishers_users_user_id",
                schema: "dbo",
                table: "publishers");

            migrationBuilder.DropIndex(
                name: "IX_publishers_user_id",
                schema: "dbo",
                table: "publishers");

            migrationBuilder.DropColumn(
                name: "user_id",
                schema: "dbo",
                table: "publishers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "user_id",
                schema: "dbo",
                table: "publishers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_publishers_user_id",
                schema: "dbo",
                table: "publishers",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_publishers_users_user_id",
                schema: "dbo",
                table: "publishers",
                column: "user_id",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
