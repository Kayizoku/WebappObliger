using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Gruppeoppgave1.DAL;
using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gruppeoppgave1.DAL.IRepositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging;

namespace Gruppeoppgave1.DAL.Repositories
{
    [ExcludeFromCodeCoverage]
    public class BestillingRepository : IBestillingRepository
    {
        private readonly BestillingContext _db;
        private ILogger<BestillingRepository> _log;

        public BestillingRepository(BestillingContext db, ILogger<BestillingRepository> log)
        {
            _log = log;
            _db = db;
        }


        public async Task<bool> Lagre(Bestilling innBestilling)
        {
            try
            {
                var nyBestillingRad = new Bestillinger();
                nyBestillingRad.Pris = innBestilling.pris;
                nyBestillingRad.Fra = innBestilling.Fra;
                nyBestillingRad.Til = innBestilling.Til;
                nyBestillingRad.Dato = innBestilling.Dato;
                nyBestillingRad.Tid = innBestilling.Tid;
                _db.Bestillinger.Add(nyBestillingRad);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _log.LogError(e.Message);
                return false;
            }
        }

        public async Task<List<Bestilling>> HentAlle()
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
                _log.LogError(e.Message);
                return null;
            }
        }

        public async Task<bool> Slett(int id)
        {
            try
            {
                Bestillinger enDBBestilling = await _db.Bestillinger.FindAsync(id);
                _db.Bestillinger.Remove(enDBBestilling);
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.LogError(e.Message);
                return false;
            }
            return true;
        }

        public async Task<Bestilling> HentEn(int id)
        {
            try
            {
                Bestillinger enBestilling = await _db.Bestillinger.FindAsync(id);
                var hentetBestilling = new Bestilling()
                {
                    Id = enBestilling.Id,
                    pris = enBestilling.Pris,
                    Fra = enBestilling.Fra,
                    Til = enBestilling.Til,
                    Dato = enBestilling.Dato,
                    Tid = enBestilling.Tid
                };
                return hentetBestilling;
            }
            catch (Exception e)
            {
                _log.LogError(e.Message);
                return null;
            }
        }

        public async Task<bool> Endre(Bestilling endreBestilling)
        {

            try
            {
                var endreObjekt = await _db.Bestillinger.FindAsync(endreBestilling.Id);
                endreObjekt.Pris = endreBestilling.pris;
                endreObjekt.Fra = endreBestilling.Fra;
                endreObjekt.Til = endreBestilling.Til;
                endreObjekt.Dato = endreBestilling.Dato;
                endreObjekt.Tid = endreBestilling.Tid;
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.LogError(e.Message);
                return false;
            }
            return true;

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

        public async Task<bool> LoggInn(Bruker bruker)
        {
            try
            {
                Brukere match = await _db.Brukere.FirstOrDefaultAsync(b => b.Brukernavn == bruker.Brukernavn);

                byte[] hash = Hash(bruker.Passord, match.Salt);
                bool hashMatch = hash.SequenceEqual(match.Passord);
                if (hashMatch)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }
    }
}
