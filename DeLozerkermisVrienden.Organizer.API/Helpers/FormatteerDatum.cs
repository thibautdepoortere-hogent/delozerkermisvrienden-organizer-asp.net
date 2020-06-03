using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Helpers
{
    public static class FormatteerDatum
    {
        public static string DateTimeOmzettenNaarExportNotatie(this DateTime datum)
        {
            if (datum == null)
            {
                return "";
            }

            return datum.ToString("MM/dd/yyyy HH:mm:ss");
        }

        public static string DateTimeOmzettenNaarBelgischeNotatie(this DateTime datum)
        {
            if (datum == null)
            {
                return "";
            }

            return datum.ToString("dd/MM/yyyy HH:mm:ss");
        }


        public static DateTime? DatumNotatieOmzettenNaarDateTime(this string datumNotatie)
        {
            DateTime datum;
            var isGeldigeDatum = DateTime.TryParse(datumNotatie, out datum);

            if (isGeldigeDatum)
            {
                return datum;
            }
            else
            {
                return null;
            }
        }


        public static bool IsGeldigeDatum(this string datumNotatie)
        {
            DateTime datum;
            return DateTime.TryParse(datumNotatie, out datum);
        }
    }
}
