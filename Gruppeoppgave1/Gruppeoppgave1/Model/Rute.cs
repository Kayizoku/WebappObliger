using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gruppeoppgave1.Model
{
    public class Rute
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public List<Stasjoner> StationsPaaRute { get; set; }
    }
}
