using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeLozerkermisVrienden.Organizer.API.Migrations
{
    public partial class V10_ChangeEntity_Login : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Logins",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "LidId", "Token", "Wachtwoord" },
                values: new object[] { new Guid("8ed92433-e0ca-42d5-b80c-89415991f1f2"), null, "Hier komt een encrypted wachtwoord!" });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "LidId", "Token", "Wachtwoord" },
                values: new object[] { new Guid("3a041df5-32a4-4d86-add2-8f0c16a407aa"), null, "Hier komt een encrypted wachtwoord!" });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "LidId", "Token", "Wachtwoord" },
                values: new object[] { new Guid("ce467c89-7619-45aa-be44-ec214b54aca0"), null, "Te verwijderen door Lid" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Logins",
                keyColumn: "LidId",
                keyValue: new Guid("3a041df5-32a4-4d86-add2-8f0c16a407aa"));

            migrationBuilder.DeleteData(
                table: "Logins",
                keyColumn: "LidId",
                keyValue: new Guid("8ed92433-e0ca-42d5-b80c-89415991f1f2"));

            migrationBuilder.DeleteData(
                table: "Logins",
                keyColumn: "LidId",
                keyValue: new Guid("ce467c89-7619-45aa-be44-ec214b54aca0"));

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Logins");
        }
    }
}
