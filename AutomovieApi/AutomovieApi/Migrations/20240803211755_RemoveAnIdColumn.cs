using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomovieApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAnIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnId",
                table: "FavoriteAnnouncements");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnId",
                table: "FavoriteAnnouncements",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
