using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp1.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class FourthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Chatrooms_ChatId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInChats_Chatrooms_ChatroomsId",
                table: "UsersInChats");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.AddColumn<string>(
                name: "HashPassword",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Chats_ChatId",
                table: "Messages",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInChats_Chats_ChatroomsId",
                table: "UsersInChats",
                column: "ChatroomsId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Chats_ChatId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInChats_Chats_ChatroomsId",
                table: "UsersInChats");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropColumn(
                name: "HashPassword",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "Chatrooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chatrooms", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Chatrooms_ChatId",
                table: "Messages",
                column: "ChatId",
                principalTable: "Chatrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInChats_Chatrooms_ChatroomsId",
                table: "UsersInChats",
                column: "ChatroomsId",
                principalTable: "Chatrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
