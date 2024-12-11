using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaSoftware.Migrations
{
    /// <inheritdoc />
    public partial class Devimprovements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_software_publishers_PublisherEntityId",
                schema: "dbo",
                table: "software");

            migrationBuilder.DropIndex(
                name: "IX_software_PublisherEntityId",
                schema: "dbo",
                table: "software");

            migrationBuilder.DropColumn(
                name: "description",
                schema: "security",
                table: "users");

            migrationBuilder.DropColumn(
                name: "PublisherEntityId",
                schema: "dbo",
                table: "software");

            migrationBuilder.AddColumn<string>(
                name: "description",
                schema: "dbo",
                table: "publishers",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "dbo",
                table: "publishers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_software_Publisher_id",
                schema: "dbo",
                table: "software",
                column: "Publisher_id");

            migrationBuilder.AddForeignKey(
                name: "FK_software_publishers_Publisher_id",
                schema: "dbo",
                table: "software",
                column: "Publisher_id",
                principalSchema: "dbo",
                principalTable: "publishers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_software_publishers_Publisher_id",
                schema: "dbo",
                table: "software");

            migrationBuilder.DropIndex(
                name: "IX_software_Publisher_id",
                schema: "dbo",
                table: "software");

            migrationBuilder.DropColumn(
                name: "description",
                schema: "dbo",
                table: "publishers");

            migrationBuilder.DropColumn(
                name: "name",
                schema: "dbo",
                table: "publishers");

            migrationBuilder.AddColumn<string>(
                name: "description",
                schema: "security",
                table: "users",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PublisherEntityId",
                schema: "dbo",
                table: "software",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_software_PublisherEntityId",
                schema: "dbo",
                table: "software",
                column: "PublisherEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_software_publishers_PublisherEntityId",
                schema: "dbo",
                table: "software",
                column: "PublisherEntityId",
                principalSchema: "dbo",
                principalTable: "publishers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
