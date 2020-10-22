using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Gruppeoppgave1.Model
{
    [ExcludeFromCodeCoverage]
    public class Avgang
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2,20}$")]
        public string Fra { get; set; }
        
        [Required]
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2,20}$")]
        public string Til { get; set; }
        
        [Required]
        public string Tid { get; set; }
    }
}
