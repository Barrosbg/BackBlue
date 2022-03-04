using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoBlue.Migrations
{
    public partial class mudancao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Usuarios_UsuarioId",
                table: "Agendamentos");

            migrationBuilder.DropIndex(
                name: "IX_Agendamentos_UsuarioId",
                table: "Agendamentos");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Agendamentos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Agendamentos");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Agendamentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Agendamentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Agendamentos");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Agendamentos");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Agendamentos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
    }
}
