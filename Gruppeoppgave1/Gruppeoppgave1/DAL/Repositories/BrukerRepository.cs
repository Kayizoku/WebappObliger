using Gruppeoppgave1.DAL.IRepositories;
using Gruppeoppgave1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gruppeoppgave1.DAL.Repositories
{
    public class BrukerRepository : IBrukerRepository
    {
        private readonly BestillingContext _db;

        public BrukerRepository(BestillingContext db)
        {
            _db = db;
        }

        public async Task<bool> LeggTilBruker(Bruker bruker)
        {
            
        }

        public async Task<bool> SjekkBruker(Bruker bruker)
        {
            throw new NotImplementedException();
        }
    }
}
