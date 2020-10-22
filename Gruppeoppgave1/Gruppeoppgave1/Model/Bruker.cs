using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Gruppeoppgave1.Model
{
    [ExcludeFromCodeCoverage]
    public class Bruker
    {
        
        public int Id { get; set; }

       [RegularExpression(@"^[0-9a-zA-ZæøåÆØÅ.\-]{2,20}$")]
        public string Brukernavn { get; set; }

       [RegularExpression(@"([0-9a-zA-ZæøåÆØÅ.\-]{8,20})")]
        public string Passord { get; set; }

        public byte[] Hash { get; set; }

        public byte[] Salt { get; set; }
    }
}
