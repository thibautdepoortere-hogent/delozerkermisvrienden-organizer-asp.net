﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Models
{
    public class InschrijvingsstatusVoorRaadpleegDto
    {
        public Guid Id { get; set; }

        public string Naam { get; set; }

        public int Volgorde { get; set; }
    }
}
