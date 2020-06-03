﻿// <auto-generated />
using System;
using DeLozerkermisVrienden.Organizer.API.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DeLozerkermisVrienden.Organizer.API.Migrations
{
    [DbContext(typeof(OrganizerContext))]
    [Migration("20200413194143_V6_AddTestData_Lid")]
    partial class V6_AddTestData_Lid
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DeLozerkermisVrienden.Organizer.API.Entities.Betaalmethode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("AantalDagenVroegerAfsluiten")
                        .HasColumnType("int");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("Opmerking")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<int>("Volgorde")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Betaalmethoden");

                    b.HasData(
                        new
                        {
                            Id = new Guid("574fa379-3144-49c0-b1f1-5e1514e270af"),
                            Naam = "Contant",
                            Opmerking = "U betaalt bij het inchecken op de dag van het evenement zelf OF U betaalt ervoor aan een lid van de vereniging.",
                            Volgorde = 1
                        },
                        new
                        {
                            Id = new Guid("a833e881-dbdb-4a09-a718-a44d1f3ce0ea"),
                            AantalDagenVroegerAfsluiten = 5,
                            Naam = "Overschrijving",
                            Volgorde = 2
                        });
                });

            modelBuilder.Entity("DeLozerkermisVrienden.Organizer.API.Entities.Betaaltransactie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Bedrag")
                        .HasColumnType("decimal(7,2)");

                    b.Property<Guid?>("BetaalmethodeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DatumBetaling")
                        .HasColumnType("datetime2");

                    b.Property<string>("GestructureerdeMededeling")
                        .HasColumnType("nvarchar(12)")
                        .HasMaxLength(12);

                    b.Property<Guid>("InschrijvingsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("LidId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VerantwoordelijkeBetaling")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.HasIndex("BetaalmethodeId");

                    b.HasIndex("InschrijvingsId");

                    b.HasIndex("LidId");

                    b.ToTable("Betaaltransacties");
                });

            modelBuilder.Entity("DeLozerkermisVrienden.Organizer.API.Entities.CheckIn", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("InschrijvingsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("LidId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("InschrijvingsId");

                    b.HasIndex("LidId");

                    b.ToTable("CheckIns");
                });

            modelBuilder.Entity("DeLozerkermisVrienden.Organizer.API.Entities.Evenement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DatumEindeEvenement")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatumEindeInschrijvingen")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatumStartEvenement")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatumStartInschrijvingen")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("EvenementCategorieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.HasIndex("EvenementCategorieId");

                    b.ToTable("Evenementen");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c4660a63-7e82-4e68-92c9-85f3c193f69e"),
                            DatumEindeEvenement = new DateTime(2020, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DatumEindeInschrijvingen = new DateTime(2020, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DatumStartEvenement = new DateTime(2020, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DatumStartInschrijvingen = new DateTime(2020, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EvenementCategorieId = new Guid("f13a3c7e-ead7-42d7-9d09-f3e2c8e292d1"),
                            Naam = "Rommelmarkt 2020"
                        },
                        new
                        {
                            Id = new Guid("6bce3045-0e7e-4d42-992a-196361d1266b"),
                            DatumEindeEvenement = new DateTime(2020, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DatumEindeInschrijvingen = new DateTime(2020, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DatumStartEvenement = new DateTime(2020, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DatumStartInschrijvingen = new DateTime(2020, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EvenementCategorieId = new Guid("f13a3c7e-ead7-42d7-9d09-f3e2c8e292d1"),
                            Naam = "Brunch 2020"
                        },
                        new
                        {
                            Id = new Guid("08df868b-7173-4355-befb-7cb16b696444"),
                            DatumEindeEvenement = new DateTime(2020, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DatumEindeInschrijvingen = new DateTime(2020, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DatumStartEvenement = new DateTime(2020, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DatumStartInschrijvingen = new DateTime(2020, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EvenementCategorieId = new Guid("c971a053-e944-45ba-9307-229b07c74041"),
                            Naam = "Ontbijtmanden vaderdag 2020"
                        },
                        new
                        {
                            Id = new Guid("9f04dc8e-b95b-4f0a-81bb-10bacbeed553"),
                            DatumEindeEvenement = new DateTime(2020, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DatumEindeInschrijvingen = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DatumStartEvenement = new DateTime(2020, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DatumStartInschrijvingen = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Naam = "Bloemenfeest 2020"
                        });
                });

            modelBuilder.Entity("DeLozerkermisVrienden.Organizer.API.Entities.EvenementCategorie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("EvenementCategorieen");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f13a3c7e-ead7-42d7-9d09-f3e2c8e292d1"),
                            Naam = "Lozerkermis"
                        },
                        new
                        {
                            Id = new Guid("c971a053-e944-45ba-9307-229b07c74041"),
                            Naam = "Vader-Moederdag"
                        },
                        new
                        {
                            Id = new Guid("6141b45b-e9e4-4684-ae4e-2dae1737de8f"),
                            Naam = "Niet gebruikte categorie"
                        });
                });

            modelBuilder.Entity("DeLozerkermisVrienden.Organizer.API.Entities.Inschrijving", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("AantalAanhangwagens")
                        .HasColumnType("int");

                    b.Property<int>("AantalMeter")
                        .HasColumnType("int");

                    b.Property<int?>("AantalMobilhomes")
                        .HasColumnType("int");

                    b.Property<int?>("AantalWagens")
                        .HasColumnType("int");

                    b.Property<string>("Achternaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<Guid?>("BetaalmethodeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DatumInschrijving")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<Guid>("EvenementId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Gemeente")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("GestructureerdeMededeling")
                        .HasColumnType("nvarchar(12)")
                        .HasMaxLength(12);

                    b.Property<Guid?>("InschrijvingsstatusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("LidId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Meterprijs")
                        .HasColumnType("decimal(5,2)");

                    b.Property<string>("MobielNummer")
                        .IsRequired()
                        .HasColumnType("nvarchar(9)")
                        .HasMaxLength(9);

                    b.Property<string>("Opmerking")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

                    b.Property<string>("PrefixMobielNummer")
                        .IsRequired()
                        .HasColumnType("nvarchar(6)")
                        .HasMaxLength(6);

                    b.Property<string>("QRCode")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("RedenAfkeuring")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Standnummer")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Voornaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.HasIndex("BetaalmethodeId");

                    b.HasIndex("EvenementId");

                    b.HasIndex("InschrijvingsstatusId");

                    b.HasIndex("LidId");

                    b.ToTable("Inschrijvingen");
                });

            modelBuilder.Entity("DeLozerkermisVrienden.Organizer.API.Entities.Inschrijvingsstatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<int>("Volgorde")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Inschrijvingsstatussen");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4c83bb5b-30b2-4ac7-9662-eab035157b86"),
                            Naam = "Aangevraagd",
                            Volgorde = 1
                        },
                        new
                        {
                            Id = new Guid("4c2c40d0-c1e9-490b-afb2-5bd9607b7869"),
                            Naam = "Goedgekeurd",
                            Volgorde = 2
                        },
                        new
                        {
                            Id = new Guid("adb494b6-10ae-495a-9ba4-48ef04d0e29f"),
                            Naam = "Gepland",
                            Volgorde = 3
                        },
                        new
                        {
                            Id = new Guid("febf6bbe-4d18-46b1-846b-eeec0581b482"),
                            Naam = "Afgekeurd",
                            Volgorde = 4
                        },
                        new
                        {
                            Id = new Guid("8e7c974e-a3d3-47e6-a19b-9e5a0e1abc3e"),
                            Naam = "Geannuleerd",
                            Volgorde = 5
                        });
                });

            modelBuilder.Entity("DeLozerkermisVrienden.Organizer.API.Entities.Lid", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Achternaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<bool>("Actief")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Voornaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("Leden");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8ed92433-e0ca-42d5-b80c-89415991f1f2"),
                            Achternaam = "De Poortere",
                            Actief = true,
                            Email = "thibaut.depoortere@student.hogent.be",
                            Voornaam = "Thibaut"
                        },
                        new
                        {
                            Id = new Guid("3a041df5-32a4-4d86-add2-8f0c16a407aa"),
                            Achternaam = "De Poortere",
                            Actief = true,
                            Email = "thibaut.depoortere@student.hogent.be",
                            Voornaam = "Filip"
                        },
                        new
                        {
                            Id = new Guid("b7096da3-4070-43e1-99c9-ebf4a1829236"),
                            Achternaam = "Van Cauwenberghe",
                            Actief = false,
                            Email = "thibaut.depoortere@student.hogent.be",
                            Voornaam = "Jo"
                        });
                });

            modelBuilder.Entity("DeLozerkermisVrienden.Organizer.API.Entities.Login", b =>
                {
                    b.Property<Guid>("LidId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Wachtwoord")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LidId");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("DeLozerkermisVrienden.Organizer.API.Entities.Nieuwsbrief", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<Guid>("EvenementId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EvenementId");

                    b.ToTable("Nieuwsbrieven");
                });

            modelBuilder.Entity("DeLozerkermisVrienden.Organizer.API.Entities.Betaaltransactie", b =>
                {
                    b.HasOne("DeLozerkermisVrienden.Organizer.API.Entities.Betaalmethode", "Betaalmethode")
                        .WithMany("Betaaltransacties")
                        .HasForeignKey("BetaalmethodeId");

                    b.HasOne("DeLozerkermisVrienden.Organizer.API.Entities.Inschrijving", "Inschrijving")
                        .WithMany("Betaaltransacties")
                        .HasForeignKey("InschrijvingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DeLozerkermisVrienden.Organizer.API.Entities.Lid", "Lid")
                        .WithMany("Betaaltransacties")
                        .HasForeignKey("LidId");
                });

            modelBuilder.Entity("DeLozerkermisVrienden.Organizer.API.Entities.CheckIn", b =>
                {
                    b.HasOne("DeLozerkermisVrienden.Organizer.API.Entities.Inschrijving", "Inschrijving")
                        .WithMany("CheckIns")
                        .HasForeignKey("InschrijvingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DeLozerkermisVrienden.Organizer.API.Entities.Lid", "Lid")
                        .WithMany("CheckIns")
                        .HasForeignKey("LidId");
                });

            modelBuilder.Entity("DeLozerkermisVrienden.Organizer.API.Entities.Evenement", b =>
                {
                    b.HasOne("DeLozerkermisVrienden.Organizer.API.Entities.EvenementCategorie", null)
                        .WithMany("Evenementen")
                        .HasForeignKey("EvenementCategorieId");
                });

            modelBuilder.Entity("DeLozerkermisVrienden.Organizer.API.Entities.Inschrijving", b =>
                {
                    b.HasOne("DeLozerkermisVrienden.Organizer.API.Entities.Betaalmethode", "Betaalmethode")
                        .WithMany("Inschrijvingen")
                        .HasForeignKey("BetaalmethodeId");

                    b.HasOne("DeLozerkermisVrienden.Organizer.API.Entities.Evenement", "Evenement")
                        .WithMany("Inschrijvingen")
                        .HasForeignKey("EvenementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DeLozerkermisVrienden.Organizer.API.Entities.Inschrijvingsstatus", "Inschrijvingsstatus")
                        .WithMany("Inschrijvingen")
                        .HasForeignKey("InschrijvingsstatusId");

                    b.HasOne("DeLozerkermisVrienden.Organizer.API.Entities.Lid", "Lid")
                        .WithMany("Inschrijvingen")
                        .HasForeignKey("LidId");
                });

            modelBuilder.Entity("DeLozerkermisVrienden.Organizer.API.Entities.Login", b =>
                {
                    b.HasOne("DeLozerkermisVrienden.Organizer.API.Entities.Lid", "Lid")
                        .WithOne("Login")
                        .HasForeignKey("DeLozerkermisVrienden.Organizer.API.Entities.Login", "LidId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DeLozerkermisVrienden.Organizer.API.Entities.Nieuwsbrief", b =>
                {
                    b.HasOne("DeLozerkermisVrienden.Organizer.API.Entities.Evenement", "Evenement")
                        .WithMany("Nieuwsbrieven")
                        .HasForeignKey("EvenementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
