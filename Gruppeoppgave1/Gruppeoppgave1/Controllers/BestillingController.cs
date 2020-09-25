using System.Collections.Generic;
using System.Threading.Tasks;
using Gruppeoppgave1.DAL;
using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gruppeoppgave1.Controller
{
    [Route("api/bestilling")]
    public class BestillingController : ControllerBase
    {
        private readonly IBestillingRepository _db;

        public BestillingController(IBestillingRepository db)
        {
            _db = db;
        }

        [Route("lagre")]
        public async Task<bool> Lagre(Bestilling innBestilling)
        {
            return await _db.Lagre(innBestilling);
        }

        public async Task<List<Bestilling>> HentAlle()
        {
            return await _db.HentAlle();
        }

        public async Task<Bestilling> HentEn(int id)
        {
            return await _db.HentEn(id);
        }
    }
}