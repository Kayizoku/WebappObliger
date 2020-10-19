using System.Collections.Generic;
using System.Threading.Tasks;
using Gruppeoppgave1.DAL;
using Gruppeoppgave1.DAL.IRepositories;
using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stripe;

namespace Gruppeoppgave1.Controller
{
    [Route("bestillinger/")]
    public class BestillingController : ControllerBase
    {
        private readonly IBestillingRepository _db;
        private ILogger<BestillingController> _log;

        private const string _loggetInn = "loggetInn";

        public BestillingController(IBestillingRepository db, ILogger<BestillingController> log)
        {
            _log = log;
            _db = db;
        }

        [Route("lagreBestilling")]
        public async Task<ActionResult> Lagre(Bestilling innBestilling)
        {
            if (ModelState.IsValid)
            {
                bool ok = await _db.Lagre(innBestilling);
                if (!ok)
                {
                    _log.LogError("Kunne ikke lagre bestillingen");
                    return BadRequest("Kunne ikke lagre");
                }
                _log.LogInformation("Bestillingen ble lagret");
                return Ok("Bestillingen ble lagret");
            }
            _log.LogError("Feil i inputvalideringen");
            return BadRequest("Bestillingen er ikke riktig");
        }

        [Route("hentAlleBestillinger")]
        public async Task<ActionResult> HentAlle()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            List<Bestilling> bestillinger = await _db.HentAlle();
            return Ok(bestillinger);
        }

        [Route("hentEnBestilling")]
        public async Task<ActionResult> HentEn(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            Bestilling bestilling =  await _db.HentEn(id);
            if(bestilling == null)
            {
                _log.LogError("Fant ikke bestillingen");
                return NotFound("Bestillingen ble ikke funnet");
            }
            _log.LogInformation("Bestillingen ble funnet");
            return Ok(bestilling);
        }

        [Route("slettEnBestilling")]
        public async Task<ActionResult> Slett(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            bool ok = await _db.Slett(id);
            if (!ok)
            {
                _log.LogInformation("Kunne ikke slette bestillingen");
                return BadRequest("Kunne ikke slette bestillingen");
            }
            _log.LogInformation("Bestillingen ble slettet");
            return Ok("Bestillingen ble slettet");
        }

        [Route("endreEnBestilling")]
        public async Task<ActionResult> Endre(Bestilling innBestilling)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool ok = await _db.Endre(innBestilling);
                if (!ok)
                {
                    _log.LogError("Kunne ikke endre bestillingen");
                    return BadRequest("Kunne ikke endre bestillingen");
                }
                return Ok("Bestillingen ble endret");
            }
            _log.LogInformation("Feil i inputvalideringen");
            return BadRequest("Bestillingen mangler felt");
        }


        public bool Charge(string stripeEmail, string stripeToken)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();
            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });
            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = 500,
                Description = "Sample Charge",
                Currency = "nok",
                Customer = customer.Id
            });
            return charge.Paid;
        }

        public IActionResult Index()
        {
            return RedirectToPage("/");
        }
        public IActionResult Error()
        {
            return RedirectToAction("/");
        }
    }
}
