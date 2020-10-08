using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Gruppeoppgave1.Model
{
    public class Avgang
    {
        [Key]
        public int Id { get; set; }
        public string Fra { get; set; }
        public string Til { get; set; }
        public string Tid { get; set; }
    }
}
