using System.Collections.Generic;
using System.Threading.Tasks;
using Gruppeoppgave1.DAL;
using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gruppeoppgave1.Controller
{
    [Route("bestillinger/")]
    public class BestillingController : ControllerBase
    {
        private readonly IBestillingRepository _db;

        public BestillingController(IBestillingRepository db)
        {
            _db = db;
        }

        [Route("lagreBestilling")]
        public async Task<bool> Lagre(Bestilling innBestilling)
        {
            return await _db.Lagre(innBestilling);
        }

        [Route("hentAlleBestillinger")]
        public async Task<List<Bestilling>> HentAlle()
        {
            return await _db.HentAlle();
        }

        [Route("hentEnBestilling")]
        public async Task<Bestilling> HentEn(int id)
        {
            return await _db.HentEn(id);
        }

        [Route("slettEnBestilling")]
        public async Task<bool> Slett(int id)
        {
            return await _db.Slett(id);
        }

        [Route("endreEnBestilling")]
        public async Task<bool> Endre(Bestilling innBestilling)
        {
            return await _db.Endre(innBestilling);
        }
    }
}