using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gruppeoppgave1.DAL;
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
        public async Task<bool> FjernStasjon(int id)
        {
            return await _db.FjernStasjon(id);
        }

        [Route("endreStasjon")]
        public async Task<bool> EndreStasjon(Stasjon stasjon)
        {
            return await _db.EndreStasjon(stasjon);
        }
    }
}
