using System.Collections.Generic;
using System.Threading.Tasks;
using Gruppeoppgave1.Controllers;
using Gruppeoppgave1.DAL;
using Gruppeoppgave1.DAL.IRepositories;
using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Gruppeoppgave1.Controller
{
    [Route("bestillinger/")]
    public class BestillingController : ControllerBase
    {
        private readonly IBestillingRepository _db;

        private ILogger<BestillingController> _log;

        public BestillingController(IBestillingRepository db, ILogger<BestillingController> log)
        {
            _db = db;
            _log = log;
        }

        [Route("lagreBestilling")]
        public async Task<ActionResult> Lagre(Bestilling innBestilling)
        {
            bool returOK = await _db.Lagre(innBestilling);
            if (!returOK)
            {
                _log.LogInformation("Bestillingen ble ikke lagret");
                return BadRequest("Kunden ble ikke lagret");
            }
            return Ok("Kunde lagret");
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