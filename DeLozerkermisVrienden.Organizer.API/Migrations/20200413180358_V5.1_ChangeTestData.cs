using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeLozerkermisVrienden.Organizer.API.Migrations
{
    public partial class V51_ChangeTestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EvenementCategorieen",
                columns: new[] { "Id", "Naam" },
                values: new object[] { new Guid("6141b45b-e9e4-4684-ae4e-2dae1737de8f"), "Niet gebruikte categorie" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EvenementCategorieen",
                keyColumn: "Id",
                keyValue: new Guid("6141b45b-e9e4-4684-ae4e-2dae1737de8f"));
        }
    }
}
