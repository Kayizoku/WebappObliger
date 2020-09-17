using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gruppeoppgave1.DAL;
using Gruppeoppgave1.Model;

namespace Gruppeoppgave1.DAL
{
    public class BestillingRepository : IBestillingRepository
    {
        private readonly BestillingContext _db;
        public Task<bool> Lagre(Bestilling innBestilling)
        {
            throw new NotImplementedException();
        }

        public Task<List<Bestilling>> HentAlle()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Slett(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Bestilling> HentEn(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Endre(Bestilling endreBestilling)
        {
            throw new NotImplementedException();
        }
    }
}
