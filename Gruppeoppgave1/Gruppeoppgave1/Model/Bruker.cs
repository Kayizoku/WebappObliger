using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gruppeoppgave1.Model
{
    public class Bruker
    {
        
        public int Id { get; set; }

       // [Required(ErrorMessage ="Må skrive inn brukernavn")]
       // [RegularExpression(@"^[0-9a-zA-ZæøåÆØÅ. \-]{2,20}$")]
        public string Brukernavn { get; set; }

       // [Required(ErrorMessage ="Du må skrive inn passord")]
       // [RegularExpression(@"^[0-9a-zA-ZæøåÆØÅ. \-]{8,20}$")]
        public string Passord { get; set; }
    }
}
