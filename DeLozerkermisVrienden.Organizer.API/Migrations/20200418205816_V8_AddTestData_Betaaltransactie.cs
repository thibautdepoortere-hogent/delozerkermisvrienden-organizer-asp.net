using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeLozerkermisVrienden.Organizer.API.Migrations
{
    public partial class V8_AddTestData_Betaaltransactie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Betaaltransacties_Inschrijvingen_InschrijvingsId",
                table: "Betaaltransacties");

            migrationBuilder.AlterColumn<Guid>(
                name: "InschrijvingsId",
                table: "Betaaltransacties",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "Betaaltransacties",
                columns: new[] { "Id", "Bedrag", "BetaalmethodeId", "DatumBetaling", "GestructureerdeMededeling", "InschrijvingsId", "LidId", "VerantwoordelijkeBetaling" },
                values: new object[,]
                {
                    { new Guid("e2298ab9-66b2-4bbe-afd3-1406c42c6825"), 8m, new Guid("a833e881-dbdb-4a09-a718-a44d1f3ce0ea"), new DateTime(2020, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "123123412345", new Guid("a17c29b6-4cc2-4cc8-9a84-592ae2bafbc7"), new Guid("8ed92433-e0ca-42d5-b80c-89415991f1f2"), "Thibaut De Poortere" },
                    { new Guid("06b1a479-ad0d-415b-b95e-a594c930879e"), 8m, new Guid("a833e881-dbdb-4a09-a718-a44d1f3ce0ea"), new DateTime(2020, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "123123412345", new Guid("a17c29b6-4cc2-4cc8-9a84-592ae2bafbc7"), new Guid("8ed92433-e0ca-42d5-b80c-89415991f1f2"), "Charlotte De Poortere" },
                    { new Guid("84ec6183-4f95-4e8b-bea3-81704dc24bb7"), 10m, new Guid("574fa379-3144-49c0-b1f1-5e1514e270af"), new DateTime(2020, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("c565df1d-c604-4ea5-b2da-aedad6e924bf"), new Guid("3a041df5-32a4-4d86-add2-8f0c16a407aa"), "Carine De Poortere" },
                    { new Guid("601c39e9-ee24-4a23-8a05-036705b0cf91"), 2m, new Guid("574fa379-3144-49c0-b1f1-5e1514e270af"), new DateTime(2020, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("945fa969-22f7-4bf3-8054-eae61f41ae6a"), new Guid("3a041df5-32a4-4d86-add2-8f0c16a407aa"), "Marc De Kimpe" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Betaaltransacties_Inschrijvingen_InschrijvingsId",
                table: "Betaaltransacties",
                column: "InschrijvingsId",
                principalTable: "Inschrijvingen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Betaaltransacties_Inschrijvingen_InschrijvingsId",
                table: "Betaaltransacties");

            migrationBuilder.DeleteData(
                table: "Betaaltransacties",
                keyColumn: "Id",
                keyValue: new Guid("06b1a479-ad0d-415b-b95e-a594c930879e"));

            migrationBuilder.DeleteData(
                table: "Betaaltransacties",
                keyColumn: "Id",
                keyValue: new Guid("601c39e9-ee24-4a23-8a05-036705b0cf91"));

            migrationBuilder.DeleteData(
                table: "Betaaltransacties",
                keyColumn: "Id",
                keyValue: new Guid("84ec6183-4f95-4e8b-bea3-81704dc24bb7"));

            migrationBuilder.DeleteData(
                table: "Betaaltransacties",
                keyColumn: "Id",
                keyValue: new Guid("e2298ab9-66b2-4bbe-afd3-1406c42c6825"));

            migrationBuilder.AlterColumn<Guid>(
                name: "InschrijvingsId",
                table: "Betaaltransacties",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Betaaltransacties_Inschrijvingen_InschrijvingsId",
                table: "Betaaltransacties",
                column: "InschrijvingsId",
                principalTable: "Inschrijvingen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
