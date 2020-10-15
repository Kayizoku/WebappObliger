using Gruppeoppgave1.DAL.IRepositories;
using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Gruppeoppgave1.Controllers
{
    [Route("bruker/")]
    public class BrukerController
    {

        private readonly IBrukerRepository _db;

        public BrukerController(IBrukerRepository db)
        {
            _db = db;
        }


        [Route("LeggTilBruker")]
        public async Task<bool> LeggTilBruker(Bruker bruker)
        {
            return await _db.LeggTilBruker(bruker);
        }

        [Route("SjekkBruker")]
        public async Task<bool> SjekkBruker(Bruker bruker)
        {
            return await _db.SjekkBruker(bruker);
        }
    }
}
