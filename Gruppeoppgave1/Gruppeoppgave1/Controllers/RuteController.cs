using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gruppeoppgave1.Controller;
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
       
        public RuteController(IRuteRepository db)
        {
            _db = db;
        }


        [Route("LeggTilRute")]
        public async Task<bool> LeggTilRute(Rute rute)
        {
            /* if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }*/
            return await _db.LeggTilRute(rute);
        }

        [Route("EndreRute")]
        public async Task<bool> EndreRute(Rute rute)
        {
            /* if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }*/
            return await _db.EndreRute(rute);
        }

        [Route("SlettRute")]
        public async Task<bool> SlettRute(int id)
        {
            /* if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }*/
            return await _db.SlettRute(id);
        }

        [Route("HentAlleRuter")]
        public async Task<List<Rute>> HentAlleRuter()
        {
            return await _db.HentAlleRuter();
        }

        [Route("HenEnRute")]
        public async Task<Rute> HentEnRute(int id)
        {
            return await _db.HentEnRute(id);
        }
    }
}
