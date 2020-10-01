using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gruppeoppgave1.Model;

namespace Gruppeoppgave1.DAL
{
    public interface IAvgangerRepository
    {
        Task<List<Avgang>> HentAlle();
        Task<Avganger> HentEn(int id);
    }
}
