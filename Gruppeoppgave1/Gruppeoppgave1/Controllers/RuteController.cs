using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Gruppeoppgave1.DAL.IRepositories;
using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gruppeoppgave1.Controllers

{
    [Route("rute/")]
    public class RuteController :ControllerBase
    {
        private readonly IRuteRepository _db;
        private ILogger<RuteController> _log;

        private const string _loggetInn = "logget inn";

        public RuteController(IRuteRepository db, ILogger<RuteController> log)
        {
            _log = log;
            _db = db;
        }


        [Route("LeggTilRute")]
        public async Task<ActionResult> LeggTilRute(Rute rute)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            if (ModelState.IsValid)
            {
                bool leggTilOK = await _db.LeggTilRute(rute);
                if (!leggTilOK)
                {
                    _log.LogError("Kunne ikke legge til rute");
                    return NotFound("Kunne ikke legge til rute");
                }
                _log.LogInformation("Ruten ble lagt til");
                return Ok("Ruten ble lagt til");
            }
            _log.LogInformation("Ruteobjektet er ikke validert");
            return BadRequest("Ruteobjektet er ikke validert");
        }

        [Route("EndreRute")]
        public async Task<ActionResult> EndreRute(Rute rute)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            if (ModelState.IsValid) {
               bool ruteOK = await _db.EndreRute(rute);
               if (!ruteOK)
               {
                 _log.LogError("Kunne ikke endre ruten");
                 return NotFound("Kunne ikke endre ruten");
               }
               _log.LogInformation("Ruten ble endret");
               return Ok("Ruten ble endret");
               }
             _log.LogError("Ruteobjektet er ikke validert");
             return BadRequest("Ruteobjektet er ikke validert");
        }

        [Route("slettRute")]
        public async Task<ActionResult> SlettRute(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            bool slettOK = await _db.SlettRute(id);
            if (!slettOK)
            {
                _log.LogError("Kunne ikke slette ruten");
                return NotFound("Kunne ikke slette ruten");
            }
            _log.LogInformation("Ruten ble slettet");
            return Ok("Ruten ble slettet");
        }

        [Route("HentAlleRuter")]
        public async Task<ActionResult> HentAlleRuter()
        {

            List<Rute> alleruter = await _db.HentAlleRuter();
            return Ok(alleruter);
        }

        [Route("HentEnRute")]
        public async Task<ActionResult> HentEnRute(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            Rute ruten = await _db.HentEnRute(id);
            if (ruten == null)
            {
                _log.LogError("Fant ikke ruten med id ");
                return NotFound("Ruten ble ikke funnet");
            }
            _log.LogInformation("Bestillingen med id ble funnet");
            return Ok(ruten);
        }
    }
}
