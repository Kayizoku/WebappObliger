using Gruppeoppgave1.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gruppeoppgave1.DAL.IRepositories
{
    public interface IBrukerRepository
    {
        Task<bool> LoggInn(Bruker bruker);
        Task<List<Avgang>> HentAlleAvgangerAdmin();
        Task<List<Bestilling>> HentAlleBestillingerAdmin();
        Task<List<Rute>> HentAlleRuterAdmin();
        Task<List<Stasjon>> HentAlleStasjonerAdmin();
    }
}
