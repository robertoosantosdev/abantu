using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace abantu.mvc.Data.Migrations
{
    public partial class CorrecaoNomeRealizadaEm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RealizadoEm",
                table: "Avaliacoes",
                newName: "RealizadaEm");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RealizadaEm",
                table: "Avaliacoes",
                newName: "RealizadoEm");
        }
    }
}
