using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Gruppeoppgave1.Model
{
    public class Stasjoner
    {
        [Key]
        public int Id { get; set; }
        public int NummerP�Stopp { get; set; }
        public string Stasjonsnavn { get; set; }
        public virtual Rute;
    }
} 