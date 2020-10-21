using Gruppeoppgave1.DAL.IRepositories;
using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Gruppeoppgave1.DAL.Repositories
{
    public class BrukerRepository : IBrukerRepository
    {
        private readonly BestillingContext _db;
        private ILogger<BrukerRepository> _log;

        private const string _loggetInn = "loggetInn";

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
