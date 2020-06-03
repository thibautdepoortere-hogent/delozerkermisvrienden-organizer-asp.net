using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeLozerkermisVrienden.Organizer.API.Migrations
{
    public partial class V71_ChangeTestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Betaalmethoden",
                columns: new[] { "Id", "AantalDagenVroegerAfsluiten", "Naam", "Opmerking", "Volgorde" },
                values: new object[] { new Guid("608f19f1-05a2-4b56-96df-ad7af75d7893"), null, "MasterCard", null, 10 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Betaalmethoden",
                keyColumn: "Id",
                keyValue: new Guid("608f19f1-05a2-4b56-96df-ad7af75d7893"));
        }
    }
}
