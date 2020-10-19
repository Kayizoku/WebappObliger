using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gruppeoppgave1.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Gruppeoppgave1.DAL.IRepositories
{
    public class StasjonRepository : IStasjonRepository
    {
        private readonly BestillingContext _db;

        public StasjonRepository(BestillingContext db)
        {
            _db = db;
        }

        public async Task<List<Stasjon>> HentAlleStasjoner()
        {
            try
            {
                List<Stasjon> alleStasjoner = await _db.Stasjoner.Select(s => new Stasjon
                {
                    Id = s.Id,
                    NummerPaaStopp = s.NummerPaaStopp,
                    StasjonsNavn = s.StasjonsNavn,

                }).ToListAsync();
                return alleStasjoner;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Stasjon> HentEnStasjon(int id)
        { 
            try
            {
                Stasjoner enStasjon = await _db.Stasjoner.FindAsync(id);
                var hentetStasjon = new Stasjon()
                {
                    Id = enStasjon.Id,
                    NummerPaaStopp = enStasjon.NummerPaaStopp,
                    StasjonsNavn = enStasjon.StasjonsNavn

                };
                return hentetStasjon;
            } catch
            {
                return null;
            }
                
        }

        

        public async Task<bool> EndreStasjon(Stasjon stasjon)
        {
            try
            {
                var gammelStasjon = await _db.Stasjoner.FindAsync(stasjon.Id);
                gammelStasjon.NummerPaaStopp = stasjon.NummerPaaStopp;
                gammelStasjon.StasjonsNavn = stasjon.StasjonsNavn;
                await _db.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;

        }

        public async Task<bool> FjernStasjon(int id)
        {
            try
            {
                var fjernetStasjon = await _db.Stasjoner.FindAsync(id);
                _db.Stasjoner.Remove(fjernetStasjon);
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

