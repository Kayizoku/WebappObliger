using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Gruppeoppgave1.Model
{
    public class Bestilling
    {
        [Key]
        public int Id { get; set; }
        public double pris { get; set; }
        public string Fra { get; set; }
        public string Til { get; set; }
        public string Dato { get; set; }
        public string Tid { get; set; }
    }
}