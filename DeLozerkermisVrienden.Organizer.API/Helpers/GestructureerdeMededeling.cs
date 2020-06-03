using DeLozerkermisVrienden.Organizer.API.ResourceParameters;
using DeLozerkermisVrienden.Organizer.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Helpers
{
    public static class GestructureerdeMededeling
    {
        private static readonly Random _rand = new Random();
        public static string GetNieuweGestructureerdeMededeling()
        {
            string huidigJaar = DateTime.Now.Year.ToString("0000");
            long willekeuringGetal;
            if (long.TryParse($"{huidigJaar}{_rand.Next(100000, 999999)}", out willekeuringGetal))
            {
                long rest = willekeuringGetal % 97;
                return $"{willekeuringGetal:0000000000}{rest:00}";
            }
            return string.Empty;
        }

        public static string GetNieuweGestructureerdeMededeling(IInschrijvingRepository inschrijvingRepository)
        {
            if (inschrijvingRepository == null)
            {
                throw new ArgumentNullException(nameof(inschrijvingRepository));
            }

            string gestructureerdeMededeling = "";
            bool inschrijvingIsUniek = false;
            int aantalPogingen = 0;
            do
            {
                aantalPogingen++;
                gestructureerdeMededeling = GetNieuweGestructureerdeMededeling();
                if (!string.IsNullOrWhiteSpace(gestructureerdeMededeling))
                {
                    InschrijvingenResourceParameters parameters = new InschrijvingenResourceParameters() {GestructureerdeMededeling = gestructureerdeMededeling};

                    IEnumerable<Entities.Inschrijving> inschrijvingen = inschrijvingRepository.GetInschrijvingen(parameters);
                    if (inschrijvingen != null)
                    {
                        inschrijvingIsUniek = (inschrijvingen.Count() == 0);
                    }
                }
            } while (!inschrijvingIsUniek & (aantalPogingen < 10));
            
            if (inschrijvingIsUniek)
            {
                return gestructureerdeMededeling;
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetGeformatteerdeGestructureerdeMededeling(string gestructureerdeMededeling)
        {
            if (!string.IsNullOrWhiteSpace(gestructureerdeMededeling))
            {
                if (gestructureerdeMededeling.Length == 12)
                {
                    return $"+++{gestructureerdeMededeling.Substring(0, 3)}/{gestructureerdeMededeling.Substring(3, 4)}/{gestructureerdeMededeling.Substring(7, 5)}+++";
                }
            }
            return string.Empty;
        }
    }
}
