using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Gruppeoppgave1.Model
{
    [ExcludeFromCodeCoverage]
    public class Bestilling
    {
        
        public int Id { get; set; }
        [Required]
        public double pris { get; set; }
        [Required]
        public string Fra { get; set; }
        [Required]
        public string Til { get; set; }
        [Required]
        public string Dato { get; set; }
        [Required]
        public string Tid { get; set; }
    }
}