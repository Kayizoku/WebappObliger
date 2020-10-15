using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gruppeoppgave1.Model
{
    public class Bruker
    {
        [Key]
        public int Id { get; set; }
        public string BrukerNavn { get; set; }

        public string Passord { get; set; };
    }
}
