using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SME.Pedagogico.Repository.Migrations
{
    public partial class TesteLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Guid = table.Column<string>(nullable: false),
                    Metodo = table.Column<string>(nullable: false),
                    Parametros = table.Column<string>(nullable: true),
                    Servico = table.Column<string>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    Usuario = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Guid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}
