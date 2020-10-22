using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Gruppeoppgave1.DAL;
using Gruppeoppgave1.DAL.IRepositories;
using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Mvc;
using Gruppeoppgave1.DAL.Repositories;
using Microsoft.AspNetCore.Http;

namespace Gruppeoppgave1.Controllers
{
    [Route("stasjoner/")]
    public class StasjonController : ControllerBase
    {
        private readonly IStasjonRepository _db;
        private ILogger<StasjonController> _log;

        public StasjonController(IStasjonRepository db, ILogger<StasjonController> log)
        {
            _log = log;
            _db = db;
        }

        [Route("hentAlleStasjoner")]
        public async Task<ActionResult> HentAlleStasjoner()
        {
            List<Stasjon> liste =  await _db.HentAlleStasjoner();
            return Ok(liste);
        }

        [Route("hentEnStasjon")]
        public async Task<ActionResult> HentEnStasjon(int id)
        {
            var stasjon = await _db.HentEnStasjon(id);
            if(stasjon == null)
            {
                _log.LogError("Fant ikke stasjonen");
                return NotFound("Fant ikke stasjonen");
            }
            return Ok(stasjon);
        }


        [Route("fjernStasjon")]
        public async Task<ActionResult> FjernStasjon(int id)
        {
            /* if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }*/
            bool ok = await _db.FjernStasjon(id);
            if (!ok)
            {
                _log.LogError("Kunne ikke fjerne stasjonen");
                return BadRequest("Kunne ikke slette stasjonen");
            }
            _log.LogInformation("Stasjonen ble fjernet");
            return Ok("Stasjonen ble fjernet");

        }

        [Route("endreStasjon")]
        public async Task<ActionResult> EndreStasjon(Stasjon stasjon)
        {
            /* if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }*/
            if (ModelState.IsValid)
            {
                bool ok =  await _db.EndreStasjon(stasjon);
                if (!ok)
                {
                    _log.LogError("Kunne ikke endre stasjonen");
                    return BadRequest("Kunne ikke endre stasjon!");
                }
                _log.LogInformation("Stasjonen " + stasjon.StasjonsNavn+" ble endret på");
                return Ok("Stasjonen "+ stasjon.StasjonsNavn +" ble endret");
            }
            _log.LogError("Ikke gyldig stasjon");
            return BadRequest("Ikke gyldig Stasjon");
        }
    }
}
