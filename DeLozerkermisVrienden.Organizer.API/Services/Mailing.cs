using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeLozerkermisVrienden.Organizer.API.Helpers;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace DeLozerkermisVrienden.Organizer.API.Services
{
    public class Mailing : IMailing
    {
        private readonly IEvenementRepository _evenementRepository;
        private readonly IBetaalmethodeRepository _betaalmethodeRepository;
        private readonly string _apiKey;
        private readonly string _rekeningnummer;
        private readonly string _email;
        private readonly string _telefoon;
        private readonly bool _versturenVanAutomatischeMails = true;
        public Mailing(IAppSettings appSettings, IEvenementRepository evenementRepository, IBetaalmethodeRepository betaalmethodeRepository)
        {
            _evenementRepository = evenementRepository ?? throw new ArgumentNullException(nameof(evenementRepository));
            _betaalmethodeRepository = betaalmethodeRepository ?? throw new ArgumentNullException(nameof(betaalmethodeRepository));
            if (appSettings == null)
            {
                throw new ArgumentNullException(nameof(appSettings));
            }
            _apiKey = appSettings.ApiKey_SendGrid();
            _rekeningnummer = appSettings.RekeningnummerEvenementen();
            _email = appSettings.EmailEvenementen();
            _telefoon = appSettings.TelefoonEvenementen();
            _versturenVanAutomatischeMails = appSettings.VersturenVanAutomatischeMails();
        }

        public void VerstuurMail_NieuweAanvraag(Entities.Inschrijving inschrijving)
        {
            if (_versturenVanAutomatischeMails)
            {
                KeyValuePair<string, string> verzender = new KeyValuePair<string, string>(_email, "De Lozerkermis vrienden");
                ICollection<KeyValuePair<string, string>> ontvangers = new List<KeyValuePair<string, string>>();
                ontvangers.Add(new KeyValuePair<string, string>(inschrijving.Email, $"{inschrijving.Voornaam} {inschrijving.Achternaam}"));
                string onderwerp = "Bevestiging van uw aanvraag tot inschrijving";

                ICollection<string> inhoudDeel1 = new List<string>();
                inhoudDeel1.Add($"Beste");
                inhoudDeel1.Add($"Wij hebben uw aanvraag tot inschrijving goed ontvangen, waarvoor dank!");
                inhoudDeel1.Add($"Uw aanvraag is nu in behandeling. Deze mail geldt dus <<niet als inschrijvingsbewijs!>>");

                // Link naar opvolgen status

                ICollection<string> inhoudDeel2 = new List<string>();
                inhoudDeel2.Add($"Hieronder vindt u nog eens alle informatie die u reeds ingevuld hebt:");

                // Details inschrijving

                ICollection<string> inhoudDeel3 = new List<string>();
                inhoudDeel3.Add($"Wij danken u alvast voor uw aanvraag en houden u op de hoogte!");
                inhoudDeel3.Add($"Met vriendelijke groeten");
                inhoudDeel3.Add($"De Lozerkermis vrienden");


                string formaat = "html";
                StringBuilder inhoudHtml = new StringBuilder();
                inhoudHtml = GetInhoudVolgensFormaat(inhoudDeel1, inhoudHtml, formaat);
                inhoudHtml.AppendLine($"Via deze <a href='https://delozerkermisvrienden-organizer-ui.azurewebsites.net/inschrijvingen/{inschrijving.Id}/status'>link</a> kan u de status van uw aanvraag volgen. U krijgt zo snel mogelijk een bericht met de melding of uw inschrijving al dan niet is goedgekeurd.");
                inhoudHtml = GetInhoudVolgensFormaat(inhoudDeel2, inhoudHtml, formaat);
                inhoudHtml = DetailsInschrijvingHtmlAanvullen(inschrijving, inhoudHtml);
                inhoudHtml = GetInhoudVolgensFormaat(inhoudDeel3, inhoudHtml, formaat);

                formaat = "txt";
                StringBuilder inhoudTxt = new StringBuilder();
                inhoudTxt = GetInhoudVolgensFormaat(inhoudDeel1, inhoudTxt, formaat);
                inhoudTxt.AppendLine($"Via deze link (https://delozerkermisvrienden-organizer-ui.azurewebsites.net/inschrijvingen/{inschrijving.Id}/status) kan u de status van uw aanvraag volgen. U krijgt zo snel mogelijk een bericht met de melding of uw inschrijving al dan niet is goedgekeurd.");
                inhoudTxt = GetInhoudVolgensFormaat(inhoudDeel2, inhoudTxt, formaat);
                inhoudTxt = DetailsInschrijvingTxtAanvullen(inschrijving, inhoudTxt);
                inhoudTxt = GetInhoudVolgensFormaat(inhoudDeel3, inhoudTxt, formaat);

                SendGridMessage bericht = MaakNieuwSendGridBericht(verzender, ontvangers, onderwerp, inhoudTxt.ToString(), inhoudHtml.ToString());

                VerstuurMail(bericht).Wait();
            }
        }

        public void VerstuurMail_AanvraagGoedgekeurd(Entities.Inschrijving inschrijving)
        {
            if (_versturenVanAutomatischeMails)
            {
                KeyValuePair<string, string> verzender = new KeyValuePair<string, string>(_email, "De Lozerkermis vrienden");
                ICollection<KeyValuePair<string, string>> ontvangers = new List<KeyValuePair<string, string>>();
                ontvangers.Add(new KeyValuePair<string, string>(inschrijving.Email, $"{inschrijving.Voornaam} {inschrijving.Achternaam}"));
                string onderwerp = "Uw aanvraag tot inschrijving is goedgekeurd";

                ICollection<string> inhoudDeel1 = new List<string>();
                inhoudDeel1.Add($"Beste");
                inhoudDeel1.Add($"Wij hebben het genoegen u mede te delen dat uw aanvraag werd goedgekeurd.");
                if (inschrijving.BetaalmethodeId.Value == _betaalmethodeRepository.GetBetaalmethode_Contant().Id)
                {
                    inhoudDeel1.Add($"Graag herinneren wij u eraan dat u als betaalmethode <<contant>> had aangeduid.");
                    inhoudDeel1.Add($"Mogen wij u vragen <<het gepaste bedrag ({(inschrijving.AantalMeter * inschrijving.Meterprijs).ToString("N", new CultureInfo("nl-BE"))} EUR)>> mee te brengen en ter plaatse te betalen bij de inschrijvingspost op de dag van het evenement?");
                }
                else if (inschrijving.BetaalmethodeId.Value == _betaalmethodeRepository.GetBetaalmethode_Overschrijving().Id)
                {
                    inhoudDeel1.Add($"Graag herinneren wij u eraan dat u als betaalmethode <<overschrijving>> had aangeduid.");
                    inhoudDeel1.Add($"Mogen wij u vragen het gepaste bedrag ({(inschrijving.AantalMeter * inschrijving.Meterprijs).ToString("N", new CultureInfo("nl-BE"))} EUR) zo snel mogelijk over te schrijven op het rekeningnummer {_rekeningnummer}, met volgende gestructureerde mededeling: {GestructureerdeMededeling.GetGeformatteerdeGestructureerdeMededeling(inschrijving.GestructureerdeMededeling)}. De overschrijving dient <<ten laatste 5 dagen voor het evenement>> te gebeuren.");
                    inhoudDeel1.Add($"Indien wij geen overschrijving van u ontvangen hebben, gelieve op de dag van het evenement <<het gepaste bedrag>> dan cash mee te brengen en ter plaatse te betalen bij de inschrijvingspost.");
                }
                inhoudDeel1.Add($"Wij danken u alvast voor uw medewerking!");

                // Link naar opvolgen status

                ICollection<string> inhoudDeel2 = new List<string>();
                inhoudDeel2.Add($"Hieronder vindt u nogmaals alle informatie die u reeds ingevuld hebt:");

                // Details inschrijving

                ICollection<string> inhoudDeel3 = new List<string>();
                inhoudDeel3.Add($"Tot dan!");
                inhoudDeel3.Add($"Met vriendelijke groeten");
                inhoudDeel3.Add($"De Lozerkermis vrienden");


                string formaat = "html";
                StringBuilder inhoudHtml = new StringBuilder();
                inhoudHtml = GetInhoudVolgensFormaat(inhoudDeel1, inhoudHtml, formaat);
                inhoudHtml.AppendLine($"Via deze <a href='https://delozerkermisvrienden-organizer-ui.azurewebsites.net/inschrijvingen/{inschrijving.Id}/status'>link</a> kan u een QR-code raadplegen. Bied deze QR-code aan bij het inchecken, dit zal het proces vlotter laten verlopen.");
                inhoudHtml = GetInhoudVolgensFormaat(inhoudDeel2, inhoudHtml, formaat);
                inhoudHtml = DetailsInschrijvingHtmlAanvullen(inschrijving, inhoudHtml);
                inhoudHtml = GetInhoudVolgensFormaat(inhoudDeel3, inhoudHtml, formaat);

                formaat = "txt";
                StringBuilder inhoudTxt = new StringBuilder();
                inhoudTxt = GetInhoudVolgensFormaat(inhoudDeel1, inhoudTxt, formaat);
                inhoudTxt.AppendLine($"Via deze link (https://delozerkermisvrienden-organizer-ui.azurewebsites.net/inschrijvingen/{inschrijving.Id}/status) kan u een QR-code raadplegen. Bied deze QR-code aan bij het inchecken, dit zal het proces vlotter laten verlopen.");
                inhoudTxt = GetInhoudVolgensFormaat(inhoudDeel2, inhoudTxt, formaat);
                inhoudTxt = DetailsInschrijvingTxtAanvullen(inschrijving, inhoudTxt);
                inhoudTxt = GetInhoudVolgensFormaat(inhoudDeel3, inhoudTxt, formaat);



                SendGridMessage bericht = MaakNieuwSendGridBericht(verzender, ontvangers, onderwerp, inhoudTxt.ToString(), inhoudHtml.ToString());

                VerstuurMail(bericht).Wait();
            }
        }

        public void VerstuurMail_AanvraagAfgekeurd(Entities.Inschrijving inschrijving)
        {
            if (_versturenVanAutomatischeMails)
            {
                KeyValuePair<string, string> verzender = new KeyValuePair<string, string>(_email, "De Lozerkermis vrienden");
                ICollection<KeyValuePair<string, string>> ontvangers = new List<KeyValuePair<string, string>>();
                ontvangers.Add(new KeyValuePair<string, string>(inschrijving.Email, $"{inschrijving.Voornaam} {inschrijving.Achternaam}"));
                string onderwerp = "Uw aanvraag tot inschrijving is afgekeurd";

                ICollection<string> inhoudDeel1 = new List<string>();
                inhoudDeel1.Add($"Beste");
                inhoudDeel1.Add($"Via deze weg moeten u jammer genoeg mededelen dat uw aanvraag <<niet werd goedgekeurd>>.");
                inhoudDeel1.Add($"De reden voor de afkeuring van uw aanvraag is:");
                inhoudDeel1.Add($"{inschrijving.RedenAfkeuring}");
                inhoudDeel1.Add($"Wij willen u alsnog bedanken voor uw aanvraag en hopen u in de toekomst toch te mogen verwelkomen op één van onze evenementen. U bent uiteraard nog steeds van harte welkom als bezoeker.");
                inhoudDeel1.Add($"Mocht u toch nog vragen hebben omtrent uw aanvraag of ons evenement, kan u ons steeds contacteren via {_email} of {_telefoon}.");
                inhoudDeel1.Add($"Met vriendelijke groeten");
                inhoudDeel1.Add($"De Lozerkermis vrienden");


                string formaat = "html";
                StringBuilder inhoudHtml = new StringBuilder();
                inhoudHtml = GetInhoudVolgensFormaat(inhoudDeel1, inhoudHtml, formaat);

                formaat = "txt";
                StringBuilder inhoudTxt = new StringBuilder();
                inhoudTxt = GetInhoudVolgensFormaat(inhoudDeel1, inhoudTxt, formaat);



                SendGridMessage bericht = MaakNieuwSendGridBericht(verzender, ontvangers, onderwerp, inhoudTxt.ToString(), inhoudHtml.ToString());

                VerstuurMail(bericht).Wait();
            }
        }

        private StringBuilder GetInhoudVolgensFormaat(ICollection<string> bron, StringBuilder doel, string formaat)
        {
            switch (formaat)
            {
                case "txt":
                default:
                    foreach (var regel in bron)
                    {
                        doel.AppendLine(regel.Replace("<<", "").Replace(">>", ""));
                    }
                    return doel;
                case "html":
                    foreach (var regel in bron)
                    {
                        doel.AppendLine("<p>" + regel.Replace("<<", "<strong>").Replace(">>", "</strong>") + "</p>");
                    }
                    return doel;
            }
        }

        private StringBuilder DetailsInschrijvingHtmlAanvullen(Entities.Inschrijving inschrijving, StringBuilder bestaandeStringBuilder)
        {
            bestaandeStringBuilder.AppendLine($"<table><tr><th>Eigenschap</th><th>Waarde</th></tr>");
            bestaandeStringBuilder.AppendLine($"<tr><td>Evenement</td><td>{_evenementRepository.GetEvenement(inschrijving.EvenementId.Value).Naam}</td></tr>");
            bestaandeStringBuilder.AppendLine($"<tr><td>Inschrijvingsnummer</td><td>{inschrijving.Id:D}</td></tr>");
            bestaandeStringBuilder.AppendLine($"<tr><td>Datum inschrijving</td><td>{inschrijving.DatumInschrijving.DateTimeOmzettenNaarBelgischeNotatie()}</td></tr>");
            bestaandeStringBuilder.AppendLine($"<tr><td>Voornaam</td><td>{inschrijving.Voornaam}</td></tr>");
            bestaandeStringBuilder.AppendLine($"<tr><td>Achternaam</td><td>{inschrijving.Achternaam}</td></tr>");
            bestaandeStringBuilder.AppendLine($"<tr><td>Postcode</td><td>{inschrijving.Postcode}</td></tr>");
            bestaandeStringBuilder.AppendLine($"<tr><td>Gemeente</td><td>{inschrijving.Gemeente}</td></tr>");
            bestaandeStringBuilder.AppendLine($"<tr><td>Mobiel nummer</td><td>{inschrijving.PrefixMobielNummer} {inschrijving.MobielNummer}</td></tr>");
            bestaandeStringBuilder.AppendLine($"<tr><td>E-mail</td><td>{inschrijving.Email}</td></tr>");
            bestaandeStringBuilder.AppendLine($"<tr><td>Aantal meter</td><td>{inschrijving.AantalMeter}</td></tr>");
            bestaandeStringBuilder.AppendLine($"<tr><td>Totale prijs</td><td>{(inschrijving.AantalMeter * inschrijving.Meterprijs).ToString("N", new CultureInfo("nl-BE"))}</td></tr>");
            bestaandeStringBuilder.AppendLine($"<tr><td>Aantal wagens</td><td>{inschrijving.AantalWagens.Value}</td></tr>");
            bestaandeStringBuilder.AppendLine($"<tr><td>Aantal aanhangwagens</td><td>{inschrijving.AantalAanhangwagens.Value}</td></tr>");
            bestaandeStringBuilder.AppendLine($"<tr><td>Aantal mobilhomes</td><td>{inschrijving.AantalMobilhomes.Value}</td></tr>");
            bestaandeStringBuilder.AppendLine($"<tr><td>Betaalmethode</td><td>{_betaalmethodeRepository.GetBetaalmethode(inschrijving.BetaalmethodeId.Value).Naam}</td></tr>");
            bestaandeStringBuilder.AppendLine($"<tr><td>Opmerking</td><td>{inschrijving.Opmerking}</td></tr>");
            bestaandeStringBuilder.AppendLine($"</table>");

            return bestaandeStringBuilder;
        }

        private StringBuilder DetailsInschrijvingTxtAanvullen(Entities.Inschrijving inschrijving, StringBuilder bestaandeStringBuilder)
        {
            bestaandeStringBuilder.AppendLine($"Evenement: {_evenementRepository.GetEvenement(inschrijving.EvenementId.Value).Naam}");
            bestaandeStringBuilder.AppendLine($"Inschrijvingsnummer: {inschrijving.Id:D}");
            bestaandeStringBuilder.AppendLine($"Datum inschrijving: {inschrijving.DatumInschrijving.DateTimeOmzettenNaarBelgischeNotatie()}");
            bestaandeStringBuilder.AppendLine($"Voornaam: {inschrijving.Voornaam}");
            bestaandeStringBuilder.AppendLine($"Achternaam: {inschrijving.Achternaam}");
            bestaandeStringBuilder.AppendLine($"Postcode: {inschrijving.Postcode}");
            bestaandeStringBuilder.AppendLine($"Gemeente: {inschrijving.Gemeente}");
            bestaandeStringBuilder.AppendLine($"Mobiel nummer: {inschrijving.PrefixMobielNummer} {inschrijving.MobielNummer}");
            bestaandeStringBuilder.AppendLine($"E-mail: {inschrijving.Email}");
            bestaandeStringBuilder.AppendLine($"Aantal meter: {inschrijving.AantalMeter}");
            bestaandeStringBuilder.AppendLine($"Totale prijs: {(inschrijving.AantalMeter * inschrijving.Meterprijs).ToString("N", new CultureInfo("nl-BE"))}");
            bestaandeStringBuilder.AppendLine($"Aantal wagens: {inschrijving.AantalWagens.Value}");
            bestaandeStringBuilder.AppendLine($"Aantal aanhangwagens: {inschrijving.AantalAanhangwagens.Value}");
            bestaandeStringBuilder.AppendLine($"Aantal mobilhomes: {inschrijving.AantalMobilhomes.Value}");
            bestaandeStringBuilder.AppendLine($"Betaalmethode: {_betaalmethodeRepository.GetBetaalmethode(inschrijving.BetaalmethodeId.Value).Naam}");
            bestaandeStringBuilder.AppendLine($"Opmerking: {inschrijving.Opmerking}");

            return bestaandeStringBuilder;
        }

        private async Task VerstuurMail(SendGridMessage bericht)
        {
            SendGridClient client = new SendGridClient(_apiKey);
            var response = await client.SendEmailAsync(bericht);
        }

        private SendGridMessage MaakNieuwSendGridBericht(KeyValuePair<string, string> verzender, ICollection<KeyValuePair<string, string>> ontvangers, string onderwerp, string inhoudTxt, string inhoudHtml)
        {
            List<EmailAddress> sendGridOntvangers = new List<EmailAddress>();

            if (verzender.Equals(default(KeyValuePair<string, string>)))
            {
                throw new ArgumentNullException(nameof(verzender));
            }

            if (ontvangers == null)
            {
                throw new ArgumentNullException(nameof(ontvangers));
            }

            if (string.IsNullOrWhiteSpace(verzender.Key))
            {
                throw new ArgumentException($"Er is geen e-mail opgegeven voor de verzender");
            }

            if (string.IsNullOrWhiteSpace(verzender.Value))
            {
                throw new ArgumentException($"Er is geen naam opgegeven voor de verzender");
            }

            foreach (KeyValuePair<string, string> ontvanger in ontvangers)
            {
                if (string.IsNullOrWhiteSpace(ontvanger.Key))
                {
                    throw new ArgumentException($"Er is geen e-mail opgegeven voor de ontvanger");
                }

                if (string.IsNullOrWhiteSpace(ontvanger.Value))
                {
                    throw new ArgumentException($"Er is geen naam opgegeven voor de ontvanger");
                }

                sendGridOntvangers.Add(new EmailAddress(ontvanger.Key, ontvanger.Value));
            }

            if (string.IsNullOrWhiteSpace(onderwerp))
            {
                throw new ArgumentException($"Er is geen onderwerp opgegeven");
            }

            SendGridMessage bericht = new SendGridMessage();
            bericht.SetFrom(new EmailAddress(verzender.Key, verzender.Value));
            bericht.AddTos(sendGridOntvangers);
            bericht.AddBcc(_email, "De Lozerkermis vrienden");
            bericht.SetSubject(onderwerp);
            if (!string.IsNullOrWhiteSpace(inhoudTxt))
            {
                bericht.AddContent(MimeType.Text, inhoudTxt);
            }
            if (!string.IsNullOrWhiteSpace(inhoudHtml))
            {
                bericht.AddContent(MimeType.Html, inhoudHtml);
            }

            return bericht;
        }
    }
}