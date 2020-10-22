using Gruppeoppgave1.DAL.IRepositories;
using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Gruppeoppgave1.Controllers
{
    [Route("bruker/")]
    public class BrukerController:ControllerBase
    {

        private readonly IBrukerRepository _db;
        private ILogger<BrukerController> _log;
        private const string _loggetInn = "loggetInn";

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
                    //_log.LogInformation feiler for It.IsAny<Bruker>(), nullReferenceException
                    _log.LogInformation("Innloggingen feilet for bruker");
                    HttpContext.Session.SetString(_loggetInn, "");
                    return Ok(false);
                }
                //_log.LogInformation("Bruker " + bruker.Brukernavn + " ble logget inn");
                //_log.LogInformation feiler for It.IsAny<Bruker>(), nullReferenceException
                HttpContext.Session.SetString(_loggetInn, "loggetInn");
                return Ok(true);
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        [Route("loggUt")]
        public void LoggUt()
        {
            _log.LogInformation("logget ut");
            HttpContext.Session.SetString(_loggetInn, "");
        }

        
    }
}
