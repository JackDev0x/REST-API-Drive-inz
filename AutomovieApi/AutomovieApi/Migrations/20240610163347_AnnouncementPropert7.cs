using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomovieApi.Migrations
{
    /// <inheritdoc />
    public partial class AnnouncementPropert7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Users_UserId",
                table: "Announcements");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Announcements_AnnouncementAnId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "PhotoLink",
                table: "Announcements");

            migrationBuilder.RenameColumn(
                name: "AnnouncementAnId",
                table: "Comments",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_AnnouncementAnId",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Voivodeship",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatePosted",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Announcements",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AnnouncementImages",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementImages", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_AnnouncementImages_Announcements_AnId",
                        column: x => x.AnId,
                        principalTable: "Announcements",
                        principalColumn: "AnId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteAnnouncements",
                columns: table => new
                {
                    FavoriteAnnouncementId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AnId = table.Column<int>(type: "int", nullable: false),
                    AnnouncementAnId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteAnnouncements", x => new { x.UserId, x.FavoriteAnnouncementId });
                    table.ForeignKey(
                        name: "FK_FavoriteAnnouncements_Announcements_AnnouncementAnId",
                        column: x => x.AnnouncementAnId,
                        principalTable: "Announcements",
                        principalColumn: "AnId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteAnnouncements_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteUsers",
                columns: table => new
                {
                    FavoriteUserId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FavoriteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteUsers", x => new { x.UserId, x.FavoriteUserId });
                    table.ForeignKey(
                        name: "FK_FavoriteUsers_Users_FavoriteId",
                        column: x => x.FavoriteId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FavoriteUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AnId",
                table: "Comments",
                column: "AnId");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_Slug",
                table: "Announcements",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementImages_AnId",
                table: "AnnouncementImages",
                column: "AnId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteAnnouncements_AnnouncementAnId",
                table: "FavoriteAnnouncements",
                column: "AnnouncementAnId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteUsers_FavoriteId",
                table: "FavoriteUsers",
                column: "FavoriteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Users_UserId",
                table: "Announcements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Announcements_AnId",
                table: "Comments",
                column: "AnId",
                principalTable: "Announcements",
                principalColumn: "AnId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Users_UserId",
                table: "Announcements");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Announcements_AnId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "AnnouncementImages");

            migrationBuilder.DropTable(
                name: "FavoriteAnnouncements");

            migrationBuilder.DropTable(
                name: "FavoriteUsers");

            migrationBuilder.DropIndex(
                name: "IX_Comments_AnId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_Slug",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Voivodeship",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DatePosted",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Announcements");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Comments",
                newName: "AnnouncementAnId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                newName: "IX_Comments_AnnouncementAnId");

            migrationBuilder.AddColumn<string>(
                name: "PhotoLink",
                table: "Announcements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Users_UserId",
                table: "Announcements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Announcements_AnnouncementAnId",
                table: "Comments",
                column: "AnnouncementAnId",
                principalTable: "Announcements",
                principalColumn: "AnId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
