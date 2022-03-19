using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MarvelHeroes.Data.Migrations
{
    public partial class commitdominio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "personagem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_marvel = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 2147483647, nullable: true),
                    Pic_url = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    Wiki_url = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personagem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "quadrinhos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_marvel = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 2147483647, nullable: true),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Pic_url = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    Wiki_url = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quadrinhos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "personagem");

            migrationBuilder.DropTable(
                name: "quadrinhos");
        }
    }
}
