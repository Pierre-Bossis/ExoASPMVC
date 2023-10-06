using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExoASP.Migrations
{
    public partial class Init_Game : Migration
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_games_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_games_UserId",
                table: "games",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "games");
        }
    }
}
