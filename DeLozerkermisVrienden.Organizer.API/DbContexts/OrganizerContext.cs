using DeLozerkermisVrienden.Organizer.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.DbContexts
{
    public class OrganizerContext : DbContext
    {
        public OrganizerContext(DbContextOptions<OrganizerContext> options) : base(options)
        {
        }

        public DbSet<Inschrijving> Inschrijvingen { get; set; }
        public DbSet<Inschrijvingsstatus> Inschrijvingsstatussen { get; set; }
        public DbSet<Betaalmethode> Betaalmethoden { get; set; }
        public DbSet<Betaaltransactie> Betaaltransacties { get; set; }
        public DbSet<EvenementCategorie> EvenementCategorieen { get; set; }
        public DbSet<Evenement> Evenementen { get; set; }
        public DbSet<Lid> Leden { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Nieuwsbrief> Nieuwsbrieven { get; set; }
        public DbSet<CheckIn> CheckIns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Betaalmethode>().HasData(
            //    new Betaalmethode()
            //    {
            //        Id = Guid.Parse("574fa379-3144-49c0-b1f1-5e1514e270af"),
            //        Naam = "Contant",
            //        Opmerking = "U betaalt bij het inchecken op de dag van het evenement zelf OF U betaalt ervoor aan een lid van de vereniging.",
            //        Volgorde = 1
            //    },
            //    new Betaalmethode()
            //    {
            //        Id = Guid.Parse("a833e881-dbdb-4a09-a718-a44d1f3ce0ea"),
            //        Naam = "Overschrijving",
            //        AantalDagenVroegerAfsluiten = 5,
            //        Volgorde = 2
            //    },
            //    new Betaalmethode()
            //    {
            //        Id = Guid.Parse("608f19f1-05a2-4b56-96df-ad7af75d7893"),
            //        Naam = "Te verwijderen",
            //        Volgorde = 10
            //    }
            //);

            //modelBuilder.Entity<Inschrijvingsstatus>().HasData(
            //    new Inschrijvingsstatus()
            //    {
            //        Id = Guid.Parse("4c83bb5b-30b2-4ac7-9662-eab035157b86"),
            //        Naam = "Aangevraagd",
            //        Volgorde = 1
            //    },
            //    new Inschrijvingsstatus()
            //    {
            //        Id = Guid.Parse("4c2c40d0-c1e9-490b-afb2-5bd9607b7869"),
            //        Naam = "Goedgekeurd",
            //        Volgorde = 2
            //    },
            //    new Inschrijvingsstatus()
            //    {
            //        Id = Guid.Parse("adb494b6-10ae-495a-9ba4-48ef04d0e29f"),
            //        Naam = "Gepland",
            //        Volgorde = 3
            //    },
            //    new Inschrijvingsstatus()
            //    {
            //        Id = Guid.Parse("febf6bbe-4d18-46b1-846b-eeec0581b482"),
            //        Naam = "Afgekeurd",
            //        Volgorde = 4
            //    },
            //    new Inschrijvingsstatus()
            //    {
            //        Id = Guid.Parse("8e7c974e-a3d3-47e6-a19b-9e5a0e1abc3e"),
            //        Naam = "te verwijderen",
            //        Volgorde = 5
            //    }
            //);

            //modelBuilder.Entity<EvenementCategorie>().HasData(
            //    new EvenementCategorie()
            //    {
            //        Id = Guid.Parse("f13a3c7e-ead7-42d7-9d09-f3e2c8e292d1"),
            //        Naam = "Lozerkermis"
            //    },
            //    new EvenementCategorie()
            //    {
            //        Id = Guid.Parse("c971a053-e944-45ba-9307-229b07c74041"),
            //        Naam = "Vader-Moederdag"
            //    },
            //    new EvenementCategorie()
            //    {
            //        Id = Guid.Parse("77d78a2a-4790-4394-9c31-05ae08108628"),
            //        Naam = "Te verwijderen"
            //    }
            //);

            //modelBuilder.Entity<Evenement>().HasData(
            //    new Evenement()
            //    {
            //        Id = Guid.Parse("c4660a63-7e82-4e68-92c9-85f3c193f69e"),
            //        Naam = "Rommelmarkt 2020",
            //        DatumStartEvenement = new DateTime(2020,9,26),
            //        DatumEindeEvenement = new DateTime(2020,9,26),
            //        DatumStartInschrijvingen = new DateTime(2020,6,26),
            //        DatumEindeInschrijvingen = new DateTime(2020,9,23),
            //        EvenementCategorieId = Guid.Parse("f13a3c7e-ead7-42d7-9d09-f3e2c8e292d1")   // Lozerkermis
            //    },
            //    new Evenement()
            //    {
            //        Id = Guid.Parse("6bce3045-0e7e-4d42-992a-196361d1266b"),
            //        Naam = "Brunch 2020",
            //        DatumStartEvenement = new DateTime(2020, 9, 27),
            //        DatumEindeEvenement = new DateTime(2020, 9, 27),
            //        DatumStartInschrijvingen = new DateTime(2020, 6, 26),
            //        DatumEindeInschrijvingen = new DateTime(2020, 9, 23),
            //        EvenementCategorieId = Guid.Parse("f13a3c7e-ead7-42d7-9d09-f3e2c8e292d1")   // Lozerkermis
            //    },
            //    new Evenement()
            //    {
            //        Id = Guid.Parse("08df868b-7173-4355-befb-7cb16b696444"),
            //        Naam = "Ontbijtmanden vaderdag 2020",
            //        DatumStartEvenement = new DateTime(2020, 6, 14),
            //        DatumEindeEvenement = new DateTime(2020, 6, 14),
            //        DatumStartInschrijvingen = new DateTime(2020, 5, 14),
            //        DatumEindeInschrijvingen = new DateTime(2020, 6, 12),
            //        EvenementCategorieId = Guid.Parse("c971a053-e944-45ba-9307-229b07c74041")   // Vader-Moederdag
            //    },
            //    new Evenement()
            //    {
            //        Id = Guid.Parse("9f04dc8e-b95b-4f0a-81bb-10bacbeed553"),
            //        Naam = "Bloemenfeest 2020",
            //        DatumStartEvenement = new DateTime(2020, 3, 15),
            //        DatumEindeEvenement = new DateTime(2020, 3, 15)
            //    },
            //    new Evenement()
            //    {
            //        Id = Guid.Parse("254df418-2646-44e6-9ae6-78ebd29475f8"),
            //        Naam = "Te verwijderen",
            //        DatumStartEvenement = new DateTime(2020, 1, 1),
            //        DatumEindeEvenement = new DateTime(2020, 1, 1)
            //    },
            //    new Evenement()
            //    {
            //        Id = Guid.Parse("fcaa7d8a-4b3a-4282-962e-2e1bf90d3448"),
            //        Naam = "Te verwijderen door verwijderen evenementGroep",
            //        DatumStartEvenement = new DateTime(2020, 1, 1),
            //        DatumEindeEvenement = new DateTime(2020, 1, 1),
            //        EvenementCategorieId = Guid.Parse("77d78a2a-4790-4394-9c31-05ae08108628")
            //    }
            //);

            //modelBuilder.Entity<Lid>().HasData(
            //    new Lid()
            //    {
            //        Id = Guid.Parse("8ed92433-e0ca-42d5-b80c-89415991f1f2"),
            //        Voornaam = "Thibaut",
            //        Achternaam = "De Poortere",
            //        Email = "thibaut.depoortere@student.hogent.be",
            //        Actief = true
            //    },
            //    new Lid()
            //    {
            //        Id = Guid.Parse("3a041df5-32a4-4d86-add2-8f0c16a407aa"),
            //        Voornaam = "Filip",
            //        Achternaam = "De Poortere",
            //        Email = "verzonnenmail01@gmail.com",
            //        Actief = true
            //    }, new Lid()
            //    {
            //        Id = Guid.Parse("b7096da3-4070-43e1-99c9-ebf4a1829236"),
            //        Voornaam = "Jo",
            //        Achternaam = "Van Cauwenberghe",
            //        Email = "verzonnenmail02@outlook.com"
            //    }, new Lid()
            //    {
            //        Id = Guid.Parse("ce467c89-7619-45aa-be44-ec214b54aca0"),
            //        Voornaam = "Tijdelijk",
            //        Achternaam = "Te verwijderen",
            //        Email = "verzonnenmail20@outlook.com"
            //    }
            //);

            //modelBuilder.Entity<Inschrijving>().HasData(
            //    new Inschrijving()
            //    {
            //        Id = Guid.Parse("a17c29b6-4cc2-4cc8-9a84-592ae2bafbc7"),
            //        DatumInschrijving = new DateTime(2020, 7, 15),
            //        Voornaam = "Thibaut",
            //        Achternaam = "De Poortere",
            //        Postcode = "9770",
            //        Gemeente = "Kruisem",
            //        PrefixMobielNummer = "+32",
            //        MobielNummer = "412345678",
            //        Email = "thibaut.depoortere@student.hogent.be",
            //        AantalMeter = 16,
            //        Meterprijs = 1m,
            //        AantalWagens = 2,
            //        Opmerking = "We staan met 2 op deze stand.",
            //        GestructureerdeMededeling = "123123412345",
            //        QRCode = "a17c29b64cc24cc89a84592ae2bafbc7",
            //        InschrijvingsstatusId = Guid.Parse("4c2c40d0-c1e9-490b-afb2-5bd9607b7869"),     // Goedgekeurd
            //        BetaalmethodeId = Guid.Parse("a833e881-dbdb-4a09-a718-a44d1f3ce0ea"),           // Overschrijving
            //        EvenementId = Guid.Parse("c4660a63-7e82-4e68-92c9-85f3c193f69e"),               // Rommelmarkt
            //        LidId = Guid.Parse("8ed92433-e0ca-42d5-b80c-89415991f1f2")                      // Thibaut De Poortere
            //    },
            //    new Inschrijving()
            //    {
            //        Id = Guid.Parse("c565df1d-c604-4ea5-b2da-aedad6e924bf"),
            //        DatumInschrijving = new DateTime(2020, 5, 5),
            //        Voornaam = "Carine",
            //        Achternaam = "De Poortere",
            //        Postcode = "9770",
            //        Gemeente = "Kruisem",
            //        PrefixMobielNummer = "+32",
            //        MobielNummer = "487654321",
            //        Email = "verzonnenmail03@icloud.com",
            //        AantalMeter = 5,
            //        Meterprijs = 1m,
            //        AantalWagens = 1,
            //        Opmerking = "Ik zal bloemstukken en tuindecoratie verkopen.",
            //        QRCode = "c565df1dc6044ea5b2daaedad6e924bf",
            //        InschrijvingsstatusId = Guid.Parse("4c2c40d0-c1e9-490b-afb2-5bd9607b7869"),     // Goedgekeurd
            //        BetaalmethodeId = Guid.Parse("574fa379-3144-49c0-b1f1-5e1514e270af"),           // Contant
            //        EvenementId = Guid.Parse("c4660a63-7e82-4e68-92c9-85f3c193f69e")                // Rommelmarkt
            //    },
            //    new Inschrijving()
            //    {
            //        Id = Guid.Parse("9b715468-6ac7-40c3-a30c-56d0a797cf45"),
            //        DatumInschrijving = new DateTime(2020, 8, 20),
            //        Voornaam = "Kerensa",
            //        Achternaam = "Baert",
            //        Postcode = "9700",
            //        Gemeente = "Nederename",
            //        PrefixMobielNummer = "+32",
            //        MobielNummer = "412348765",
            //        Email = "verzonnenmail04@gmail.com",
            //        AantalMeter = 3,
            //        Meterprijs = 1m,
            //        InschrijvingsstatusId = Guid.Parse("4c83bb5b-30b2-4ac7-9662-eab035157b86"),     // Aangevraagd
            //        BetaalmethodeId = Guid.Parse("a833e881-dbdb-4a09-a718-a44d1f3ce0ea"),           // Overschrijving
            //        EvenementId = Guid.Parse("c4660a63-7e82-4e68-92c9-85f3c193f69e")                // Rommelmarkt
            //    },
            //    new Inschrijving()
            //    {
            //        Id = Guid.Parse("945fa969-22f7-4bf3-8054-eae61f41ae6a"),
            //        DatumInschrijving = new DateTime(2020, 4, 10),
            //        Voornaam = "Nicole",
            //        Achternaam = "Marysse",
            //        Postcode = "9750",
            //        Gemeente = "Kruisem",
            //        PrefixMobielNummer = "+32",
            //        MobielNummer = "456873421",
            //        Email = "klantheeftgeenmail@delozerkermisvrienden.com",
            //        AantalMeter = 4,
            //        Meterprijs = 1m,
            //        AantalWagens = 1,
            //        Opmerking = "Speelgoed van reeds opgegroeide kleinkinderen.",
            //        Standnummer = "235",
            //        QRCode = "945fa96922f74bf38054eae61f41ae6a",
            //        InschrijvingsstatusId = Guid.Parse("adb494b6-10ae-495a-9ba4-48ef04d0e29f"),     // Gepland
            //        BetaalmethodeId = Guid.Parse("574fa379-3144-49c0-b1f1-5e1514e270af"),           // Contant
            //        EvenementId = Guid.Parse("c4660a63-7e82-4e68-92c9-85f3c193f69e"),               // Rommelmarkt
            //        LidId = Guid.Parse("8ed92433-e0ca-42d5-b80c-89415991f1f2")                      // Thibaut De Poortere
            //    },
            //    new Inschrijving()
            //    {
            //        Id = Guid.Parse("8366d2f6-b8a5-4a90-8250-1e4a87e2ab06"),
            //        DatumInschrijving = new DateTime(2020, 6, 12),
            //        Voornaam = "Jonas",
            //        Achternaam = "Marysse",
            //        Postcode = "2000",
            //        Gemeente = "Antwerpen",
            //        PrefixMobielNummer = "+32",
            //        MobielNummer = "418273645",
            //        Email = "verzonnenmail05@gmail.com",
            //        AantalMeter = 5,
            //        Meterprijs = 1m,
            //        AantalWagens = 1,
            //        RedenAfkeuring = "Reeds 2x online ingeschreven met contante betaling bij inchecken, maar reeds 2x zonder enig tegenbericht niet opgedaagd. Hierdoor wordt u voor deze editie niet toegelaten.",
            //        InschrijvingsstatusId = Guid.Parse("febf6bbe-4d18-46b1-846b-eeec0581b482"),     // Afgekeurd
            //        BetaalmethodeId = Guid.Parse("574fa379-3144-49c0-b1f1-5e1514e270af"),           // Contant
            //        EvenementId = Guid.Parse("c4660a63-7e82-4e68-92c9-85f3c193f69e")                // Rommelmarkt
            //    },
            //    new Inschrijving()
            //    {
            //        Id = Guid.Parse("c7a1c924-711b-4c81-962d-b3a96ffd2e8a"),
            //        DatumInschrijving = new DateTime(2020, 1, 1),
            //        Voornaam = "Tijdelijke",
            //        Achternaam = "Inschrijving",
            //        Postcode = "9000",
            //        Gemeente = "Gent",
            //        PrefixMobielNummer = "+32",
            //        MobielNummer = "418273645",
            //        Email = "verzonnenmail10@gmail.com",
            //        AantalMeter = 30,
            //        Meterprijs = 1m,
            //        AantalWagens = 1,
            //        RedenAfkeuring = "Te verwijderen",
            //        InschrijvingsstatusId = Guid.Parse("febf6bbe-4d18-46b1-846b-eeec0581b482"),     // Afgekeurd
            //        BetaalmethodeId = Guid.Parse("574fa379-3144-49c0-b1f1-5e1514e270af"),           // Contant
            //        EvenementId = Guid.Parse("c4660a63-7e82-4e68-92c9-85f3c193f69e")                // Rommelmarkt
            //    },
            //    new Inschrijving()
            //    {
            //        Id = Guid.Parse("c1dea00c-0513-4ebf-8c01-306c6533a270"),
            //        DatumInschrijving = new DateTime(2020, 1, 1),
            //        Voornaam = "Tijdelijke",
            //        Achternaam = "Inschrijving",
            //        Postcode = "9000",
            //        Gemeente = "Gent",
            //        PrefixMobielNummer = "+32",
            //        MobielNummer = "418273645",
            //        Email = "verzonnenmail11@gmail.com",
            //        AantalMeter = 30,
            //        Meterprijs = 1m,
            //        AantalWagens = 1,
            //        RedenAfkeuring = "Te verwijderen door verwijderen evenement.",
            //        InschrijvingsstatusId = Guid.Parse("febf6bbe-4d18-46b1-846b-eeec0581b482"),     // Afgekeurd
            //        BetaalmethodeId = Guid.Parse("574fa379-3144-49c0-b1f1-5e1514e270af"),           // Contant
            //        EvenementId = Guid.Parse("254df418-2646-44e6-9ae6-78ebd29475f8")
            //    },
            //    new Inschrijving()
            //    {
            //        Id = Guid.Parse("bfebd9b2-7c28-446a-9483-a281550212ec"),
            //        DatumInschrijving = new DateTime(2020, 1, 1),
            //        Voornaam = "Tijdelijke",
            //        Achternaam = "Inschrijving",
            //        Postcode = "9000",
            //        Gemeente = "Gent",
            //        PrefixMobielNummer = "+32",
            //        MobielNummer = "418273645",
            //        Email = "verzonnenmail12@gmail.com",
            //        AantalMeter = 30,
            //        Meterprijs = 1m,
            //        AantalWagens = 1,
            //        RedenAfkeuring = "Te verwijderen door verwijderen evenementGroep.",
            //        InschrijvingsstatusId = Guid.Parse("febf6bbe-4d18-46b1-846b-eeec0581b482"),     // Afgekeurd
            //        BetaalmethodeId = Guid.Parse("574fa379-3144-49c0-b1f1-5e1514e270af"),           // Contant
            //        EvenementId = Guid.Parse("fcaa7d8a-4b3a-4282-962e-2e1bf90d3448")
            //    }
            //);

            //modelBuilder.Entity<Betaaltransactie>().HasData(
            //    new Betaaltransactie()
            //    {
            //        Id = Guid.Parse("e2298ab9-66b2-4bbe-afd3-1406c42c6825"),
            //        DatumBetaling = new DateTime(2020,7,20),
            //        Bedrag = 8m,
            //        VerantwoordelijkeBetaling = "Thibaut De Poortere",
            //        GestructureerdeMededeling = "123123412345",
            //        BetaalmethodeId = Guid.Parse("a833e881-dbdb-4a09-a718-a44d1f3ce0ea"),           // Overschrijving
            //        InschrijvingsId = Guid.Parse("a17c29b6-4cc2-4cc8-9a84-592ae2bafbc7"),
            //        LidId = Guid.Parse("8ed92433-e0ca-42d5-b80c-89415991f1f2")                      // Thibaut De Poortere
            //    },
            //    new Betaaltransactie()
            //    {
            //        Id = Guid.Parse("06b1a479-ad0d-415b-b95e-a594c930879e"),
            //        DatumBetaling = new DateTime(2020, 7, 16),
            //        Bedrag = 8m,
            //        VerantwoordelijkeBetaling = "Charlotte De Poortere",
            //        GestructureerdeMededeling = "123123412345",
            //        BetaalmethodeId = Guid.Parse("a833e881-dbdb-4a09-a718-a44d1f3ce0ea"),           // Overschrijving
            //        InschrijvingsId = Guid.Parse("a17c29b6-4cc2-4cc8-9a84-592ae2bafbc7"),
            //        LidId = Guid.Parse("8ed92433-e0ca-42d5-b80c-89415991f1f2")                      // Thibaut De Poortere
            //    },
            //    new Betaaltransactie()
            //    {
            //        Id = Guid.Parse("84ec6183-4f95-4e8b-bea3-81704dc24bb7"),
            //        DatumBetaling = new DateTime(2020, 5, 18),
            //        Bedrag = 10m,
            //        VerantwoordelijkeBetaling = "Carine De Poortere",
            //        BetaalmethodeId = Guid.Parse("574fa379-3144-49c0-b1f1-5e1514e270af"),           // Contant
            //        InschrijvingsId = Guid.Parse("c565df1d-c604-4ea5-b2da-aedad6e924bf"),
            //        LidId = Guid.Parse("3a041df5-32a4-4d86-add2-8f0c16a407aa")                      // Filip De Poortere
            //    },
            //    new Betaaltransactie()
            //    {
            //        Id = Guid.Parse("601c39e9-ee24-4a23-8a05-036705b0cf91"),
            //        DatumBetaling = new DateTime(2020, 4, 10),
            //        Bedrag = 2m,
            //        VerantwoordelijkeBetaling = "Marc De Kimpe",
            //        BetaalmethodeId = Guid.Parse("574fa379-3144-49c0-b1f1-5e1514e270af"),           // Contant
            //        InschrijvingsId = Guid.Parse("945fa969-22f7-4bf3-8054-eae61f41ae6a"),
            //        LidId = Guid.Parse("3a041df5-32a4-4d86-add2-8f0c16a407aa")                      // Filip De Poortere
            //    },
            //    new Betaaltransactie()
            //    {
            //        Id = Guid.Parse("5cc00408-02a2-4a86-a88b-d6b3f0147a48"),
            //        DatumBetaling = new DateTime(2020, 1, 1),
            //        Bedrag = 30m,
            //        VerantwoordelijkeBetaling = "Te verwijderen",
            //        BetaalmethodeId = Guid.Parse("574fa379-3144-49c0-b1f1-5e1514e270af"),           // Contant
            //        InschrijvingsId = Guid.Parse("945fa969-22f7-4bf3-8054-eae61f41ae6a"),
            //        LidId = Guid.Parse("3a041df5-32a4-4d86-add2-8f0c16a407aa")                      // Filip De Poortere
            //    },
            //    new Betaaltransactie()
            //    {
            //        Id = Guid.Parse("68cb7db0-4af5-4469-9838-099ee2b1c8c0"),                        // Te verwijderen door verwijderen inschrijving
            //        DatumBetaling = new DateTime(2020, 1, 1),
            //        Bedrag = 30m,
            //        VerantwoordelijkeBetaling = "Te verwijderen door verwijderen inschrijving",
            //        BetaalmethodeId = Guid.Parse("574fa379-3144-49c0-b1f1-5e1514e270af"),           // Contant
            //        InschrijvingsId = Guid.Parse("c7a1c924-711b-4c81-962d-b3a96ffd2e8a"),
            //        LidId = Guid.Parse("3a041df5-32a4-4d86-add2-8f0c16a407aa")                      // Filip De Poortere
            //    },
            //    new Betaaltransactie()
            //    {
            //        Id = Guid.Parse("8c72a872-c977-4574-9abc-8c9e911dc4a5"),                        // Te verwijderen door verwijderen Evenement
            //        DatumBetaling = new DateTime(2020, 1, 1),
            //        Bedrag = 30m,
            //        VerantwoordelijkeBetaling = "Te verwijderen door verwijderen Evenement",
            //        BetaalmethodeId = Guid.Parse("574fa379-3144-49c0-b1f1-5e1514e270af"),           // Contant
            //        InschrijvingsId = Guid.Parse("c1dea00c-0513-4ebf-8c01-306c6533a270"),
            //        LidId = Guid.Parse("3a041df5-32a4-4d86-add2-8f0c16a407aa")                      // Filip De Poortere
            //    },
            //    new Betaaltransactie()
            //    {
            //        Id = Guid.Parse("a4f5e57b-4ec3-4b57-8ad7-561f406bde12"),                        // Te verwijderen door verwijderen EvenementGroep
            //        DatumBetaling = new DateTime(2020, 1, 1),
            //        Bedrag = 30m,
            //        VerantwoordelijkeBetaling = "Te verwijderen door verwijderen EvenementGroep",
            //        BetaalmethodeId = Guid.Parse("574fa379-3144-49c0-b1f1-5e1514e270af"),           // Contant
            //        InschrijvingsId = Guid.Parse("bfebd9b2-7c28-446a-9483-a281550212ec"),
            //        LidId = Guid.Parse("3a041df5-32a4-4d86-add2-8f0c16a407aa")                      // Filip De Poortere
            //    },
            //    new Betaaltransactie()
            //    {
            //        Id = Guid.Parse("35009a2b-19df-484d-9cd7-e6f688c23b95"),                        // Te verwijderen door verwijderen EvenementGroep
            //        DatumBetaling = new DateTime(2020, 1, 1),
            //        Bedrag = 30m,
            //        VerantwoordelijkeBetaling = "Te verwijderen door verwijderen EvenementGroep",
            //        BetaalmethodeId = Guid.Parse("574fa379-3144-49c0-b1f1-5e1514e270af"),           // Contant
            //        InschrijvingsId = Guid.Parse("bfebd9b2-7c28-446a-9483-a281550212ec"),
            //        LidId = Guid.Parse("3a041df5-32a4-4d86-add2-8f0c16a407aa")                      // Filip De Poortere
            //    }
            //);

            //modelBuilder.Entity<CheckIn>().HasData(
            //    new CheckIn()
            //    {
            //        Id = Guid.Parse("195925fe-c828-4572-a039-4d0f2bea8a58"),
            //        InschrijvingsId = Guid.Parse("a17c29b6-4cc2-4cc8-9a84-592ae2bafbc7"),
            //        LidId = Guid.Parse("3a041df5-32a4-4d86-add2-8f0c16a407aa"),
            //        CheckInMoment = new DateTime(2020, 2,15)
            //    },
            //    new CheckIn()
            //    {
            //        // Te verwijderen
            //        Id = Guid.Parse("7a439907-d6a5-477b-93d9-e34573f2c8d4"),
            //        InschrijvingsId = Guid.Parse("c7a1c924-711b-4c81-962d-b3a96ffd2e8a"),
            //        CheckInMoment = new DateTime(2020, 2, 1)
            //    },
            //    new CheckIn()
            //    {
            //        // Te verwijderen door verwijderen inschrijving
            //        Id = Guid.Parse("18ccc083-02f9-4487-9935-18ae25d12100"),
            //        InschrijvingsId = Guid.Parse("c7a1c924-711b-4c81-962d-b3a96ffd2e8a"),
            //        CheckInMoment = new DateTime(2020, 2, 1)
            //    },
            //    new CheckIn()
            //    {
            //        // Te verwijderen door verwijderen Evenement
            //        Id = Guid.Parse("cadad060-3162-45cf-9076-fc7dfbfc6ddd"),
            //        InschrijvingsId = Guid.Parse("c1dea00c-0513-4ebf-8c01-306c6533a270"),
            //        LidId = Guid.Parse("3a041df5-32a4-4d86-add2-8f0c16a407aa"),
            //        CheckInMoment = new DateTime(2020, 3, 1)
            //    },
            //    new CheckIn()
            //    {
            //        // Te verwijderen door verwijderen EvenementGroep
            //        Id = Guid.Parse("b950c3d8-9ba5-4588-8781-a2e031389616"),
            //        InschrijvingsId = Guid.Parse("bfebd9b2-7c28-446a-9483-a281550212ec"),
            //        CheckInMoment = new DateTime(2020, 1, 1)
            //    }
            //);

            //modelBuilder.Entity<Login>().HasData(
            //    new Login()
            //    {
            //        LidId = Guid.Parse("8ed92433-e0ca-42d5-b80c-89415991f1f2"),
            //        Wachtwoord = "Hier komt een encrypted wachtwoord!",
            //    },
            //    new Login()
            //    {
            //        LidId = Guid.Parse("3a041df5-32a4-4d86-add2-8f0c16a407aa"),
            //        Wachtwoord = "Hier komt een encrypted wachtwoord!",
            //    },
            //    new Login()
            //    {
            //        LidId = Guid.Parse("ce467c89-7619-45aa-be44-ec214b54aca0"),
            //        Wachtwoord = "Te verwijderen door Lid"
            //    }
            //);


            base.OnModelCreating(modelBuilder);
        }
            
    }
}
