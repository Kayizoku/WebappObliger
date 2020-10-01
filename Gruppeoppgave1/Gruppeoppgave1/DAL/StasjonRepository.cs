using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gruppeoppgave1.Model;
using Microsoft.EntityFrameworkCore;

namespace Gruppeoppgave1.DAL
{
    public class StasjonRepository : IStasjonRepository
    {
        private readonly BestillingContext _db;

        public StasjonRepository(BestillingContext db)
        {
            _db = db;
        }

        public async Task<List<Stasjon>> HentAlle()
        {
            try
            {
                List<Stasjon> alleStasjoner = await _db.Stasjoner.Select(s => new Stasjon
                {
                    Id = s.Id,
                    NummerPaaStopp = s.NummerPaaStopp,
                    Stasjonsnavn = s.StasjonsNavn,

                }).ToListAsync();
                return alleStasjoner;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Stasjon> HentEn(int id)
        { 
                Stasjoner enStasjon = await _db.Stasjoner.FindAsync(id);
                var hentetStasjon = new Stasjon()
                {
                    Id = enStasjon.Id,
                    NummerPaaStopp = enStasjon.NummerPaaStopp,
                    Stasjonsnavn = enStasjon.StasjonsNavn

                };
                return hentetStasjon;
            }
        }
    }

