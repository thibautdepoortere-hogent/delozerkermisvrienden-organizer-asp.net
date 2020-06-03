using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeLozerkermisVrienden.Organizer.API.Migrations
{
    public partial class V4_AddTestData_EvenementCategorie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EvenementCategorieen",
                columns: new[] { "Id", "Naam" },
                values: new object[] { new Guid("f13a3c7e-ead7-42d7-9d09-f3e2c8e292d1"), "Lozerkermis" });

            migrationBuilder.InsertData(
                table: "EvenementCategorieen",
                columns: new[] { "Id", "Naam" },
                values: new object[] { new Guid("c971a053-e944-45ba-9307-229b07c74041"), "Vader-Moederdag" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EvenementCategorieen",
                keyColumn: "Id",
                keyValue: new Guid("c971a053-e944-45ba-9307-229b07c74041"));

            migrationBuilder.DeleteData(
                table: "EvenementCategorieen",
                keyColumn: "Id",
                keyValue: new Guid("f13a3c7e-ead7-42d7-9d09-f3e2c8e292d1"));
        }
    }
}
