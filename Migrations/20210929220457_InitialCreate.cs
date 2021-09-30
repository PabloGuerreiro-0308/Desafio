using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DESAFIOO.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "estudantes",
                columns: table => new
                {
                    matricula = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estudantes", x => x.matricula);
                });

            migrationBuilder.CreateTable(
                name: "notas",
                columns: table => new
                {
                    codDisciplina = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nomeDisciplina = table.Column<string>(nullable: true),
                    av1 = table.Column<float>(nullable: false),
                    av2 = table.Column<float>(nullable: false),
                    av3 = table.Column<float>(nullable: false),
                    media = table.Column<float>(nullable: false),
                    matricula = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notas", x => x.codDisciplina);
                    table.ForeignKey(
                        name: "FK_notas_estudantes_matricula",
                        column: x => x.matricula,
                        principalTable: "estudantes",
                        principalColumn: "matricula",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_notas_matricula",
                table: "notas",
                column: "matricula");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notas");

            migrationBuilder.DropTable(
                name: "estudantes");
        }
    }
}
