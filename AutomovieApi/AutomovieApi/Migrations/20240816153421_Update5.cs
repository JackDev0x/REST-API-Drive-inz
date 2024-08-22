using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomovieApi.Migrations
{
    /// <inheritdoc />
    public partial class Update5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteUsers_Users_FavoriteId",
                table: "FavoriteUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteUsers_Users_UserId",
                table: "FavoriteUsers");

 

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteUsers",
                table: "FavoriteUsers");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteUsers_FavoriteId",
                table: "FavoriteUsers");



            migrationBuilder.DropColumn(
                name: "FavoriteId",
                table: "FavoriteUsers");

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            //
            migrationBuilder.AddColumn<int>(
                name: "FavoriteId",
                table: "FavoriteUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
            //
            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteUsers",
                table: "FavoriteUsers",
                columns: new[] { "UserId", "FavoriteUserId" });

            //
            migrationBuilder.CreateIndex(
                name: "IX_FavoriteUsers_FavoriteId",
                table: "FavoriteUsers",
                column: "FavoriteId");

            //
            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteUsers_Users_FavoriteId",
                table: "FavoriteUsers",
                column: "FavoriteId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            //
            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteUsers_Users_UserId",
                table: "FavoriteUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
            
        }
    }
}
