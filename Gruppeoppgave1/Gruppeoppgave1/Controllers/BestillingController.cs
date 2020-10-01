using System.Collections.Generic;
using System.Threading.Tasks;
using Gruppeoppgave1.DAL;
using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gruppeoppgave1.Controller
{
    [Route("[controller]/[action]")]
    public class BestillingController : ControllerBase
    {
        private readonly IBestillingRepository _db;

        public BestillingController(IBestillingRepository db)
        {
            _db = db;
        }

        
        public async Task<bool> lagre(Bestilling innBestilling)
        {
            return await _db.lagre(innBestilling);
        }

        
        public async Task<List<Bestilling>> HentAlle()
        {
            return await _db.HentAlle();
        }

        public async Task<Bestilling> HentEn(int id)
        {
            return await _db.HentEn(id);
        }
    }
}