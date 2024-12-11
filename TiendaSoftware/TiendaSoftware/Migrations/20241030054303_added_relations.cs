using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaSoftware.Migrations
{
    /// <inheritdoc />
    public partial class added_relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reviews_users_user_id",
                schema: "dbo",
                table: "reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_software_publishers_Publisher_id",
                schema: "dbo",
                table: "software");

            migrationBuilder.DropIndex(
                name: "IX_software_Publisher_id",
                schema: "dbo",
                table: "software");

            migrationBuilder.DropIndex(
                name: "IX_reviews_user_id",
                schema: "dbo",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "user_id",
                schema: "dbo",
                table: "reviews");

            migrationBuilder.AlterColumn<string>(
                name: "updated_by",
                schema: "dbo",
                table: "user_downloads",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                schema: "dbo",
                table: "user_downloads",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "updated_by",
                schema: "dbo",
                table: "tags",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                schema: "dbo",
                table: "tags",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "updated_by",
                schema: "dbo",
                table: "software_tags",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                schema: "dbo",
                table: "software_tags",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "updated_by",
                schema: "dbo",
                table: "software_lists",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                schema: "dbo",
                table: "software_lists",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "updated_by",
                schema: "dbo",
                table: "software",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                schema: "dbo",
                table: "software",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PublisherEntityId",
                schema: "dbo",
                table: "software",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "updated_by",
                schema: "dbo",
                table: "reviews",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                schema: "dbo",
                table: "reviews",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "updated_by",
                schema: "dbo",
                table: "publishers",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                schema: "dbo",
                table: "publishers",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "updated_by",
                schema: "dbo",
                table: "lists",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                schema: "dbo",
                table: "lists",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_downloads_created_by",
                schema: "dbo",
                table: "user_downloads",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_user_downloads_updated_by",
                schema: "dbo",
                table: "user_downloads",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_tags_created_by",
                schema: "dbo",
                table: "tags",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_tags_updated_by",
                schema: "dbo",
                table: "tags",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_software_tags_created_by",
                schema: "dbo",
                table: "software_tags",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_software_tags_updated_by",
                schema: "dbo",
                table: "software_tags",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_software_lists_created_by",
                schema: "dbo",
                table: "software_lists",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_software_lists_updated_by",
                schema: "dbo",
                table: "software_lists",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_software_created_by",
                schema: "dbo",
                table: "software",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_software_PublisherEntityId",
                schema: "dbo",
                table: "software",
                column: "PublisherEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_software_updated_by",
                schema: "dbo",
                table: "software",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_created_by",
                schema: "dbo",
                table: "reviews",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_updated_by",
                schema: "dbo",
                table: "reviews",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_publishers_created_by",
                schema: "dbo",
                table: "publishers",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_publishers_updated_by",
                schema: "dbo",
                table: "publishers",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_lists_created_by",
                schema: "dbo",
                table: "lists",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_lists_updated_by",
                schema: "dbo",
                table: "lists",
                column: "updated_by");

            migrationBuilder.AddForeignKey(
                name: "FK_lists_users_created_by",
                schema: "dbo",
                table: "lists",
                column: "created_by",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_lists_users_updated_by",
                schema: "dbo",
                table: "lists",
                column: "updated_by",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_publishers_users_created_by",
                schema: "dbo",
                table: "publishers",
                column: "created_by",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_publishers_users_updated_by",
                schema: "dbo",
                table: "publishers",
                column: "updated_by",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_users_created_by",
                schema: "dbo",
                table: "reviews",
                column: "created_by",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_users_updated_by",
                schema: "dbo",
                table: "reviews",
                column: "updated_by",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_software_publishers_PublisherEntityId",
                schema: "dbo",
                table: "software",
                column: "PublisherEntityId",
                principalSchema: "dbo",
                principalTable: "publishers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_software_users_created_by",
                schema: "dbo",
                table: "software",
                column: "created_by",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_software_users_updated_by",
                schema: "dbo",
                table: "software",
                column: "updated_by",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_software_lists_users_created_by",
                schema: "dbo",
                table: "software_lists",
                column: "created_by",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_software_lists_users_updated_by",
                schema: "dbo",
                table: "software_lists",
                column: "updated_by",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_software_tags_users_created_by",
                schema: "dbo",
                table: "software_tags",
                column: "created_by",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_software_tags_users_updated_by",
                schema: "dbo",
                table: "software_tags",
                column: "updated_by",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tags_users_created_by",
                schema: "dbo",
                table: "tags",
                column: "created_by",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tags_users_updated_by",
                schema: "dbo",
                table: "tags",
                column: "updated_by",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_user_downloads_users_created_by",
                schema: "dbo",
                table: "user_downloads",
                column: "created_by",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_user_downloads_users_updated_by",
                schema: "dbo",
                table: "user_downloads",
                column: "updated_by",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lists_users_created_by",
                schema: "dbo",
                table: "lists");

            migrationBuilder.DropForeignKey(
                name: "FK_lists_users_updated_by",
                schema: "dbo",
                table: "lists");

            migrationBuilder.DropForeignKey(
                name: "FK_publishers_users_created_by",
                schema: "dbo",
                table: "publishers");

            migrationBuilder.DropForeignKey(
                name: "FK_publishers_users_updated_by",
                schema: "dbo",
                table: "publishers");

            migrationBuilder.DropForeignKey(
                name: "FK_reviews_users_created_by",
                schema: "dbo",
                table: "reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_reviews_users_updated_by",
                schema: "dbo",
                table: "reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_software_publishers_PublisherEntityId",
                schema: "dbo",
                table: "software");

            migrationBuilder.DropForeignKey(
                name: "FK_software_users_created_by",
                schema: "dbo",
                table: "software");

            migrationBuilder.DropForeignKey(
                name: "FK_software_users_updated_by",
                schema: "dbo",
                table: "software");

            migrationBuilder.DropForeignKey(
                name: "FK_software_lists_users_created_by",
                schema: "dbo",
                table: "software_lists");

            migrationBuilder.DropForeignKey(
                name: "FK_software_lists_users_updated_by",
                schema: "dbo",
                table: "software_lists");

            migrationBuilder.DropForeignKey(
                name: "FK_software_tags_users_created_by",
                schema: "dbo",
                table: "software_tags");

            migrationBuilder.DropForeignKey(
                name: "FK_software_tags_users_updated_by",
                schema: "dbo",
                table: "software_tags");

            migrationBuilder.DropForeignKey(
                name: "FK_tags_users_created_by",
                schema: "dbo",
                table: "tags");

            migrationBuilder.DropForeignKey(
                name: "FK_tags_users_updated_by",
                schema: "dbo",
                table: "tags");

            migrationBuilder.DropForeignKey(
                name: "FK_user_downloads_users_created_by",
                schema: "dbo",
                table: "user_downloads");

            migrationBuilder.DropForeignKey(
                name: "FK_user_downloads_users_updated_by",
                schema: "dbo",
                table: "user_downloads");

            migrationBuilder.DropIndex(
                name: "IX_user_downloads_created_by",
                schema: "dbo",
                table: "user_downloads");

            migrationBuilder.DropIndex(
                name: "IX_user_downloads_updated_by",
                schema: "dbo",
                table: "user_downloads");

            migrationBuilder.DropIndex(
                name: "IX_tags_created_by",
                schema: "dbo",
                table: "tags");

            migrationBuilder.DropIndex(
                name: "IX_tags_updated_by",
                schema: "dbo",
                table: "tags");

            migrationBuilder.DropIndex(
                name: "IX_software_tags_created_by",
                schema: "dbo",
                table: "software_tags");

            migrationBuilder.DropIndex(
                name: "IX_software_tags_updated_by",
                schema: "dbo",
                table: "software_tags");

            migrationBuilder.DropIndex(
                name: "IX_software_lists_created_by",
                schema: "dbo",
                table: "software_lists");

            migrationBuilder.DropIndex(
                name: "IX_software_lists_updated_by",
                schema: "dbo",
                table: "software_lists");

            migrationBuilder.DropIndex(
                name: "IX_software_created_by",
                schema: "dbo",
                table: "software");

            migrationBuilder.DropIndex(
                name: "IX_software_PublisherEntityId",
                schema: "dbo",
                table: "software");

            migrationBuilder.DropIndex(
                name: "IX_software_updated_by",
                schema: "dbo",
                table: "software");

            migrationBuilder.DropIndex(
                name: "IX_reviews_created_by",
                schema: "dbo",
                table: "reviews");

            migrationBuilder.DropIndex(
                name: "IX_reviews_updated_by",
                schema: "dbo",
                table: "reviews");

            migrationBuilder.DropIndex(
                name: "IX_publishers_created_by",
                schema: "dbo",
                table: "publishers");

            migrationBuilder.DropIndex(
                name: "IX_publishers_updated_by",
                schema: "dbo",
                table: "publishers");

            migrationBuilder.DropIndex(
                name: "IX_lists_created_by",
                schema: "dbo",
                table: "lists");

            migrationBuilder.DropIndex(
                name: "IX_lists_updated_by",
                schema: "dbo",
                table: "lists");

            migrationBuilder.DropColumn(
                name: "PublisherEntityId",
                schema: "dbo",
                table: "software");

            migrationBuilder.AlterColumn<string>(
                name: "updated_by",
                schema: "dbo",
                table: "user_downloads",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                schema: "dbo",
                table: "user_downloads",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "updated_by",
                schema: "dbo",
                table: "tags",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                schema: "dbo",
                table: "tags",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "updated_by",
                schema: "dbo",
                table: "software_tags",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                schema: "dbo",
                table: "software_tags",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "updated_by",
                schema: "dbo",
                table: "software_lists",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                schema: "dbo",
                table: "software_lists",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "updated_by",
                schema: "dbo",
                table: "software",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                schema: "dbo",
                table: "software",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "updated_by",
                schema: "dbo",
                table: "reviews",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                schema: "dbo",
                table: "reviews",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                schema: "dbo",
                table: "reviews",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "updated_by",
                schema: "dbo",
                table: "publishers",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                schema: "dbo",
                table: "publishers",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "updated_by",
                schema: "dbo",
                table: "lists",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                schema: "dbo",
                table: "lists",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.CreateIndex(
                name: "IX_software_Publisher_id",
                schema: "dbo",
                table: "software",
                column: "Publisher_id");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_user_id",
                schema: "dbo",
                table: "reviews",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_users_user_id",
                schema: "dbo",
                table: "reviews",
                column: "user_id",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
    }
}
