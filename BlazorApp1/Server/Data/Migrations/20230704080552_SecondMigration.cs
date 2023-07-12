using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp1.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Chatroom_ChatId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_SenderId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInChats_Chatroom_ChatroomsId",
                table: "UsersInChats");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInChats_User_UsersId",
                table: "UsersInChats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chatroom",
                table: "Chatroom");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "Messages");

            migrationBuilder.RenameTable(
                name: "Chatroom",
                newName: "Chatrooms");

            migrationBuilder.RenameIndex(
                name: "IX_Message_SenderId",
                table: "Messages",
                newName: "IX_Messages_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_ChatId",
                table: "Messages",
                newName: "IX_Messages_ChatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chatrooms",
                table: "Chatrooms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Chatrooms_ChatId",
                table: "Messages",
                column: "ChatId",
                principalTable: "Chatrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInChats_Chatrooms_ChatroomsId",
                table: "UsersInChats",
                column: "ChatroomsId",
                principalTable: "Chatrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInChats_Users_UsersId",
                table: "UsersInChats",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Chatrooms_ChatId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInChats_Chatrooms_ChatroomsId",
                table: "UsersInChats");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInChats_Users_UsersId",
                table: "UsersInChats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chatrooms",
                table: "Chatrooms");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "Message");

            migrationBuilder.RenameTable(
                name: "Chatrooms",
                newName: "Chatroom");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_SenderId",
                table: "Message",
                newName: "IX_Message_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ChatId",
                table: "Message",
                newName: "IX_Message_ChatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                table: "Message",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chatroom",
                table: "Chatroom",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Chatroom_ChatId",
                table: "Message",
                column: "ChatId",
                principalTable: "Chatroom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_SenderId",
                table: "Message",
                column: "SenderId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInChats_Chatroom_ChatroomsId",
                table: "UsersInChats",
                column: "ChatroomsId",
                principalTable: "Chatroom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInChats_User_UsersId",
                table: "UsersInChats",
                column: "UsersId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
