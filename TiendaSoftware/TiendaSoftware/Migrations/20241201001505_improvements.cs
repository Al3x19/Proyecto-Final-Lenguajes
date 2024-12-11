using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaSoftware.Migrations
{
    /// <inheritdoc />
    public partial class improvements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lists_users_user_id",
                schema: "dbo",
                table: "lists");

            migrationBuilder.DropIndex(
                name: "IX_lists_user_id",
                schema: "dbo",
                table: "lists");

            migrationBuilder.DropColumn(
                name: "description",
                schema: "dbo",
                table: "tags");

            migrationBuilder.DropColumn(
                name: "description",
                schema: "dbo",
                table: "publishers");

            migrationBuilder.DropColumn(
                name: "name",
                schema: "dbo",
                table: "publishers");

            migrationBuilder.DropColumn(
                name: "securitycode",
                schema: "dbo",
                table: "publishers");

            migrationBuilder.DropColumn(
                name: "user_id",
                schema: "dbo",
                table: "lists");

            migrationBuilder.RenameColumn(
                name: "imagen",
                schema: "dbo",
                table: "software",
                newName: "version");

            migrationBuilder.AlterColumn<string>(
                name: "securitycode",
                schema: "security",
                table: "users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "description",
                schema: "security",
                table: "users",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "resfesh_token",
                schema: "security",
                table: "users",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "resfesh_token_expire",
                schema: "security",
                table: "users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "icono",
                schema: "dbo",
                table: "software",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "description",
                schema: "security",
                table: "users");

            migrationBuilder.DropColumn(
                name: "resfesh_token",
                schema: "security",
                table: "users");

            migrationBuilder.DropColumn(
                name: "resfesh_token_expire",
                schema: "security",
                table: "users");

            migrationBuilder.DropColumn(
                name: "icono",
                schema: "dbo",
                table: "software");

            migrationBuilder.DropColumn(
                name: "user_id",
                schema: "dbo",
                table: "publishers");

            migrationBuilder.RenameColumn(
                name: "version",
                schema: "dbo",
                table: "software",
                newName: "imagen");

            migrationBuilder.AlterColumn<string>(
                name: "securitycode",
                schema: "security",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                schema: "dbo",
                table: "tags",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                schema: "dbo",
                table: "publishers",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "dbo",
                table: "publishers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "securitycode",
                schema: "dbo",
                table: "publishers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                schema: "dbo",
                table: "lists",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_lists_user_id",
                schema: "dbo",
                table: "lists",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_lists_users_user_id",
                schema: "dbo",
                table: "lists",
                column: "user_id",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
