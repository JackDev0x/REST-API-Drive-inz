using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomovieApi.Migrations
{
    /// <inheritdoc />
    public partial class Update7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "Damaged",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "DoorCount",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "FuelConsumption",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "IsFirstOwner",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "RightHandDrive",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "SeatCount",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "Undamaged",
                table: "Announcements");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Announcements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Damaged",
                table: "Announcements",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoorCount",
                table: "Announcements",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FuelConsumption",
                table: "Announcements",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFirstOwner",
                table: "Announcements",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RightHandDrive",
                table: "Announcements",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeatCount",
                table: "Announcements",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Undamaged",
                table: "Announcements",
                type: "bit",
                nullable: true);
        }
    }
}
