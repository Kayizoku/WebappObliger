using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gruppeoppgave1.DAL;
using Gruppeoppgave1.Model;
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

        [Route("hentAlleAvganger")]
        public async Task<List<Avgang>> HentAlle()
        {
            return await _db.HentAlle();
        }

        [Route("hentEnAvgang")]
        public async Task<Avgang> HentEn(int id)
        {
            return await _db.HentEn(id);
        }
    }
}
