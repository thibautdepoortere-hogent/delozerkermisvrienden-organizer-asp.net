using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Entities
{
    public class CheckIn
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? InschrijvingsId { get; set; }

        public Guid? LidId { get; set; }

        public DateTime CheckInMoment { get; set; }

        //

        [ForeignKey("InschrijvingsId")]
        public Inschrijving Inschrijving { get; set; }

        [ForeignKey("LidId")]
        public Lid Lid { get; set; }
    }
}
