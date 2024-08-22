using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomovieApi.Migrations
{
    /// <inheritdoc />
    public partial class ListsOfFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Announcements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "lan",
                table: "Announcements",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "lng",
                table: "Announcements",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "DriverAssistanceSystemsDataset",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    feature = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverAssistanceSystemsDataset", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MultimediaDataset",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    feature = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultimediaDataset", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OtherDataset",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    feature = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherDataset", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceDataset",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    feature = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceDataset", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SafetyDataset",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    feature = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyDataset", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DriverAssistanceSystems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnId = table.Column<int>(type: "int", nullable: false),
                    feature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    featureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverAssistanceSystems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverAssistanceSystems_Announcements_AnId",
                        column: x => x.AnId,
                        principalTable: "Announcements",
                        principalColumn: "AnId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverAssistanceSystems_DriverAssistanceSystemsDataset_featureId",
                        column: x => x.featureId,
                        principalTable: "DriverAssistanceSystemsDataset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Multimedia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnId = table.Column<int>(type: "int", nullable: false),
                    feature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    featureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Multimedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Multimedia_Announcements_AnId",
                        column: x => x.AnId,
                        principalTable: "Announcements",
                        principalColumn: "AnId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Multimedia_MultimediaDataset_featureId",
                        column: x => x.featureId,
                        principalTable: "MultimediaDataset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Other",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnId = table.Column<int>(type: "int", nullable: false),
                    feature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    featureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Other", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Other_Announcements_AnId",
                        column: x => x.AnId,
                        principalTable: "Announcements",
                        principalColumn: "AnId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Other_OtherDataset_featureId",
                        column: x => x.featureId,
                        principalTable: "OtherDataset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Performance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnId = table.Column<int>(type: "int", nullable: false),
                    feature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    featureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Performance_Announcements_AnId",
                        column: x => x.AnId,
                        principalTable: "Announcements",
                        principalColumn: "AnId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Performance_PerformanceDataset_featureId",
                        column: x => x.featureId,
                        principalTable: "PerformanceDataset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Safety",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnId = table.Column<int>(type: "int", nullable: false),
                    feature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    featureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Safety", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Safety_Announcements_AnId",
                        column: x => x.AnId,
                        principalTable: "Announcements",
                        principalColumn: "AnId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Safety_SafetyDataset_featureId",
                        column: x => x.featureId,
                        principalTable: "SafetyDataset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverAssistanceSystems_AnId",
                table: "DriverAssistanceSystems",
                column: "AnId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverAssistanceSystems_featureId",
                table: "DriverAssistanceSystems",
                column: "featureId");

            migrationBuilder.CreateIndex(
                name: "IX_Multimedia_AnId",
                table: "Multimedia",
                column: "AnId");

            migrationBuilder.CreateIndex(
                name: "IX_Multimedia_featureId",
                table: "Multimedia",
                column: "featureId");

            migrationBuilder.CreateIndex(
                name: "IX_Other_AnId",
                table: "Other",
                column: "AnId");

            migrationBuilder.CreateIndex(
                name: "IX_Other_featureId",
                table: "Other",
                column: "featureId");

            migrationBuilder.CreateIndex(
                name: "IX_Performance_AnId",
                table: "Performance",
                column: "AnId");

            migrationBuilder.CreateIndex(
                name: "IX_Performance_featureId",
                table: "Performance",
                column: "featureId");

            migrationBuilder.CreateIndex(
                name: "IX_Safety_AnId",
                table: "Safety",
                column: "AnId");

            migrationBuilder.CreateIndex(
                name: "IX_Safety_featureId",
                table: "Safety",
                column: "featureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverAssistanceSystems");

            migrationBuilder.DropTable(
                name: "Multimedia");

            migrationBuilder.DropTable(
                name: "Other");

            migrationBuilder.DropTable(
                name: "Performance");

            migrationBuilder.DropTable(
                name: "Safety");

            migrationBuilder.DropTable(
                name: "DriverAssistanceSystemsDataset");

            migrationBuilder.DropTable(
                name: "MultimediaDataset");

            migrationBuilder.DropTable(
                name: "OtherDataset");

            migrationBuilder.DropTable(
                name: "PerformanceDataset");

            migrationBuilder.DropTable(
                name: "SafetyDataset");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "lan",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "lng",
                table: "Announcements");
        }
    }
}
