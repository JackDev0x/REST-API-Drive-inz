using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomovieApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedSummaryAndDateposted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<DateTime>(
                name: "DatePosted",
                table: "Announcements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Announcements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "summary");

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "DatePosted",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Announcements");

           
        }
    }
}
