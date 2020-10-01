using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gruppeoppgave1.Model;

namespace Gruppeoppgave1.DAL
{
    public interface IBestillingRepository
    {
        Task<bool> lagre(Bestilling innBestilling);
        Task<List<Bestilling>> HentAlle();
        Task<Bestilling> HentEn(int id);
    }
}
