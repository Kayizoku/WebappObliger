using Gruppeoppgave1.DAL.IRepositories;
using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gruppeoppgave1.Controllers
{
    [Route("stasjoner/")]
    public class StasjonController : ControllerBase
    {
        private readonly IStasjonRepository _db;
        private ILogger<StasjonController> _log;

        private const string _loggetInn = "logget inn";


        public StasjonController(IStasjonRepository db, ILogger<StasjonController> log)
        {
            _log = log;
            _db = db;
        }

        [Route("lagreStasjon")]
        public async Task<ActionResult> LagreStasjon(Stasjon stasjon)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool ok = await _db.LagreStasjon(stasjon);
                if (!ok)
                {
                    _log.LogInformation("Kunne ikke legge til stasjon");
                    return BadRequest("Kunne ikke legge til stasjon");
                }
                _log.LogInformation("Stasjonen ble lagt til");
                return Ok("Stasjonen ble lagt til");
            }
            _log.LogInformation("Feil i inputvalidering på stasjon");
            return BadRequest("Stasjonen mangler felt");
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
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("ikke logget inn");
            }

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
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("ikke logget inn");
            }

            bool ok = await _db.FjernStasjon(id);
            if (!ok)
            {
                _log.LogError("Kunne ikke fjerne stasjonen");
                return NotFound("Kunne ikke slette stasjonen");
            }
            _log.LogInformation("Stasjonen ble fjernet");
            return Ok("Stasjonen ble fjernet");

        }

        [Route("endreStasjon")]
        public async Task<ActionResult> EndreStasjon(Stasjon stasjon)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("ikke logget inn");
            }

            if (ModelState.IsValid)
            {
                bool ok =  await _db.EndreStasjon(stasjon);
                if (!ok)
                {
                    _log.LogError("Kunne ikke endre stasjonen");
                    return NotFound("Kunne ikke endre stasjon!");
                }
                _log.LogInformation("Stasjonen ble endret på");
                return Ok("Stasjonen ble endret");
            }
            _log.LogError("Ikke gyldig stasjon");
            return BadRequest("Ikke gyldig Stasjon");
        }
    }
}
