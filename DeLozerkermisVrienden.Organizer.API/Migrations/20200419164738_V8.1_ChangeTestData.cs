using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeLozerkermisVrienden.Organizer.API.Migrations
{
    public partial class V81_ChangeTestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EvenementCategorieen",
                keyColumn: "Id",
                keyValue: new Guid("6141b45b-e9e4-4684-ae4e-2dae1737de8f"));

            migrationBuilder.UpdateData(
                table: "Betaalmethoden",
                keyColumn: "Id",
                keyValue: new Guid("608f19f1-05a2-4b56-96df-ad7af75d7893"),
                column: "Naam",
                value: "Te verwijderen");

            migrationBuilder.InsertData(
                table: "Betaaltransacties",
                columns: new[] { "Id", "Bedrag", "BetaalmethodeId", "DatumBetaling", "GestructureerdeMededeling", "InschrijvingsId", "LidId", "VerantwoordelijkeBetaling" },
                values: new object[] { new Guid("5cc00408-02a2-4a86-a88b-d6b3f0147a48"), 30m, new Guid("574fa379-3144-49c0-b1f1-5e1514e270af"), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("945fa969-22f7-4bf3-8054-eae61f41ae6a"), new Guid("3a041df5-32a4-4d86-add2-8f0c16a407aa"), "Te verwijderen" });

            migrationBuilder.InsertData(
                table: "EvenementCategorieen",
                columns: new[] { "Id", "Naam" },
                values: new object[] { new Guid("77d78a2a-4790-4394-9c31-05ae08108628"), "Te verwijderen" });

            migrationBuilder.InsertData(
                table: "Evenementen",
                columns: new[] { "Id", "DatumEindeEvenement", "DatumEindeInschrijvingen", "DatumStartEvenement", "DatumStartInschrijvingen", "EvenementCategorieId", "Naam" },
                values: new object[] { new Guid("254df418-2646-44e6-9ae6-78ebd29475f8"), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Te verwijderen" });

            migrationBuilder.InsertData(
                table: "Inschrijvingen",
                columns: new[] { "Id", "AantalAanhangwagens", "AantalMeter", "AantalMobilhomes", "AantalWagens", "Achternaam", "BetaalmethodeId", "DatumInschrijving", "Email", "EvenementId", "Gemeente", "GestructureerdeMededeling", "InschrijvingsstatusId", "LidId", "Meterprijs", "MobielNummer", "Opmerking", "Postcode", "PrefixMobielNummer", "QRCode", "RedenAfkeuring", "Standnummer", "Voornaam" },
                values: new object[] { new Guid("c7a1c924-711b-4c81-962d-b3a96ffd2e8a"), null, 30, null, 1, "Inschrijving", new Guid("574fa379-3144-49c0-b1f1-5e1514e270af"), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "verzonnenmail10@gmail.com", new Guid("c4660a63-7e82-4e68-92c9-85f3c193f69e"), "Gent", null, new Guid("febf6bbe-4d18-46b1-846b-eeec0581b482"), null, 1m, "418273645", null, "9000", "+32", null, "Te verwijderen", null, "Tijdelijke" });

            migrationBuilder.UpdateData(
                table: "Inschrijvingsstatussen",
                keyColumn: "Id",
                keyValue: new Guid("8e7c974e-a3d3-47e6-a19b-9e5a0e1abc3e"),
                column: "Naam",
                value: "te verwijderen");

            migrationBuilder.InsertData(
                table: "Leden",
                columns: new[] { "Id", "Achternaam", "Actief", "Email", "Voornaam" },
                values: new object[] { new Guid("ce467c89-7619-45aa-be44-ec214b54aca0"), "Te verwijderen", false, "verzonnenmail20@outlook.com", "Tijdelijk" });

            migrationBuilder.InsertData(
                table: "Betaaltransacties",
                columns: new[] { "Id", "Bedrag", "BetaalmethodeId", "DatumBetaling", "GestructureerdeMededeling", "InschrijvingsId", "LidId", "VerantwoordelijkeBetaling" },
                values: new object[] { new Guid("68cb7db0-4af5-4469-9838-099ee2b1c8c0"), 30m, new Guid("574fa379-3144-49c0-b1f1-5e1514e270af"), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("c7a1c924-711b-4c81-962d-b3a96ffd2e8a"), new Guid("3a041df5-32a4-4d86-add2-8f0c16a407aa"), "Te verwijderen door verwijderen inschrijving" });

            migrationBuilder.InsertData(
                table: "Evenementen",
                columns: new[] { "Id", "DatumEindeEvenement", "DatumEindeInschrijvingen", "DatumStartEvenement", "DatumStartInschrijvingen", "EvenementCategorieId", "Naam" },
                values: new object[] { new Guid("fcaa7d8a-4b3a-4282-962e-2e1bf90d3448"), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("77d78a2a-4790-4394-9c31-05ae08108628"), "Te verwijderen door verwijderen evenementGroep" });

            migrationBuilder.InsertData(
                table: "Inschrijvingen",
                columns: new[] { "Id", "AantalAanhangwagens", "AantalMeter", "AantalMobilhomes", "AantalWagens", "Achternaam", "BetaalmethodeId", "DatumInschrijving", "Email", "EvenementId", "Gemeente", "GestructureerdeMededeling", "InschrijvingsstatusId", "LidId", "Meterprijs", "MobielNummer", "Opmerking", "Postcode", "PrefixMobielNummer", "QRCode", "RedenAfkeuring", "Standnummer", "Voornaam" },
                values: new object[] { new Guid("c1dea00c-0513-4ebf-8c01-306c6533a270"), null, 30, null, 1, "Inschrijving", new Guid("574fa379-3144-49c0-b1f1-5e1514e270af"), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "verzonnenmail11@gmail.com", new Guid("254df418-2646-44e6-9ae6-78ebd29475f8"), "Gent", null, new Guid("febf6bbe-4d18-46b1-846b-eeec0581b482"), null, 1m, "418273645", null, "9000", "+32", null, "Te verwijderen door verwijderen evenement.", null, "Tijdelijke" });

            migrationBuilder.InsertData(
                table: "Betaaltransacties",
                columns: new[] { "Id", "Bedrag", "BetaalmethodeId", "DatumBetaling", "GestructureerdeMededeling", "InschrijvingsId", "LidId", "VerantwoordelijkeBetaling" },
                values: new object[] { new Guid("8c72a872-c977-4574-9abc-8c9e911dc4a5"), 30m, new Guid("574fa379-3144-49c0-b1f1-5e1514e270af"), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("c1dea00c-0513-4ebf-8c01-306c6533a270"), new Guid("3a041df5-32a4-4d86-add2-8f0c16a407aa"), "Te verwijderen door verwijderen Evenement" });

            migrationBuilder.InsertData(
                table: "Inschrijvingen",
                columns: new[] { "Id", "AantalAanhangwagens", "AantalMeter", "AantalMobilhomes", "AantalWagens", "Achternaam", "BetaalmethodeId", "DatumInschrijving", "Email", "EvenementId", "Gemeente", "GestructureerdeMededeling", "InschrijvingsstatusId", "LidId", "Meterprijs", "MobielNummer", "Opmerking", "Postcode", "PrefixMobielNummer", "QRCode", "RedenAfkeuring", "Standnummer", "Voornaam" },
                values: new object[] { new Guid("bfebd9b2-7c28-446a-9483-a281550212ec"), null, 30, null, 1, "Inschrijving", new Guid("574fa379-3144-49c0-b1f1-5e1514e270af"), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "verzonnenmail12@gmail.com", new Guid("fcaa7d8a-4b3a-4282-962e-2e1bf90d3448"), "Gent", null, new Guid("febf6bbe-4d18-46b1-846b-eeec0581b482"), null, 1m, "418273645", null, "9000", "+32", null, "Te verwijderen door verwijderen evenementGroep.", null, "Tijdelijke" });

            migrationBuilder.InsertData(
                table: "Betaaltransacties",
                columns: new[] { "Id", "Bedrag", "BetaalmethodeId", "DatumBetaling", "GestructureerdeMededeling", "InschrijvingsId", "LidId", "VerantwoordelijkeBetaling" },
                values: new object[] { new Guid("a4f5e57b-4ec3-4b57-8ad7-561f406bde12"), 30m, new Guid("574fa379-3144-49c0-b1f1-5e1514e270af"), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("bfebd9b2-7c28-446a-9483-a281550212ec"), new Guid("3a041df5-32a4-4d86-add2-8f0c16a407aa"), "Te verwijderen door verwijderen EvenementGroep" });

            migrationBuilder.InsertData(
                table: "Betaaltransacties",
                columns: new[] { "Id", "Bedrag", "BetaalmethodeId", "DatumBetaling", "GestructureerdeMededeling", "InschrijvingsId", "LidId", "VerantwoordelijkeBetaling" },
                values: new object[] { new Guid("35009a2b-19df-484d-9cd7-e6f688c23b95"), 30m, new Guid("574fa379-3144-49c0-b1f1-5e1514e270af"), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("bfebd9b2-7c28-446a-9483-a281550212ec"), new Guid("3a041df5-32a4-4d86-add2-8f0c16a407aa"), "Te verwijderen door verwijderen EvenementGroep" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Betaaltransacties",
                keyColumn: "Id",
                keyValue: new Guid("35009a2b-19df-484d-9cd7-e6f688c23b95"));

            migrationBuilder.DeleteData(
                table: "Betaaltransacties",
                keyColumn: "Id",
                keyValue: new Guid("5cc00408-02a2-4a86-a88b-d6b3f0147a48"));

            migrationBuilder.DeleteData(
                table: "Betaaltransacties",
                keyColumn: "Id",
                keyValue: new Guid("68cb7db0-4af5-4469-9838-099ee2b1c8c0"));

            migrationBuilder.DeleteData(
                table: "Betaaltransacties",
                keyColumn: "Id",
                keyValue: new Guid("8c72a872-c977-4574-9abc-8c9e911dc4a5"));

            migrationBuilder.DeleteData(
                table: "Betaaltransacties",
                keyColumn: "Id",
                keyValue: new Guid("a4f5e57b-4ec3-4b57-8ad7-561f406bde12"));

            migrationBuilder.DeleteData(
                table: "Leden",
                keyColumn: "Id",
                keyValue: new Guid("ce467c89-7619-45aa-be44-ec214b54aca0"));

            migrationBuilder.DeleteData(
                table: "Inschrijvingen",
                keyColumn: "Id",
                keyValue: new Guid("bfebd9b2-7c28-446a-9483-a281550212ec"));

            migrationBuilder.DeleteData(
                table: "Inschrijvingen",
                keyColumn: "Id",
                keyValue: new Guid("c1dea00c-0513-4ebf-8c01-306c6533a270"));

            migrationBuilder.DeleteData(
                table: "Inschrijvingen",
                keyColumn: "Id",
                keyValue: new Guid("c7a1c924-711b-4c81-962d-b3a96ffd2e8a"));

            migrationBuilder.DeleteData(
                table: "Evenementen",
                keyColumn: "Id",
                keyValue: new Guid("254df418-2646-44e6-9ae6-78ebd29475f8"));

            migrationBuilder.DeleteData(
                table: "Evenementen",
                keyColumn: "Id",
                keyValue: new Guid("fcaa7d8a-4b3a-4282-962e-2e1bf90d3448"));

            migrationBuilder.DeleteData(
                table: "EvenementCategorieen",
                keyColumn: "Id",
                keyValue: new Guid("77d78a2a-4790-4394-9c31-05ae08108628"));

            migrationBuilder.UpdateData(
                table: "Betaalmethoden",
                keyColumn: "Id",
                keyValue: new Guid("608f19f1-05a2-4b56-96df-ad7af75d7893"),
                column: "Naam",
                value: "MasterCard");

            migrationBuilder.InsertData(
                table: "EvenementCategorieen",
                columns: new[] { "Id", "Naam" },
                values: new object[] { new Guid("6141b45b-e9e4-4684-ae4e-2dae1737de8f"), "Niet gebruikte categorie" });

            migrationBuilder.UpdateData(
                table: "Inschrijvingsstatussen",
                keyColumn: "Id",
                keyValue: new Guid("8e7c974e-a3d3-47e6-a19b-9e5a0e1abc3e"),
                column: "Naam",
                value: "Geannuleerd");
        }
    }
}
