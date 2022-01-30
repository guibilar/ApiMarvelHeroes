using Microsoft.EntityFrameworkCore.Migrations;

namespace PigOut.Data.Migrations
{
    public partial class configauto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Wiki_url",
                table: "personagem",
                newName: "LinkWiki");

            migrationBuilder.RenameColumn(
                name: "Pic_url",
                table: "personagem",
                newName: "LinkImagem");

            migrationBuilder.RenameColumn(
                name: "Id_marvel",
                table: "personagem",
                newName: "IdMarvel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LinkWiki",
                table: "personagem",
                newName: "Wiki_url");

            migrationBuilder.RenameColumn(
                name: "LinkImagem",
                table: "personagem",
                newName: "Pic_url");

            migrationBuilder.RenameColumn(
                name: "IdMarvel",
                table: "personagem",
                newName: "Id_marvel");
        }
    }
}
