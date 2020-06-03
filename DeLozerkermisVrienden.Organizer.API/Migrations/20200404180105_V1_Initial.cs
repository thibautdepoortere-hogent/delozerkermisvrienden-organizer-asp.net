using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeLozerkermisVrienden.Organizer.API.Migrations
{
    public partial class V1_Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Betaalmethoden",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Naam = table.Column<string>(maxLength: 150, nullable: false),
                    AantalDagenVroegerAfsluiten = table.Column<int>(nullable: true),
                    Opmerking = table.Column<string>(maxLength: 150, nullable: true),
                    Volgorde = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Betaalmethoden", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvenementCategorieen",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Naam = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvenementCategorieen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inschrijvingsstatussen",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Naam = table.Column<string>(maxLength: 150, nullable: false),
                    Volgorde = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inschrijvingsstatussen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Leden",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Voornaam = table.Column<string>(maxLength: 150, nullable: false),
                    Achternaam = table.Column<string>(maxLength: 150, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    Actief = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leden", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Evenementen",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Naam = table.Column<string>(maxLength: 150, nullable: false),
                    DatumStartEvenement = table.Column<DateTime>(nullable: false),
                    DatumEindeEvenement = table.Column<DateTime>(nullable: false),
                    DatumStartInschrijvingen = table.Column<DateTime>(nullable: false),
                    DatumEindeInschrijvingen = table.Column<DateTime>(nullable: false),
                    EvenementCategorieId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evenementen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evenementen_EvenementCategorieen_EvenementCategorieId",
                        column: x => x.EvenementCategorieId,
                        principalTable: "EvenementCategorieen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    LidId = table.Column<Guid>(nullable: false),
                    Wachtwoord = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.LidId);
                    table.ForeignKey(
                        name: "FK_Logins_Leden_LidId",
                        column: x => x.LidId,
                        principalTable: "Leden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inschrijvingen",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DatumInschrijving = table.Column<DateTime>(nullable: false),
                    Voornaam = table.Column<string>(maxLength: 150, nullable: false),
                    Achternaam = table.Column<string>(maxLength: 150, nullable: false),
                    Postcode = table.Column<string>(maxLength: 4, nullable: false),
                    Gemeente = table.Column<string>(maxLength: 100, nullable: false),
                    PrefixMobielNummer = table.Column<string>(maxLength: 6, nullable: false),
                    MobielNummer = table.Column<string>(maxLength: 9, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    AantalMeter = table.Column<int>(nullable: false),
                    Meterprijs = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    AantalWagens = table.Column<int>(nullable: true),
                    AantalAanhangwagens = table.Column<int>(nullable: true),
                    AantalMobilhomes = table.Column<int>(nullable: true),
                    Opmerking = table.Column<string>(maxLength: 200, nullable: true),
                    Standnummer = table.Column<string>(maxLength: 10, nullable: true),
                    GestructureerdeMededeling = table.Column<string>(maxLength: 12, nullable: true),
                    QRCode = table.Column<string>(maxLength: 50, nullable: true),
                    RedenAfkeuring = table.Column<string>(maxLength: 200, nullable: true),
                    InschrijvingsstatusId = table.Column<Guid>(nullable: true),
                    BetaalmethodeId = table.Column<Guid>(nullable: true),
                    EvenementId = table.Column<Guid>(nullable: false),
                    LidId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inschrijvingen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inschrijvingen_Betaalmethoden_BetaalmethodeId",
                        column: x => x.BetaalmethodeId,
                        principalTable: "Betaalmethoden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inschrijvingen_Evenementen_EvenementId",
                        column: x => x.EvenementId,
                        principalTable: "Evenementen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inschrijvingen_Inschrijvingsstatussen_InschrijvingsstatusId",
                        column: x => x.InschrijvingsstatusId,
                        principalTable: "Inschrijvingsstatussen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inschrijvingen_Leden_LidId",
                        column: x => x.LidId,
                        principalTable: "Leden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Nieuwsbrieven",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    EvenementId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nieuwsbrieven", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nieuwsbrieven_Evenementen_EvenementId",
                        column: x => x.EvenementId,
                        principalTable: "Evenementen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Betaaltransacties",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Bedrag = table.Column<decimal>(type: "decimal(7,2)", nullable: false),
                    DatumBetaling = table.Column<DateTime>(nullable: false),
                    VerantwoordelijkeBetaling = table.Column<string>(maxLength: 250, nullable: true),
                    GestructureerdeMededeling = table.Column<string>(maxLength: 12, nullable: true),
                    InschrijvingsId = table.Column<Guid>(nullable: false),
                    BetaalmethodeId = table.Column<Guid>(nullable: true),
                    LidId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Betaaltransacties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Betaaltransacties_Betaalmethoden_BetaalmethodeId",
                        column: x => x.BetaalmethodeId,
                        principalTable: "Betaalmethoden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Betaaltransacties_Inschrijvingen_InschrijvingsId",
                        column: x => x.InschrijvingsId,
                        principalTable: "Inschrijvingen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Betaaltransacties_Leden_LidId",
                        column: x => x.LidId,
                        principalTable: "Leden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CheckIns",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InschrijvingsId = table.Column<Guid>(nullable: false),
                    LidId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckIns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckIns_Inschrijvingen_InschrijvingsId",
                        column: x => x.InschrijvingsId,
                        principalTable: "Inschrijvingen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckIns_Leden_LidId",
                        column: x => x.LidId,
                        principalTable: "Leden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Betaaltransacties_BetaalmethodeId",
                table: "Betaaltransacties",
                column: "BetaalmethodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Betaaltransacties_InschrijvingsId",
                table: "Betaaltransacties",
                column: "InschrijvingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Betaaltransacties_LidId",
                table: "Betaaltransacties",
                column: "LidId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckIns_InschrijvingsId",
                table: "CheckIns",
                column: "InschrijvingsId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckIns_LidId",
                table: "CheckIns",
                column: "LidId");

            migrationBuilder.CreateIndex(
                name: "IX_Evenementen_EvenementCategorieId",
                table: "Evenementen",
                column: "EvenementCategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Inschrijvingen_BetaalmethodeId",
                table: "Inschrijvingen",
                column: "BetaalmethodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Inschrijvingen_EvenementId",
                table: "Inschrijvingen",
                column: "EvenementId");

            migrationBuilder.CreateIndex(
                name: "IX_Inschrijvingen_InschrijvingsstatusId",
                table: "Inschrijvingen",
                column: "InschrijvingsstatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Inschrijvingen_LidId",
                table: "Inschrijvingen",
                column: "LidId");

            migrationBuilder.CreateIndex(
                name: "IX_Nieuwsbrieven_EvenementId",
                table: "Nieuwsbrieven",
                column: "EvenementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Betaaltransacties");

            migrationBuilder.DropTable(
                name: "CheckIns");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "Nieuwsbrieven");

            migrationBuilder.DropTable(
                name: "Inschrijvingen");

            migrationBuilder.DropTable(
                name: "Betaalmethoden");

            migrationBuilder.DropTable(
                name: "Evenementen");

            migrationBuilder.DropTable(
                name: "Inschrijvingsstatussen");

            migrationBuilder.DropTable(
                name: "Leden");

            migrationBuilder.DropTable(
                name: "EvenementCategorieen");
        }
    }
}
