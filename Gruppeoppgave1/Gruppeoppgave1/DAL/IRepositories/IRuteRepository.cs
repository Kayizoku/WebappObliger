using Gruppeoppgave1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gruppeoppgave1.DAL.IRepositories
{
    public interface IRuteRepository
    {
        Task<bool> LeggTilRute(Rute rute);
        Task<bool> EndreRute(Rute rute);
        Task<bool> SlettRute(int id);
        Task<List<Rute>> HentAlleRuter();
        Task<Rute> HentEnRute(int id);
    }
}
