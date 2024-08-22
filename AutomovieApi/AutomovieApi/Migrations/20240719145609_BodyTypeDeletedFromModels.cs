using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomovieApi.Migrations
{
    /// <inheritdoc />
    public partial class BodyTypeDeletedFromModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BodyType",
                table: "Models");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BodyType",
                table: "Models",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
