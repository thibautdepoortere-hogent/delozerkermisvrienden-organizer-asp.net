using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeLozerkermisVrienden.Organizer.API.Migrations
{
    public partial class V72_ChangeTestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Inschrijvingen",
                keyColumn: "Id",
                keyValue: new Guid("8366d2f6-b8a5-4a90-8250-1e4a87e2ab06"),
                column: "DatumInschrijving",
                value: new DateTime(2020, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Inschrijvingen",
                keyColumn: "Id",
                keyValue: new Guid("945fa969-22f7-4bf3-8054-eae61f41ae6a"),
                column: "DatumInschrijving",
                value: new DateTime(2020, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Inschrijvingen",
                keyColumn: "Id",
                keyValue: new Guid("9b715468-6ac7-40c3-a30c-56d0a797cf45"),
                column: "DatumInschrijving",
                value: new DateTime(2020, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Inschrijvingen",
                keyColumn: "Id",
                keyValue: new Guid("c565df1d-c604-4ea5-b2da-aedad6e924bf"),
                column: "DatumInschrijving",
                value: new DateTime(2020, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Inschrijvingen",
                keyColumn: "Id",
                keyValue: new Guid("8366d2f6-b8a5-4a90-8250-1e4a87e2ab06"),
                column: "DatumInschrijving",
                value: new DateTime(2020, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Inschrijvingen",
                keyColumn: "Id",
                keyValue: new Guid("945fa969-22f7-4bf3-8054-eae61f41ae6a"),
                column: "DatumInschrijving",
                value: new DateTime(2020, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Inschrijvingen",
                keyColumn: "Id",
                keyValue: new Guid("9b715468-6ac7-40c3-a30c-56d0a797cf45"),
                column: "DatumInschrijving",
                value: new DateTime(2020, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Inschrijvingen",
                keyColumn: "Id",
                keyValue: new Guid("c565df1d-c604-4ea5-b2da-aedad6e924bf"),
                column: "DatumInschrijving",
                value: new DateTime(2020, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
