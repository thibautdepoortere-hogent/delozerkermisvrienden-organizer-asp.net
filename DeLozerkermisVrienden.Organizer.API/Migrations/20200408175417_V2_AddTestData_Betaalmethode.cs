using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeLozerkermisVrienden.Organizer.API.Migrations
{
    public partial class V2_AddTestData_Betaalmethode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Betaalmethoden",
                columns: new[] { "Id", "AantalDagenVroegerAfsluiten", "Naam", "Opmerking", "Volgorde" },
                values: new object[] { new Guid("574fa379-3144-49c0-b1f1-5e1514e270af"), null, "Contant", null, 1 });

            migrationBuilder.InsertData(
                table: "Betaalmethoden",
                columns: new[] { "Id", "AantalDagenVroegerAfsluiten", "Naam", "Opmerking", "Volgorde" },
                values: new object[] { new Guid("a833e881-dbdb-4a09-a718-a44d1f3ce0ea"), null, "Overschrijving", null, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Betaalmethoden",
                keyColumn: "Id",
                keyValue: new Guid("574fa379-3144-49c0-b1f1-5e1514e270af"));

            migrationBuilder.DeleteData(
                table: "Betaalmethoden",
                keyColumn: "Id",
                keyValue: new Guid("a833e881-dbdb-4a09-a718-a44d1f3ce0ea"));
        }
    }
}
