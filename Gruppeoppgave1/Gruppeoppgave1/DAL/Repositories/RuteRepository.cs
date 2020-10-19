using Gruppeoppgave1.DAL.IRepositories;
using Gruppeoppgave1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gruppeoppgave1.DAL.Repositories
{
    public class RuteRepository : IRuteRepository
    {
        private readonly BestillingContext _db;

        public RuteRepository(BestillingContext db)
        {
            _db = db;
        }

        public async Task<bool> EndreRute(Rute rute)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Rute>> HentAlleRuter()
        {
            throw new NotImplementedException();
        }

        public async Task<Rute> HentEnRute(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> LeggTilRute(Rute rute)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SlettRute(int id)
        {
            throw new NotImplementedException();
        }
    }
}
