using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Gruppeoppgave1.Model
{
    public class Ruter
    {
        [Key]
        public int Id { get; set; }
        public string Navn { get; set; }
        public List<Stasjoner> stasjoner;

    }
}