using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeLozerkermisVrienden.Organizer.API.Migrations
{
    public partial class V21_ChangeTestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Betaalmethoden",
                keyColumn: "Id",
                keyValue: new Guid("574fa379-3144-49c0-b1f1-5e1514e270af"),
                column: "Opmerking",
                value: "U betaalt bij het inchecken op de dag van het evenement zelf OF U betaalt ervoor aan een lid van de vereniging.");

            migrationBuilder.UpdateData(
                table: "Betaalmethoden",
                keyColumn: "Id",
                keyValue: new Guid("a833e881-dbdb-4a09-a718-a44d1f3ce0ea"),
                column: "AantalDagenVroegerAfsluiten",
                value: 5);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Betaalmethoden",
                keyColumn: "Id",
                keyValue: new Guid("574fa379-3144-49c0-b1f1-5e1514e270af"),
                column: "Opmerking",
                value: null);

            migrationBuilder.UpdateData(
                table: "Betaalmethoden",
                keyColumn: "Id",
                keyValue: new Guid("a833e881-dbdb-4a09-a718-a44d1f3ce0ea"),
                column: "AantalDagenVroegerAfsluiten",
                value: null);
        }
    }
}
