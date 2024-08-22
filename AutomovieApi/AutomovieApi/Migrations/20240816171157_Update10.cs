using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomovieApi.Migrations
{
    /// <inheritdoc />
    public partial class Update10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompany",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompany",
                table: "Users",
                type: "bit",
                nullable: true);
        }
    }
}
