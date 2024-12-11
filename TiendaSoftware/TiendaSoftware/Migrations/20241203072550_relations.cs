using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaSoftware.Migrations
{
    /// <inheritdoc />
    public partial class relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEntityId",
                schema: "dbo",
                table: "reviews",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEntityId",
                schema: "dbo",
                table: "lists",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_reviews_UserEntityId",
                schema: "dbo",
                table: "reviews",
                column: "UserEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_lists_UserEntityId",
                schema: "dbo",
                table: "lists",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_lists_users_UserEntityId",
                schema: "dbo",
                table: "lists",
                column: "UserEntityId",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_users_UserEntityId",
                schema: "dbo",
                table: "reviews",
                column: "UserEntityId",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lists_users_UserEntityId",
                schema: "dbo",
                table: "lists");

            migrationBuilder.DropForeignKey(
                name: "FK_reviews_users_UserEntityId",
                schema: "dbo",
                table: "reviews");

            migrationBuilder.DropIndex(
                name: "IX_reviews_UserEntityId",
                schema: "dbo",
                table: "reviews");

            migrationBuilder.DropIndex(
                name: "IX_lists_UserEntityId",
                schema: "dbo",
                table: "lists");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                schema: "dbo",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                schema: "dbo",
                table: "lists");
        }
    }
}
