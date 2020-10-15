using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gruppeoppgave1.DAL;
using Gruppeoppgave1.DAL.IRepositories;
using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Mvc;

namespace Gruppeoppgave1.Controllers
{
    [Route("stasjoner/")]
    public class StasjonController : ControllerBase
    {
        private readonly IStasjonRepository _db;

        public StasjonController(IStasjonRepository db)
        {
            _db = db;
        }

        [Route("hentAlleStasjoner")]
        public async Task<List<Stasjon>> HentAlleStasjoner()
        {
            return await _db.HentAlleStasjoner();
        }

        [Route("hentEnStasjon")]
        public async Task<Stasjon> HentEnStasjon(int id)
        {
            return await _db.HentEnStasjon(id);
        }


        [Route("fjernStasjon")]
        public async Task<ActionResult> FjernStasjon(int id)
        {
                bool ok = await _db.FjernStasjon(id);
                if (!ok)
                {
                    return BadRequest("Kunne ikke slette stasjonen");
                }
                return Ok("Stasjonen ble fjernet");

        }

        [Route("endreStasjon")]
        public async Task<ActionResult> EndreStasjon(Stasjon stasjon)
        {
            if (ModelState.IsValid)
            {
                bool ok =  await _db.EndreStasjon(stasjon);
                if (!ok)
                {
                    return BadRequest("Kunne ikke endre stasjon!");
                }
                return Ok("Stasjonen ble endret");
            }
            return BadRequest("Ikke gyldig Stasjon");
        }
    }
}
