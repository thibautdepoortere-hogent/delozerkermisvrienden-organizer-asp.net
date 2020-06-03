using DeLozerkermisVrienden.Organizer.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.ValidationAttributes
{
    public class Evenement_ControlePeriodeEvenement : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var evenement = (EvenementVoorManipulatieDto)validationContext.ObjectInstance;

            if (evenement.DatumStartEvenement.HasValue && evenement.DatumEindeEvenement.HasValue)
            {
                if (evenement.DatumStartEvenement.Value > evenement.DatumEindeEvenement.Value)
                {
                    return new ValidationResult(ErrorMessage, new[] { nameof(EvenementVoorManipulatieDto) });
                }
            }

            return ValidationResult.Success;
        }
    }
}
