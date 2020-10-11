using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gruppeoppgave1.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Gruppeoppgave1.DAL
{
    [ExcludeFromCodeCoverage]
    public class AvgangRepository : IAvgangerRepository
    {
        private readonly BestillingContext _db;

        public AvgangRepository(BestillingContext db)
        {
            _db = db;
        }

        public async Task<bool> LeggTil(Avgang avgang)
        {
            try
            {
                var nyAvgang = new Avganger();
                nyAvgang.Id = avgang.Id;
                nyAvgang.Fra = avgang.Fra;
                nyAvgang.Til = avgang.Til;
                nyAvgang.Tid = avgang.Tid;
                _db.Avganger.Add(nyAvgang);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Avgang>> HentAlle()
        {
            try
            {
                List<Avgang> alleAvganger = await _db.Avganger.Select(a => new Avgang
                {
                    Id = a.Id,
                    Fra = a.Fra,
                    Til = a.Til,
                    Tid = a.Tid
                }).ToListAsync();
                return alleAvganger;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Avgang> HentEn(int id)
        {
            Avganger enAvgang = await _db.Avganger.FindAsync(id);
            var hentetAvgang = new Avgang()
            {
                Id = enAvgang.Id,
                Fra = enAvgang.Fra,
                Til = enAvgang.Til,
                Tid = enAvgang.Tid
            };
            return hentetAvgang;
        }


        public async Task<bool> Endre(Avgang avgang)
        {
            try
            {
                Avganger enAvgang = await _db.Avganger.FindAsync(avgang.Id);
                enAvgang.Fra = avgang.Fra;
                enAvgang.Til = avgang.Til;
                enAvgang.Tid = avgang.Tid;
                await _db.SaveChangesAsync();
            } 
            catch
            {
                return false;
            }
            return true;
        }


        public async Task<bool> Slett(int id)
        {
            try
            {
                Avganger enDBavgang = await _db.Avganger.FindAsync(id);
                _db.Avganger.Remove(enDBavgang);
                await _db.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}
