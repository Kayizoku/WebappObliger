using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Gruppeoppgave1.Model
{
    [ExcludeFromCodeCoverage]
    public class Rute
    {
        public int Id { get; set; }
        
        [Required]
        public string Navn { get; set; }

        [Required] 
        public List<Stasjon> StasjonerPaaRute { get; set; }
    }
}
