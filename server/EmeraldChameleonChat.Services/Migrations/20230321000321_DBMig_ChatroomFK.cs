using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmeraldChameleonChat.Services.Migrations
{
    /// <inheritdoc />
    public partial class DBMig_ChatroomFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Chatroom_CreatorId",
                table: "Chatroom",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chatroom_Users_CreatorId",
                table: "Chatroom",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chatroom_Users_CreatorId",
                table: "Chatroom");

            migrationBuilder.DropIndex(
                name: "IX_Chatroom_CreatorId",
                table: "Chatroom");
        }
    }
}
