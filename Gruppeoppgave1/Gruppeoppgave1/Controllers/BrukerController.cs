using Gruppeoppgave1.DAL.IRepositories;
using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Gruppeoppgave1.Controllers
{
    [Route("bruker/")]
    public class BrukerController: ControllerBase
    {


        private readonly IBrukerRepository _db;

        private ILogger<BrukerController> _log;
        private const string _loggetInn = "logget inn";

        

        public BrukerController(IBrukerRepository db, ILogger<BrukerController> log)
        {
            _log = log;
            _db = db;
        }

        [Route("hentAlleAvgangerAdmin")]
        public async Task<ActionResult> HentAlleAvgangerAdmin()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            List<Avgang> alleAvganger = await _db.HentAlleAvgangerAdmin();
            return Ok(alleAvganger);
        }

        [Route("hentAlleBestillingerAdmin")]
        public async Task<ActionResult> HentAlleBestillingerAdmin()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            List<Bestilling> alleBestillinger = await _db.HentAlleBestillingerAdmin();
            return Ok(alleBestillinger);
        }

        [Route("hentAlleRuterAdmin")]
        public async Task<ActionResult> HentAlleRuterAdmin()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            List<Rute> alleRuter = await _db.HentAlleRuterAdmin();
            return Ok(alleRuter);
        }

        [Route("hentAlleStasjonerAdmin")]
        public async Task<ActionResult> HentAlleStasjonerAdmin()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            List<Stasjon> alleStasjoner = await _db.HentAlleStasjonerAdmin();
            return Ok(alleStasjoner);
        }

        [Route("loggInn")]
        public async Task<ActionResult> LoggInn(Bruker bruker)
        {
            if (ModelState.IsValid)
            {
                bool OK = await _db.LoggInn(bruker);
                if (!OK)
                {
                    _log.LogInformation("Innlogging feilet");
                    HttpContext.Session.SetString(_loggetInn, "");
                    return Ok(false);
                }
                HttpContext.Session.SetString(_loggetInn, "logget inn");
                return Ok(true);
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public void LoggUt()
        {
            HttpContext.Session.SetString(_loggetInn, "");
        }
    }
}

//hentAlle på all repo
