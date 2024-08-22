using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomovieApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFavoriteAnnouncementsKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteAnnouncements_Announcements_AnnouncementAnId",
                table: "FavoriteAnnouncements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteAnnouncements",
                table: "FavoriteAnnouncements");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteAnnouncements",
                table: "FavoriteAnnouncements",
                columns: new[] { "UserId", "AnnouncementAnId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteAnnouncements_Announcements_AnnouncementAnId",
                table: "FavoriteAnnouncements",
                column: "AnnouncementAnId",
                principalTable: "Announcements",
                principalColumn: "AnId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteAnnouncements_Announcements_AnnouncementAnId",
                table: "FavoriteAnnouncements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteAnnouncements",
                table: "FavoriteAnnouncements");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteAnnouncements",
                table: "FavoriteAnnouncements",
                columns: new[] { "UserId", "FavoriteAnnouncementId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteAnnouncements_Announcements_AnnouncementAnId",
                table: "FavoriteAnnouncements",
                column: "AnnouncementAnId",
                principalTable: "Announcements",
                principalColumn: "AnId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
