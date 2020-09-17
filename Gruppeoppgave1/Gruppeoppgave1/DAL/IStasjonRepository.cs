using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gruppeoppgave1.Model;

namespace Gruppeoppgave1.DAL
{
    public interface IStasjonRepository
    {
        Task<List<Stasjon>> HentAlle();
        Task<Stasjon> HentEn(int id);
    }
}
