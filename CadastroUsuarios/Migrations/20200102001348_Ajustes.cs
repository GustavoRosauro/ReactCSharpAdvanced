using Microsoft.EntityFrameworkCore.Migrations;

namespace CadastroUsuarios.Migrations
{
    public partial class Ajustes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PessoaJuridica_CNPJ",
                table: "PessoaJuridica",
                column: "CNPJ",
                unique: true,
                filter: "[CNPJ] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaFisica_CPF",
                table: "PessoaFisica",
                column: "CPF",
                unique: true,
                filter: "[CPF] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PessoaJuridica_CNPJ",
                table: "PessoaJuridica");

            migrationBuilder.DropIndex(
                name: "IX_PessoaFisica_CPF",
                table: "PessoaFisica");
        }
    }
}
