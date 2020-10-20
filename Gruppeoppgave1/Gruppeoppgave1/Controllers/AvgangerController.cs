using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gruppeoppgave1.DAL;
using Gruppeoppgave1.DAL.IRepositories;
using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gruppeoppgave1.Controllers
{
    [Route("avganger/")]
    public class AvgangerController : ControllerBase
    {
        private readonly IAvgangerRepository _db;

        public AvgangerController(IAvgangerRepository db)
        {
            _db = db;
        }

        [Route("leggTilAvgang")]
        public async Task<ActionResult> LeggTil(Avgang avgang)
        {
            if (ModelState.IsValid)
            {
                bool resultat = await _db.LeggTil(avgang);
                if (!resultat)
                {
                    return BadRequest("Kunne ikke legge til avgang");
                }
                return Ok("Avgangen ble lagt til");
            }
            return BadRequest("Avgangsobjektet er feil");
            
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

            Avgang avgang = await _db.HentEn(id);
            if(avgang == null)
            {
                return NotFound("Fant ikke avgangen");
            }
            return Ok(avgang);
        }

        [Route("endreAvgang")]
        public async Task<ActionResult> Endre(Avgang avgang)
        {
            if (ModelState.IsValid)
            {
                bool ok = await _db.Endre(avgang);
                if (!ok)
                {
                    return BadRequest("Kunne ikke endre på avgangen");
                }
                return Ok("Avgangen ble endret");
            }
            return BadRequest("Avgangen er feil");
            
        }

        [Route("slettAvgang")]
        public async Task<ActionResult> Slett(int id)
        {
            bool ok =  await _db.Slett(id);
            if (!ok)
            {
                return BadRequest("Kunne ikke slette avgangen");
            }
            return Ok("Avgangen ble slettet");
        }
    }

}

