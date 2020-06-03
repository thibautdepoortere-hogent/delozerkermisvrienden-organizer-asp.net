using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeLozerkermisVrienden.Organizer.API.Migrations
{
    public partial class V9_AddTestData_CheckIn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_Inschrijvingen_InschrijvingsId",
                table: "CheckIns");

            migrationBuilder.AlterColumn<Guid>(
                name: "InschrijvingsId",
                table: "CheckIns",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckInMoment",
                table: "CheckIns",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "CheckIns",
                columns: new[] { "Id", "CheckInMoment", "InschrijvingsId", "LidId" },
                values: new object[,]
                {
                    { new Guid("195925fe-c828-4572-a039-4d0f2bea8a58"), new DateTime(2020, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a17c29b6-4cc2-4cc8-9a84-592ae2bafbc7"), new Guid("3a041df5-32a4-4d86-add2-8f0c16a407aa") },
                    { new Guid("7a439907-d6a5-477b-93d9-e34573f2c8d4"), new DateTime(2020, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c7a1c924-711b-4c81-962d-b3a96ffd2e8a"), null },
                    { new Guid("18ccc083-02f9-4487-9935-18ae25d12100"), new DateTime(2020, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c7a1c924-711b-4c81-962d-b3a96ffd2e8a"), null },
                    { new Guid("cadad060-3162-45cf-9076-fc7dfbfc6ddd"), new DateTime(2020, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c1dea00c-0513-4ebf-8c01-306c6533a270"), new Guid("3a041df5-32a4-4d86-add2-8f0c16a407aa") },
                    { new Guid("b950c3d8-9ba5-4588-8781-a2e031389616"), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bfebd9b2-7c28-446a-9483-a281550212ec"), null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_Inschrijvingen_InschrijvingsId",
                table: "CheckIns",
                column: "InschrijvingsId",
                principalTable: "Inschrijvingen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_Inschrijvingen_InschrijvingsId",
                table: "CheckIns");

            migrationBuilder.DeleteData(
                table: "CheckIns",
                keyColumn: "Id",
                keyValue: new Guid("18ccc083-02f9-4487-9935-18ae25d12100"));

            migrationBuilder.DeleteData(
                table: "CheckIns",
                keyColumn: "Id",
                keyValue: new Guid("195925fe-c828-4572-a039-4d0f2bea8a58"));

            migrationBuilder.DeleteData(
                table: "CheckIns",
                keyColumn: "Id",
                keyValue: new Guid("7a439907-d6a5-477b-93d9-e34573f2c8d4"));

            migrationBuilder.DeleteData(
                table: "CheckIns",
                keyColumn: "Id",
                keyValue: new Guid("b950c3d8-9ba5-4588-8781-a2e031389616"));

            migrationBuilder.DeleteData(
                table: "CheckIns",
                keyColumn: "Id",
                keyValue: new Guid("cadad060-3162-45cf-9076-fc7dfbfc6ddd"));

            migrationBuilder.DropColumn(
                name: "CheckInMoment",
                table: "CheckIns");

            migrationBuilder.AlterColumn<Guid>(
                name: "InschrijvingsId",
                table: "CheckIns",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_Inschrijvingen_InschrijvingsId",
                table: "CheckIns",
                column: "InschrijvingsId",
                principalTable: "Inschrijvingen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
