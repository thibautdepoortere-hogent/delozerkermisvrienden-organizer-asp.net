using DeLozerkermisVrienden.Organizer.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.ValidationAttributes
{
    public class Evenement_ControlePeriodeInschrijving : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var evenement = (EvenementVoorManipulatieDto)validationContext.ObjectInstance;

            if (evenement.DatumStartInschrijvingen.HasValue && evenement.DatumEindeInschrijvingen.HasValue)
            {
                if (evenement.DatumStartInschrijvingen.Value > evenement.DatumEindeInschrijvingen.Value)
                {
                    return new ValidationResult(ErrorMessage, new[] { nameof(EvenementVoorManipulatieDto) });
                }
            }

            return ValidationResult.Success;
        }
    }
}
