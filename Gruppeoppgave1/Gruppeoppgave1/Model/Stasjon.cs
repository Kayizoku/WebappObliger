using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Gruppeoppgave1.Model
{
    public class Stasjon
    {
        public int Id { get; set; }
        [Required]
        public int NummerPaaStopp { get; set; }
        [Required]
        public string StasjonsNavn { get; set; }
    }
} 