using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gruppeoppgave1.DAL
{
    public class AvgangRepository : IAvgangerRepository
    {
        prvate readonly AvgangContext _db;

        public AvgangRepository(AvgangContext db)
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

    }
}
