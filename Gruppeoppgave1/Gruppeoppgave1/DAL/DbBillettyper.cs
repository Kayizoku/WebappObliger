using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Gruppeoppgave1.Model
{
    public class Billettyper
    {
        [Key]
        public int Id { get; set; }
        public double prisKalk { get; set; }
        public string Type { get; set; }
    }
}
