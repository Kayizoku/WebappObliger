using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gruppeoppgave1.DAL;
using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Mvc;

namespace Gruppeoppgave1.Controllers
{
    [Route("[controller]/[action]")]
    public class StasjonController : ControllerBase
    {
        private readonly IStasjonRepository _db;

        public StasjonController(IStasjonRepository db)
        {
            _db = db;
        }

        public async Task<List<Stasjon>> HentAlle()
        {
            return await _db.HentAlle();
        }
    }
}
