using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomovieApi.Migrations
{
    /// <inheritdoc />
    public partial class Update9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nick",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "lan",
                table: "Users",
                newName: "lat");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lat",
                table: "Users",
                newName: "lan");

            migrationBuilder.AddColumn<string>(
                name: "Nick",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
