using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeLozerkermisVrienden.Organizer.API.Migrations
{
    public partial class V12_ChangeEntity_Nieuwsbrief : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nieuwsbrieven_Evenementen_EvenementId",
                table: "Nieuwsbrieven");

            migrationBuilder.AlterColumn<Guid>(
                name: "EvenementId",
                table: "Nieuwsbrieven",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Nieuwsbrieven_Evenementen_EvenementId",
                table: "Nieuwsbrieven",
                column: "EvenementId",
                principalTable: "Evenementen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nieuwsbrieven_Evenementen_EvenementId",
                table: "Nieuwsbrieven");

            migrationBuilder.AlterColumn<Guid>(
                name: "EvenementId",
                table: "Nieuwsbrieven",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Nieuwsbrieven_Evenementen_EvenementId",
                table: "Nieuwsbrieven",
                column: "EvenementId",
                principalTable: "Evenementen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
