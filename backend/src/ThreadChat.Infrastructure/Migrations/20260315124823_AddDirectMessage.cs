using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThreadChat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDirectMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "direct_message_conversations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    User1Id = table.Column<Guid>(type: "uuid", nullable: false),
                    User2Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_direct_message_conversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_direct_message_conversations_users_User1Id",
                        column: x => x.User1Id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_direct_message_conversations_users_User2Id",
                        column: x => x.User2Id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "direct_messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConversationId = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    FileUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SentAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_direct_messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_direct_messages_direct_message_conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "direct_message_conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_direct_messages_users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_direct_message_conversations_User1Id_User2Id",
                table: "direct_message_conversations",
                columns: new[] { "User1Id", "User2Id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_direct_message_conversations_User2Id",
                table: "direct_message_conversations",
                column: "User2Id");

            migrationBuilder.CreateIndex(
                name: "IX_direct_messages_ConversationId",
                table: "direct_messages",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_direct_messages_SenderId",
                table: "direct_messages",
                column: "SenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "direct_messages");

            migrationBuilder.DropTable(
                name: "direct_message_conversations");
        }
    }
}
