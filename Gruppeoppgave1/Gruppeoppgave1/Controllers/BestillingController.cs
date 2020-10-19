using System.Collections.Generic;
using System.Threading.Tasks;
using Gruppeoppgave1.DAL;
using Gruppeoppgave1.DAL.IRepositories;
using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace Gruppeoppgave1.Controller
{
    [Route("bestillinger/")]
    public class BestillingController : ControllerBase
    {
        private readonly IBestillingRepository _db;

        public BestillingController(IBestillingRepository db)
        {
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
            bool ok = await _db.Slett(id);
            if (!ok)
            {
                return BadRequest("Kunne ikke slette bestillingen");
            }
            return Ok("Bestillingen ble slettet");
        }

        [Route("endreEnBestilling")]
        public async Task<ActionResult> Endre(Bestilling innBestilling)
        {
            if(ModelState.IsValid)
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
    }
}
