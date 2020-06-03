using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeLozerkermisVrienden.Organizer.API.Migrations
{
    public partial class V3_AddTestData_Inschrijvingsstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Inschrijvingsstatussen",
                columns: new[] { "Id", "Naam", "Volgorde" },
                values: new object[,]
                {
                    { new Guid("4c83bb5b-30b2-4ac7-9662-eab035157b86"), "Aangevraagd", 1 },
                    { new Guid("4c2c40d0-c1e9-490b-afb2-5bd9607b7869"), "Goedgekeurd", 2 },
                    { new Guid("adb494b6-10ae-495a-9ba4-48ef04d0e29f"), "Gepland", 3 },
                    { new Guid("febf6bbe-4d18-46b1-846b-eeec0581b482"), "Afgekeurd", 4 },
                    { new Guid("8e7c974e-a3d3-47e6-a19b-9e5a0e1abc3e"), "Geannuleerd", 5 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Inschrijvingsstatussen",
                keyColumn: "Id",
                keyValue: new Guid("4c2c40d0-c1e9-490b-afb2-5bd9607b7869"));

            migrationBuilder.DeleteData(
                table: "Inschrijvingsstatussen",
                keyColumn: "Id",
                keyValue: new Guid("4c83bb5b-30b2-4ac7-9662-eab035157b86"));

            migrationBuilder.DeleteData(
                table: "Inschrijvingsstatussen",
                keyColumn: "Id",
                keyValue: new Guid("8e7c974e-a3d3-47e6-a19b-9e5a0e1abc3e"));

            migrationBuilder.DeleteData(
                table: "Inschrijvingsstatussen",
                keyColumn: "Id",
                keyValue: new Guid("adb494b6-10ae-495a-9ba4-48ef04d0e29f"));

            migrationBuilder.DeleteData(
                table: "Inschrijvingsstatussen",
                keyColumn: "Id",
                keyValue: new Guid("febf6bbe-4d18-46b1-846b-eeec0581b482"));
        }
    }
}
