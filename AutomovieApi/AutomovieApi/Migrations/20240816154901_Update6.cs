using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomovieApi.Migrations
{
    /// <inheritdoc />
    public partial class Update6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Usunięcie tabeli FavoriteUsers
            migrationBuilder.DropTable(
                name: "FavoriteUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Przywrócenie tabeli FavoriteUsers, jeśli trzeba cofnąć migrację
            migrationBuilder.CreateTable(
                name: "FavoriteUsers",
                columns: table => new
                {
                    FavoriteUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteUsers", x => x.FavoriteUserId);
                });
        }
    }
}
