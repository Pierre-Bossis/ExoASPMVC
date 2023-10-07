using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExoASP.Migrations
{
    public partial class GameTableIntermediaire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Editeur = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AnneeDeSortie = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateAjout = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_games_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserGame",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    DateAchat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstOccasion = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGame", x => new { x.UserId, x.GameId });
                    table.ForeignKey(
                        name: "FK_UserGame_games_GameId",
                        column: x => x.GameId,
                        principalTable: "games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserGame_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_games_CreatorId",
                table: "games",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGame_GameId",
                table: "UserGame",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserGame");

            migrationBuilder.DropTable(
                name: "games");
        }
    }
}
