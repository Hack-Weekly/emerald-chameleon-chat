using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmeraldChameleonChat.Services.Migrations
{
    /// <inheritdoc />
    public partial class addChatRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChatId",
                table: "ChatMessage",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ChatMessage",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "ChatMessage");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ChatMessage");
        }
    }
}
