using Gruppeoppgave1.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gruppeoppgave1.DAL.IRepositories
{
    public interface IBestillingRepository
    {
        Task<bool> Lagre(Bestilling innBestilling);
        Task<List<Bestilling>> HentAlle();
        Task<Bestilling> HentEn(int id);
        Task<bool> Slett(int id);
        Task<bool> Endre(Bestilling innBestilling);
    }
}
