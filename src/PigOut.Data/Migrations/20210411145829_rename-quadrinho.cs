using Microsoft.EntityFrameworkCore.Migrations;

namespace PigOut.Data.Migrations
{
    public partial class renamequadrinho : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Wiki_url",
                table: "quadrinhos",
                newName: "LinkWiki");

            migrationBuilder.RenameColumn(
                name: "Pic_url",
                table: "quadrinhos",
                newName: "LinkImagem");

            migrationBuilder.RenameColumn(
                name: "Id_marvel",
                table: "quadrinhos",
                newName: "IdMarvel");

            migrationBuilder.CreateIndex(
                name: "IX_quadrinhos_Guid",
                table: "quadrinhos",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_personagem_Guid",
                table: "personagem",
                column: "Guid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_quadrinhos_Guid",
                table: "quadrinhos");

            migrationBuilder.DropIndex(
                name: "IX_personagem_Guid",
                table: "personagem");

            migrationBuilder.RenameColumn(
                name: "LinkWiki",
                table: "quadrinhos",
                newName: "Wiki_url");

            migrationBuilder.RenameColumn(
                name: "LinkImagem",
                table: "quadrinhos",
                newName: "Pic_url");

            migrationBuilder.RenameColumn(
                name: "IdMarvel",
                table: "quadrinhos",
                newName: "Id_marvel");
        }
    }
}
