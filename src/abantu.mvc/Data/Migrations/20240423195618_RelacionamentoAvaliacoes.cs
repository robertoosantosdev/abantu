using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace abantu.mvc.Data.Migrations
{
    public partial class RelacionamentoAvaliacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Funcionarios_AvaliadoId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Funcionarios_AvaliadorId",
                table: "Avaliacoes");

            migrationBuilder.RenameColumn(
                name: "AvaliadorId",
                table: "Avaliacoes",
                newName: "IdAvaliador");

            migrationBuilder.RenameColumn(
                name: "AvaliadoId",
                table: "Avaliacoes",
                newName: "IdAvaliado");

            migrationBuilder.RenameIndex(
                name: "IX_Avaliacoes_AvaliadorId",
                table: "Avaliacoes",
                newName: "IX_Avaliacoes_IdAvaliador");

            migrationBuilder.RenameIndex(
                name: "IX_Avaliacoes_AvaliadoId",
                table: "Avaliacoes",
                newName: "IX_Avaliacoes_IdAvaliado");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Funcionarios_IdAvaliado",
                table: "Avaliacoes",
                column: "IdAvaliado",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Funcionarios_IdAvaliador",
                table: "Avaliacoes",
                column: "IdAvaliador",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Funcionarios_IdAvaliado",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Funcionarios_IdAvaliador",
                table: "Avaliacoes");

            migrationBuilder.RenameColumn(
                name: "IdAvaliador",
                table: "Avaliacoes",
                newName: "AvaliadorId");

            migrationBuilder.RenameColumn(
                name: "IdAvaliado",
                table: "Avaliacoes",
                newName: "AvaliadoId");

            migrationBuilder.RenameIndex(
                name: "IX_Avaliacoes_IdAvaliador",
                table: "Avaliacoes",
                newName: "IX_Avaliacoes_AvaliadorId");

            migrationBuilder.RenameIndex(
                name: "IX_Avaliacoes_IdAvaliado",
                table: "Avaliacoes",
                newName: "IX_Avaliacoes_AvaliadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Funcionarios_AvaliadoId",
                table: "Avaliacoes",
                column: "AvaliadoId",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Funcionarios_AvaliadorId",
                table: "Avaliacoes",
                column: "AvaliadorId",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
