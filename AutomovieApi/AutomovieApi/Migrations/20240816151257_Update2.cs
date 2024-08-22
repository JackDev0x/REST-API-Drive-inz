using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomovieApi.Migrations
{
    /// <inheritdoc />
    public partial class Update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Generations_Models_ModelId",
                table: "Generations");

            migrationBuilder.DropColumn(
                name: "AvgPrice_0_50000",
                table: "Generations");

            migrationBuilder.DropColumn(
                name: "AvgPrice_100001_150000",
                table: "Generations");

            migrationBuilder.DropColumn(
                name: "AvgPrice_150001_200000",
                table: "Generations");

            migrationBuilder.DropColumn(
                name: "AvgPrice_200001_250000",
                table: "Generations");

            migrationBuilder.DropColumn(
                name: "AvgPrice_250001_300000",
                table: "Generations");

            migrationBuilder.DropColumn(
                name: "AvgPrice_50001_100000",
                table: "Generations");

            migrationBuilder.AlterColumn<int>(
                name: "ModelId",
                table: "Generations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Generations_Models_ModelId",
                table: "Generations",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "ModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Generations_Models_ModelId",
                table: "Generations");

            migrationBuilder.AlterColumn<int>(
                name: "ModelId",
                table: "Generations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AvgPrice_0_50000",
                table: "Generations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AvgPrice_100001_150000",
                table: "Generations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AvgPrice_150001_200000",
                table: "Generations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AvgPrice_200001_250000",
                table: "Generations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AvgPrice_250001_300000",
                table: "Generations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AvgPrice_50001_100000",
                table: "Generations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Generations_Models_ModelId",
                table: "Generations",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "ModelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
