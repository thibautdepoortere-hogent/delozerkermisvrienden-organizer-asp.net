using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeLozerkermisVrienden.Organizer.API.Migrations
{
    public partial class V61_ChangeTestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Leden",
                keyColumn: "Id",
                keyValue: new Guid("3a041df5-32a4-4d86-add2-8f0c16a407aa"),
                column: "Email",
                value: "verzonnenmail01@gmail.com");

            migrationBuilder.UpdateData(
                table: "Leden",
                keyColumn: "Id",
                keyValue: new Guid("b7096da3-4070-43e1-99c9-ebf4a1829236"),
                column: "Email",
                value: "verzonnenmail02@outlook.com");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Leden",
                keyColumn: "Id",
                keyValue: new Guid("3a041df5-32a4-4d86-add2-8f0c16a407aa"),
                column: "Email",
                value: "thibaut.depoortere@student.hogent.be");

            migrationBuilder.UpdateData(
                table: "Leden",
                keyColumn: "Id",
                keyValue: new Guid("b7096da3-4070-43e1-99c9-ebf4a1829236"),
                column: "Email",
                value: "thibaut.depoortere@student.hogent.be");
        }
    }
}
