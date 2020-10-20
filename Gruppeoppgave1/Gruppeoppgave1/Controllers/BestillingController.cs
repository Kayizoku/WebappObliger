using System;
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
    [Route("[controller]/[action]")]
    public class BestillingController : ControllerBase
    {
        private readonly IBestillingRepository _db;

        private const string _innlogget = "innlogget";

        private ILogger<BestillingController> _log;

        public BestillingController(IBestillingRepository db)
        {
            _db = db;
        }

        public async Task<ActionResult> Lagre(Bestilling innBestilling)
        {
            if (ModelState.IsValid)
            {
                bool ok = await _db.Lagre(innBestilling);
                if (!ok)
                {
                    return BadRequest("Kunne ikke lagre");
                }
                return Ok("Bestillingen ble lagret");
            }
            return BadRequest("Bestillingen er ikke riktig");
        }

        [Route("hentAlleBestillinger")]
        public async Task<ActionResult> HentAlle()
        {
            List<Bestilling> bestillinger = await _db.HentAlle();
            return Ok(bestillinger);
        }

        [Route("hentEnBestilling")]
        public async Task<ActionResult> HentEn(int id)
        {
            Bestilling bestilling =  await _db.HentEn(id);
            if(bestilling == null)
            {
                return NotFound("Bestillingen ble ikke funnet");
            }
            return Ok(bestilling);
        }

        [Route("slettEnBestilling")]
        public async Task<ActionResult> Slett(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_innlogget)))
            {
                return Unauthorized();
            }

            bool ok = await _db.Slett(id);
            if (!ok)
            {
                return BadRequest("Kunne ikke slette bestillingen");
            }
            return Ok("Bestillingen ble slettet");
        }

        public async Task<ActionResult> Endre(Bestilling innBestilling)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_innlogget)))
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                bool ok = await _db.Endre(innBestilling);
                if (!ok)
                {
                    return BadRequest("Kunne ikke endre bestillingen");
                }
            }
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
                Currency = "NOK",
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

        public async Task<ActionResult> LoggInn(Bruker bruker)
        {
            if (ModelState.IsValid)
            {
                bool OK = await _db.LoggInn(bruker);
                if (!OK)
                {
                   // _log.LogInformation("Innloggingen feilet for bruker" + bruker.Brukernavn);
                    HttpContext.Session.SetString(_innlogget, "");
                    return Ok(false);
                }
                HttpContext.Session.SetString(_innlogget, "innlogget");
                return Ok(true);
            }
           // _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public void LoggUt()
        {
            HttpContext.Session.SetString(_innlogget, "");
        }
    }
}
