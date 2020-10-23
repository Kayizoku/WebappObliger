using Gruppeoppgave1.DAL.IRepositories;
using Gruppeoppgave1.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Gruppeoppgave1.DAL.Repositories
{
    [ExcludeFromCodeCoverage]
    public class RuteRepository : IRuteRepository
    {
        private readonly BestillingContext _db;

        private ILogger<RuteRepository> _log;

        public RuteRepository(BestillingContext db, ILogger<RuteRepository> log)
        {
            _log = log;
            _db = db;
        }

        public async Task<bool> EndreRute(Rute nyRute)
        {
            try
            {
                var gammelRute = await _db.Ruter.FindAsync(nyRute.Id);
                gammelRute.Navn = nyRute.Navn;
                gammelRute.StasjonerPaaRute = nyRute.StasjonerPaaRute;
                gammelRute.Id = nyRute.Id;
                await _db.SaveChangesAsync();
            }
            catch(Exception e)
            {
                _log.LogError(e.Message);
                return false;
            }
            return true;
        }

        public async Task<List<Rute>> HentAlleRuter()
        {
            try
            {
                List<Rute> alleRuter = await _db.Ruter.Select(r => new Rute
                {
                    Id = r.Id,
                    Navn = r.Navn,
                    StasjonerPaaRute = r.StasjonerPaaRute
                }).ToListAsync();

                return alleRuter;
            } 
            catch (Exception e)
            {
                _log.LogError(e.Message);
                return null;
            }
        }

        public async Task<Rute> HentEnRute(int id)
        {
            try
            {
                Ruter rute = await _db.Ruter.FindAsync(id);
                var hentetRute = new Rute()
                {
                    Id = rute.Id,
                    Navn = rute.Navn,
                    StasjonerPaaRute = rute.StasjonerPaaRute
                };
                return hentetRute;
            } 
            catch(Exception e)
            {
                _log.LogError(e.Message);
                return null;
            }
        }

        public async Task<bool> LeggTilRute(Rute rute)
        {
            try
            {
                var leggTilRute = new Ruter();
                leggTilRute.Id = rute.Id;
                leggTilRute.Navn = rute.Navn;
                leggTilRute.StasjonerPaaRute = rute.StasjonerPaaRute;
                _db.Ruter.Add(leggTilRute);
                await _db.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                _log.LogError(e.Message);
                return false;
            }
        }

        public async Task<bool> SlettRute(int id)
        {
            try
            {
                Ruter enDBRute = await _db.Ruter.FindAsync(id);
                _db.Ruter.Remove(enDBRute);
                await _db.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                _log.LogError(e.Message);
                return false;
            }
        }
    }
}
