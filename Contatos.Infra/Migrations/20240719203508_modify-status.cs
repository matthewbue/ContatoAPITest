using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contatos.Infra.Migrations
{
    /// <inheritdoc />
    public partial class modifystatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Contato",
                newName: "Ativo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ativo",
                table: "Contato",
                newName: "Status");
        }
    }
}
