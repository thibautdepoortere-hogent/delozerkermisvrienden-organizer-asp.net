using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeLozerkermisVrienden.Organizer.API.Migrations
{
    public partial class V5_AddTestData_Evenement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evenementen_EvenementCategorieen_EvenementCategorieId",
                table: "Evenementen");

            migrationBuilder.AlterColumn<Guid>(
                name: "EvenementCategorieId",
                table: "Evenementen",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "Evenementen",
                columns: new[] { "Id", "DatumEindeEvenement", "DatumEindeInschrijvingen", "DatumStartEvenement", "DatumStartInschrijvingen", "EvenementCategorieId", "Naam" },
                values: new object[,]
                {
                    { new Guid("c4660a63-7e82-4e68-92c9-85f3c193f69e"), new DateTime(2020, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f13a3c7e-ead7-42d7-9d09-f3e2c8e292d1"), "Rommelmarkt 2020" },
                    { new Guid("6bce3045-0e7e-4d42-992a-196361d1266b"), new DateTime(2020, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f13a3c7e-ead7-42d7-9d09-f3e2c8e292d1"), "Brunch 2020" },
                    { new Guid("08df868b-7173-4355-befb-7cb16b696444"), new DateTime(2020, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c971a053-e944-45ba-9307-229b07c74041"), "Ontbijtmanden vaderdag 2020" },
                    { new Guid("9f04dc8e-b95b-4f0a-81bb-10bacbeed553"), new DateTime(2020, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Bloemenfeest 2020" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Evenementen_EvenementCategorieen_EvenementCategorieId",
                table: "Evenementen",
                column: "EvenementCategorieId",
                principalTable: "EvenementCategorieen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evenementen_EvenementCategorieen_EvenementCategorieId",
                table: "Evenementen");

            migrationBuilder.DeleteData(
                table: "Evenementen",
                keyColumn: "Id",
                keyValue: new Guid("08df868b-7173-4355-befb-7cb16b696444"));

            migrationBuilder.DeleteData(
                table: "Evenementen",
                keyColumn: "Id",
                keyValue: new Guid("6bce3045-0e7e-4d42-992a-196361d1266b"));

            migrationBuilder.DeleteData(
                table: "Evenementen",
                keyColumn: "Id",
                keyValue: new Guid("9f04dc8e-b95b-4f0a-81bb-10bacbeed553"));

            migrationBuilder.DeleteData(
                table: "Evenementen",
                keyColumn: "Id",
                keyValue: new Guid("c4660a63-7e82-4e68-92c9-85f3c193f69e"));

            migrationBuilder.AlterColumn<Guid>(
                name: "EvenementCategorieId",
                table: "Evenementen",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Evenementen_EvenementCategorieen_EvenementCategorieId",
                table: "Evenementen",
                column: "EvenementCategorieId",
                principalTable: "EvenementCategorieen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
