using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomovieApi.Migrations
{
    /// <inheritdoc />
    public partial class Update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Najpierw usuń indeks
            migrationBuilder.DropIndex(
                name: "IX_Generations_ModelId",
                table: "Generations");

            // Usuń klucz obcy
            migrationBuilder.DropForeignKey(
                name: "FK_Generations_Models_ModelId",
                table: "Generations");

            // Usuń kolumnę
            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "Generations");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Najpierw dodaj kolumnę z powrotem
            migrationBuilder.AddColumn<int>(
                name: "ModelId",
                table: "Generations",
                type: "int",
                nullable: true);

            // Następnie dodaj klucz obcy
            migrationBuilder.AddForeignKey(
                name: "FK_Generations_Models_ModelId",
                table: "Generations",
                column: "ModelId",
                principalTable: "Models",  // Popraw tabelę docelową, jeśli to jest tabela "Models", a nie "Generations"
                principalColumn: "ModelId", // Popraw kolumnę docelową, jeśli to jest "ModelId" w tabeli "Models"
                onDelete: ReferentialAction.Restrict);

            // Dodaj indeks z powrotem
            migrationBuilder.CreateIndex(
                name: "IX_Generations_ModelId",
                table: "Generations",
                column: "ModelId");
        }

    }
}
