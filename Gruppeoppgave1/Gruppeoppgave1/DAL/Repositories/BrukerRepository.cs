using Castle.Core.Logging;
using Gruppeoppgave1.DAL.IRepositories;
using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Gruppeoppgave1.DAL.Repositories
{
    [ExcludeFromCodeCoverage]
    public class BrukerRepository : IBrukerRepository, IAvgangerRepository
    {
        private readonly BestillingContext _db;
        private ILogger<BrukerRepository> _log;

        public BrukerRepository(BestillingContext db, ILogger<BrukerRepository> log)
        {
            _log = log;
            _db = db;
        }

        public static byte[] Hash(string passord, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                                password: passord,
                                salt: salt,
                                prf: KeyDerivationPrf.HMACSHA512,
                                iterationCount: 1000,
                                numBytesRequested: 32);
        }

        public static byte[] Salt()
        {
            var csp = new RNGCryptoServiceProvider();
            var salt = new byte[24];
            csp.GetBytes(salt);
            return salt;
        }

        public Task<bool> Endre(Avgang avgang)
        {
            throw new NotImplementedException();
        }

        public Task<List<Avgang>> HentAlle()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Avgang>> HentAlleAvgangerAdmin()
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
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return null;
            }
        }

        public async Task<List<Bestilling>> HentAlleBestillingerAdmin()
        {
            try
            {
                List<Bestilling> alleBestillinger = await _db.Bestillinger.Select(b => new Bestilling
                {
                    Id = b.Id,
                    pris = b.Pris,
                    Fra = b.Fra,
                    Til = b.Til,
                    Dato = b.Dato,
                    Tid = b.Tid
                }).ToListAsync();
                return alleBestillinger;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return null;
            }
        }

        public async Task<List<Rute>> HentAlleRuterAdmin()
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
                _log.LogInformation(e.Message);
                return null;
            }
        }

        public async Task<List<Stasjon>> HentAlleStasjonerAdmin()
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
            catch (Exception e)
            {
                _log.LogError(e.Message);
                return null;
            }
        }

        public Task<Avgang> HentEn(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LeggTil(Avgang avgang)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> LoggInn(Bruker bruker)
        {
            try
            {
                Brukere match = await _db.Brukere.FirstOrDefaultAsync(b => b.Brukernavn == bruker.Brukernavn);
                if (match == null)
                {
                    return false;
                }
                else
                {
                    byte[] hash = Hash(bruker.Passord, match.Salt);
                    bool hashMatch = hash.SequenceEqual(match.Passord);
                    if (hashMatch)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }

        public Task<bool> Slett(int id)
        {
            throw new NotImplementedException();
        }
    }
}
