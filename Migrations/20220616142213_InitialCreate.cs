using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeachSys.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Armario",
                columns: table => new
                {
                    ArmarioId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Regiao = table.Column<string>(nullable: true),
                    PontoX = table.Column<string>(nullable: true),
                    PontoY = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Armario", x => x.ArmarioId);
                });

            migrationBuilder.CreateTable(
                name: "Cadastro",
                columns: table => new
                {
                    CadastroId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Cpf = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cadastro", x => x.CadastroId);
                });

            migrationBuilder.CreateTable(
                name: "Compartimento",
                columns: table => new
                {
                    CompartimentoId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Numero = table.Column<int>(nullable: false),
                    Tamanho = table.Column<string>(nullable: true),
                    Disponivel = table.Column<bool>(nullable: false),
                    Trancado = table.Column<bool>(nullable: false),
                    CadastroId = table.Column<int>(nullable: true),
                    ArmarioId = table.Column<int>(nullable: true),
                    Regiao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compartimento", x => x.CompartimentoId);
                    table.ForeignKey(
                        name: "FK_Compartimento_Armario_ArmarioId",
                        column: x => x.ArmarioId,
                        principalTable: "Armario",
                        principalColumn: "ArmarioId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Compartimento_Cadastro_CadastroId",
                        column: x => x.CadastroId,
                        principalTable: "Cadastro",
                        principalColumn: "CadastroId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compartimento_ArmarioId",
                table: "Compartimento",
                column: "ArmarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Compartimento_CadastroId",
                table: "Compartimento",
                column: "CadastroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compartimento");

            migrationBuilder.DropTable(
                name: "Armario");

            migrationBuilder.DropTable(
                name: "Cadastro");
        }
    }
}
