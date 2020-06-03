using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeLozerkermisVrienden.Organizer.API.Migrations
{
    public partial class V6_AddTestData_Lid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Leden",
                columns: new[] { "Id", "Achternaam", "Actief", "Email", "Voornaam" },
                values: new object[] { new Guid("8ed92433-e0ca-42d5-b80c-89415991f1f2"), "De Poortere", true, "thibaut.depoortere@student.hogent.be", "Thibaut" });

            migrationBuilder.InsertData(
                table: "Leden",
                columns: new[] { "Id", "Achternaam", "Actief", "Email", "Voornaam" },
                values: new object[] { new Guid("3a041df5-32a4-4d86-add2-8f0c16a407aa"), "De Poortere", true, "thibaut.depoortere@student.hogent.be", "Filip" });

            migrationBuilder.InsertData(
                table: "Leden",
                columns: new[] { "Id", "Achternaam", "Actief", "Email", "Voornaam" },
                values: new object[] { new Guid("b7096da3-4070-43e1-99c9-ebf4a1829236"), "Van Cauwenberghe", false, "thibaut.depoortere@student.hogent.be", "Jo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Leden",
                keyColumn: "Id",
                keyValue: new Guid("3a041df5-32a4-4d86-add2-8f0c16a407aa"));

            migrationBuilder.DeleteData(
                table: "Leden",
                keyColumn: "Id",
                keyValue: new Guid("8ed92433-e0ca-42d5-b80c-89415991f1f2"));

            migrationBuilder.DeleteData(
                table: "Leden",
                keyColumn: "Id",
                keyValue: new Guid("b7096da3-4070-43e1-99c9-ebf4a1829236"));
        }
    }
}
