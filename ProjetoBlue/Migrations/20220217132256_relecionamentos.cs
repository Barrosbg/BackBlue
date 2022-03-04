using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoBlue.Migrations
{
    public partial class relecionamentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Agendamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_UsuarioId",
                table: "Agendamentos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Usuarios_UsuarioId",
                table: "Agendamentos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Usuarios_UsuarioId",
                table: "Agendamentos");

            migrationBuilder.DropIndex(
                name: "IX_Agendamentos_UsuarioId",
                table: "Agendamentos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Agendamentos");
        }
    }
}
