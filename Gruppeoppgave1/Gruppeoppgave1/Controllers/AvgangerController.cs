using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Gruppeoppgave1.DAL;
using Gruppeoppgave1.DAL.IRepositories;
using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Gruppeoppgave1.Controller;
using Stripe.BillingPortal;

namespace Gruppeoppgave1.Controllers
{
    [Route("avganger/")]
    public class AvgangerController : ControllerBase
    {
        private readonly IAvgangerRepository _db;

        private ILogger<AvgangerController> _log;
        private const string _loggetInn = "logget inn";

        public AvgangerController(IAvgangerRepository db, ILogger<AvgangerController> log)
        {
            _log = log;
            _db = db;
        }

        [Route("leggTilAvgang")]
        public async Task<ActionResult> LeggTil(Avgang avgang)
        {
           if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            if (ModelState.IsValid)
                {
                    bool resultat = await _db.LeggTil(avgang);
                    if (!resultat)
                    {
                        _log.LogError("Kunne ikke legge til avgang");
                        return BadRequest("Kunne ikke legge til avgang");
                    }
                    _log.LogInformation("Avgangen ble lagt til");
                    return Ok("Avgangen ble lagt til");
                }
                _log.LogError("Avgangsobjektet er ikke riktig");
                return BadRequest("Avgangsobjektet er ikke riktig");
        }

        [Route("hentAlleAvganger")]
        public async Task<ActionResult> HentAlle()
        {
            List<Avgang> liste = await _db.HentAlle();
            return Ok(liste);
        }

        [Route("hentEnAvgang")]
        public async Task<ActionResult> HentEn(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            Avgang avgang = await _db.HentEn(id);
            if(avgang == null)
            {
                _log.LogInformation("Fant ikke avgangen");
                return NotFound("Fant ikke avgangen");
            }
            return Ok(avgang);
        }

        [Route("endreAvgang")]
        public async Task<ActionResult> Endre(Avgang avgang)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            if (ModelState.IsValid)
            {
                bool ok = await _db.Endre(avgang);
                if (!ok)
                {
                    _log.LogError("Avgangen kunne ikke bli endret");
                    return NotFound("Kunne ikke endre på avgangen");
                }
                _log.LogInformation("Avgangen ble endret");
                return Ok("Avgangen ble endret");
            }
            _log.LogError("Feil i inputvalidering");
            return BadRequest("Avgangen er feil");
            
        }

        [Route("slettAvgang")]
        public async Task<ActionResult> Slett(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            bool ok =  await _db.Slett(id);
            if (!ok)
            {
                _log.LogError("Kunne ikke slette avgangen");
                return NotFound("Kunne ikke slette avgangen");
            }
            _log.LogInformation("Avgangen ble slettet");
            return Ok("Avgangen ble slettet");
        }
    }

}

