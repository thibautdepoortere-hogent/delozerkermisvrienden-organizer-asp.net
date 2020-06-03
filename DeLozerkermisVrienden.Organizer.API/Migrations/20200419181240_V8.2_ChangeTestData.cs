using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeLozerkermisVrienden.Organizer.API.Migrations
{
    public partial class V82_ChangeTestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Inschrijvingen",
                keyColumn: "Id",
                keyValue: new Guid("945fa969-22f7-4bf3-8054-eae61f41ae6a"),
                column: "LidId",
                value: new Guid("8ed92433-e0ca-42d5-b80c-89415991f1f2"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Inschrijvingen",
                keyColumn: "Id",
                keyValue: new Guid("945fa969-22f7-4bf3-8054-eae61f41ae6a"),
                column: "LidId",
                value: new Guid("3a041df5-32a4-4d86-add2-8f0c16a407aa"));
        }
    }
}
