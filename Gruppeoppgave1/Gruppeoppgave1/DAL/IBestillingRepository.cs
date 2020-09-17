using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gruppeoppgave1.Model;

namespace Gruppeoppgave1.DAL
{
    public interface IBestillingRepository
    {
        Task<bool> Lagre(Bestilling innBestilling);
        Task<List<Bestilling>> HentAlle();
        Task<bool> Slett(int id);
        Task<Bestilling> HentEn(int id);
        Task<bool> Endre(Bestilling endreBestilling);
    }
}
