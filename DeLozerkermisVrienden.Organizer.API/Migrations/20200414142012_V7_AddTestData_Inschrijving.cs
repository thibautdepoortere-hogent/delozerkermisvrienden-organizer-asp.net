using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeLozerkermisVrienden.Organizer.API.Migrations
{
    public partial class V7_AddTestData_Inschrijving : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inschrijvingen_Evenementen_EvenementId",
                table: "Inschrijvingen");

            migrationBuilder.AlterColumn<Guid>(
                name: "EvenementId",
                table: "Inschrijvingen",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "Inschrijvingen",
                columns: new[] { "Id", "AantalAanhangwagens", "AantalMeter", "AantalMobilhomes", "AantalWagens", "Achternaam", "BetaalmethodeId", "DatumInschrijving", "Email", "EvenementId", "Gemeente", "GestructureerdeMededeling", "InschrijvingsstatusId", "LidId", "Meterprijs", "MobielNummer", "Opmerking", "Postcode", "PrefixMobielNummer", "QRCode", "RedenAfkeuring", "Standnummer", "Voornaam" },
                values: new object[,]
                {
                    { new Guid("a17c29b6-4cc2-4cc8-9a84-592ae2bafbc7"), null, 16, null, 2, "De Poortere", new Guid("a833e881-dbdb-4a09-a718-a44d1f3ce0ea"), new DateTime(2020, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "thibaut.depoortere@student.hogent.be", new Guid("c4660a63-7e82-4e68-92c9-85f3c193f69e"), "Kruisem", "123123412345", new Guid("4c2c40d0-c1e9-490b-afb2-5bd9607b7869"), new Guid("8ed92433-e0ca-42d5-b80c-89415991f1f2"), 1m, "412345678", "We staan met 2 op deze stand.", "9770", "+32", "a17c29b64cc24cc89a84592ae2bafbc7", null, null, "Thibaut" },
                    { new Guid("c565df1d-c604-4ea5-b2da-aedad6e924bf"), null, 5, null, 1, "De Poortere", new Guid("574fa379-3144-49c0-b1f1-5e1514e270af"), new DateTime(2020, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "verzonnenmail03@icloud.com", new Guid("c4660a63-7e82-4e68-92c9-85f3c193f69e"), "Kruisem", null, new Guid("4c2c40d0-c1e9-490b-afb2-5bd9607b7869"), null, 1m, "487654321", "Ik zal bloemstukken en tuindecoratie verkopen.", "9770", "+32", "c565df1dc6044ea5b2daaedad6e924bf", null, null, "Carine" },
                    { new Guid("9b715468-6ac7-40c3-a30c-56d0a797cf45"), null, 3, null, null, "Baert", new Guid("a833e881-dbdb-4a09-a718-a44d1f3ce0ea"), new DateTime(2020, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "verzonnenmail04@gmail.com", new Guid("c4660a63-7e82-4e68-92c9-85f3c193f69e"), "Nederename", null, new Guid("4c83bb5b-30b2-4ac7-9662-eab035157b86"), null, 1m, "412348765", null, "9700", "+32", null, null, null, "Kerensa" },
                    { new Guid("945fa969-22f7-4bf3-8054-eae61f41ae6a"), null, 4, null, 1, "Marysse", new Guid("574fa379-3144-49c0-b1f1-5e1514e270af"), new DateTime(2020, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "klantheeftgeenmail@delozerkermisvrienden.com", new Guid("c4660a63-7e82-4e68-92c9-85f3c193f69e"), "Kruisem", null, new Guid("adb494b6-10ae-495a-9ba4-48ef04d0e29f"), new Guid("3a041df5-32a4-4d86-add2-8f0c16a407aa"), 1m, "456873421", "Speelgoed van reeds opgegroeide kleinkinderen.", "9750", "+32", "945fa96922f74bf38054eae61f41ae6a", null, "235", "Nicole" },
                    { new Guid("8366d2f6-b8a5-4a90-8250-1e4a87e2ab06"), null, 5, null, 1, "Marysse", new Guid("574fa379-3144-49c0-b1f1-5e1514e270af"), new DateTime(2020, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "verzonnenmail05@gmail.com", new Guid("c4660a63-7e82-4e68-92c9-85f3c193f69e"), "Antwerpen", null, new Guid("febf6bbe-4d18-46b1-846b-eeec0581b482"), null, 1m, "418273645", null, "2000", "+32", null, "Reeds 2x online ingeschreven met contante betaling bij inchecken, maar reeds 2x zonder enig tegenbericht niet opgedaagd. Hierdoor wordt u voor deze editie niet toegelaten.", null, "Jonas" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Inschrijvingen_Evenementen_EvenementId",
                table: "Inschrijvingen",
                column: "EvenementId",
                principalTable: "Evenementen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inschrijvingen_Evenementen_EvenementId",
                table: "Inschrijvingen");

            migrationBuilder.DeleteData(
                table: "Inschrijvingen",
                keyColumn: "Id",
                keyValue: new Guid("8366d2f6-b8a5-4a90-8250-1e4a87e2ab06"));

            migrationBuilder.DeleteData(
                table: "Inschrijvingen",
                keyColumn: "Id",
                keyValue: new Guid("945fa969-22f7-4bf3-8054-eae61f41ae6a"));

            migrationBuilder.DeleteData(
                table: "Inschrijvingen",
                keyColumn: "Id",
                keyValue: new Guid("9b715468-6ac7-40c3-a30c-56d0a797cf45"));

            migrationBuilder.DeleteData(
                table: "Inschrijvingen",
                keyColumn: "Id",
                keyValue: new Guid("a17c29b6-4cc2-4cc8-9a84-592ae2bafbc7"));

            migrationBuilder.DeleteData(
                table: "Inschrijvingen",
                keyColumn: "Id",
                keyValue: new Guid("c565df1d-c604-4ea5-b2da-aedad6e924bf"));

            migrationBuilder.AlterColumn<Guid>(
                name: "EvenementId",
                table: "Inschrijvingen",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Inschrijvingen_Evenementen_EvenementId",
                table: "Inschrijvingen",
                column: "EvenementId",
                principalTable: "Evenementen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
