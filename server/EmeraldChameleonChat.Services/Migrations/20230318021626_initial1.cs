using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmeraldChameleonChat.Services.Migrations
{
    /// <inheritdoc />
    public partial class initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatRoomMessage_Chatroom_ChatRoomId",
                table: "ChatRoomMessage");

            migrationBuilder.DropIndex(
                name: "IX_ChatRoomMessage_ChatRoomId",
                table: "ChatRoomMessage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ChatRoomMessage_ChatRoomId",
                table: "ChatRoomMessage",
                column: "ChatRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRoomMessage_Chatroom_ChatRoomId",
                table: "ChatRoomMessage",
                column: "ChatRoomId",
                principalTable: "Chatroom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
