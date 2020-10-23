using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Gruppeoppgave1.Model;

namespace Gruppeoppgave1.DAL.IRepositories
{
    public interface IAvgangerRepository
    {
        Task<bool> LeggTil(Avgang avgang);
        Task<List<Avgang>> HentAlle();
        Task<Avgang> HentEn(int id);
        Task<bool> Endre(Avgang avgang);
        Task<bool> Slett(int id);
    }
}
