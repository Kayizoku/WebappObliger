﻿using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gruppeoppgave1.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Gruppeoppgave1.DAL.IRepositories;
using Microsoft.Extensions.Logging;
using System;

namespace Gruppeoppgave1.DAL.Repositories
{
    public class StasjonRepository : IStasjonRepository
    {
        private readonly BestillingContext _db;
        private ILogger<StasjonRepository> _log;

        public StasjonRepository(BestillingContext db, ILogger<StasjonRepository> log)
        {
            _log = log;
            _db = db;
        }

        public async Task<List<Stasjon>> HentAlleStasjoner()
        {
            try
            {
                List<Stasjon> alleStasjoner = await _db.Stasjoner.Select(s => new Stasjon
                {
                    Id = s.Id,
                    NummerPaaStopp = s.NummerPaaStopp,
                    StasjonsNavn = s.StasjonsNavn,

                }).ToListAsync();
                return alleStasjoner;
            }
            catch (Exception e)
            {
                _log.LogError(e.Message);
                return null;
            }
        }

        public async Task<Stasjon> HentEnStasjon(int id)
        { 
            try
            {
                Stasjoner enStasjon = await _db.Stasjoner.FindAsync(id);
                var hentetStasjon = new Stasjon()
                {
                    Id = enStasjon.Id,
                    NummerPaaStopp = enStasjon.NummerPaaStopp,
                    StasjonsNavn = enStasjon.StasjonsNavn

                };
                return hentetStasjon;
            }
            catch (Exception e)
            {
                _log.LogError(e.Message);
                return null;
            }
                
        }

        

        public async Task<bool> EndreStasjon(Stasjon stasjon)
        {
            try
            {
                var gammelStasjon = await _db.Stasjoner.FindAsync(stasjon.Id);
                gammelStasjon.NummerPaaStopp = stasjon.NummerPaaStopp;
                gammelStasjon.StasjonsNavn = stasjon.StasjonsNavn;
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.LogError(e.Message);
                return false;
            }
            return true;

        }

        public async Task<bool> FjernStasjon(int id)
        {
            try
            {
                var fjernetStasjon = await _db.Stasjoner.FindAsync(id);
                _db.Stasjoner.Remove(fjernetStasjon);
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.LogError(e.Message);
                return false;
            }
            return true;
        }

        public async Task<bool> LagreStasjon(Stasjon stasjon)
        {
            try
            {
                var nyStasjon = new Stasjoner();
                nyStasjon.NummerPaaStopp = stasjon.NummerPaaStopp;
                nyStasjon.StasjonsNavn = stasjon.StasjonsNavn;
                _db.Stasjoner.Add(nyStasjon);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
    }

