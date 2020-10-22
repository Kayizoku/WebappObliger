﻿using Gruppeoppgave1.DAL.IRepositories;
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
        private readonly IAvgangerRepository _dbA;
        private readonly IBestillingRepository _dbB;
        private readonly IRuteRepository _dbR;
        private readonly IStasjonRepository _dbS;

        private ILogger<BrukerController> _log;
        private const string _loggetInn = "logget inn";

        public BrukerController(IBrukerRepository db, IAvgangerRepository dbA, IBestillingRepository dbB,
            IRuteRepository dbR, IStasjonRepository dbS, ILogger<BrukerController> log)
        {
            _log = log;
            _db = db;
            _dbA = dbA;
            _dbB = dbB;
            _dbR = dbR;
            _dbS = dbS;
        }

        [Route("hentAlleAvgangerAdmin")]
        public async Task<ActionResult> HentAlleAvgangerAdmin()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            List<Avgang> alleAvganger = await _dbA.HentAlle();
            return Ok(alleAvganger);
        }

        [Route("hentAlleBestillingerAdmin")]
        public async Task<ActionResult> HentAlleBestillingerAdmin()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            List<Bestilling> alleBestillinger = await _dbB.HentAlle();
            return Ok(alleBestillinger);
        }

        [Route("hentAlleRuterAdmin")]
        public async Task<ActionResult> HentAlleRuterAdmin()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            List<Rute> alleRuter = await _dbR.HentAlleRuter();
            return Ok(alleRuter);
        }

        [Route("hentAlleStasjonerAdmin")]
        public async Task<ActionResult> HentAlleStasjonerAdmin()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            List<Stasjon> alleStasjoner = await _dbS.HentAlleStasjoner();
            return Ok(alleStasjoner);
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

//hentAlle på all repo
