using Gruppeoppgave1.DAL.IRepositories;
using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Gruppeoppgave1.Controllers
{
    [Route("bruker/")]
    public class BrukerController: ControllerBase
    {


        private readonly IBrukerRepository _db;
        private ILogger<BrukerController> _log;
        private const string _loggetInn = "logget inn";

        public BrukerController(IBrukerRepository db, ILogger<BrukerController> log)
        {
            _log = log;
            _db = db;
        }

        [Route("loggInn")]
        public async Task<ActionResult> LoggInn(Bruker bruker)
        {
            if (ModelState.IsValid)
            {
                bool OK = await _db.LoggInn(bruker);
                if (!OK)
                {
                    try
                    {
                        _log.LogInformation("Innloggingen feilet for bruker" + bruker.Brukernavn);
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e.Message);
                    }
                    HttpContext.Session.SetString(_loggetInn, "");
                    return Ok(false);
                }
                HttpContext.Session.SetString(_loggetInn, "innlogget");
                return Ok(true);
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public void LoggUt()
        {
            HttpContext.Session.SetString(_loggetInn, "");
        }
    }
}
    }
}
