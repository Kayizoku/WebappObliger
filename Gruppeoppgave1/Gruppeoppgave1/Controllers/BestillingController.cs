using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gruppeoppgave1.Model;
using Gruppeoppgave1.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Gruppeoppgave1.Controllers
{
    [Route("[controller]/[action]")]
    public class BestillingController : ControllerBase
    {
        private readonly DbContext _db;



        public List<Bestillinger> HentAlle()
        {

        }
    }
}
