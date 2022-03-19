using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarvelHeroes.Data.Migrations
{
    public partial class DatabaseGeneration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMarvel = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 2147483647, nullable: true),
                    ImageLink = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    WikiLink = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hero", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "hq",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMarvel = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 2147483647, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ImageLink = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    WikiLink = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hq", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_hero_Guid",
                table: "hero",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_hq_Guid",
                table: "hq",
                column: "Guid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hero");

            migrationBuilder.DropTable(
                name: "hq");
        }
    }
}
