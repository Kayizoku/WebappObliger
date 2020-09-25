using Gruppeoppgave1.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gruppeoppgave1.DAL
{
    public class AvgangRepository : IAvgangerRepository
    {
        private readonly BestillingContext _db;

        public AvgangRepository(BestillingContext db)
        {
            _db = db;
        }

        public async Task<List<Avganger>> HentAlle()
        {
            try
            {
                List<Avgang> alleAvganger = await _db.Avganger.Select(a => new Avgang
                {
                    Id = a.Id,

                }).ToListAsync();
                return alleAvganger;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Avganger> HentEn(int id)
        {
            Avganger enAvgang = await _db.Avganger.FindAsync(id);
            var hentetAvgang = new Avgang()
            {
                Id = enAvgang.Id,

            };
            return hentetAvgang;
        }

        Task<List<Avganger>> IAvgangerRepository.HentAlle()
        {
            throw new NotImplementedException();
        }

        Task<Avganger> IAvgangerRepository.HentEn(int id)
        {
            throw new NotImplementedException();
        }
    }
}
