using Castle.Components.DictionaryAdapter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gruppeoppgave1.Model
{
    public class Bestillinger 
    {
        public int Id { get; set; }
        virtual public Avganger Avgang { get; set; }
        virtual public Priser Pris { get; set; }

    }

    public class Avganger
    {
        public int Id { get; set; }
        virtual public Stasjoner Fra { get; set; }
        virtual public Stasjoner Til { get; set; }
        public string Dato { get; set; }
        public string Tid { get; set; }
        virtual public List<Bestillinger> Bestillinger { get; set; }

    }

    public class Stasjoner
    {
        public int Id { get; set; }
        public string Navn { get; set;  }
        public int NummerPaaStopp { get; set; } //vet ikke om dette skal med enda
        virtual public List<Avganger> Avganger { get; set; }

    }

    public class Priser
    {
        public int Id { get; set; }
        public string Pristype { get; set; } //ikke sikker på om dette skal med enda, eller om må endres på
        public double Pris { get; set; }
        virtual public List<Bestillinger> Bestillinger { get; set; }
    }

    public class BestillingContext : DbContext
    {
        public BestillingContext(DbContextOptions<BestillingContext> options)
                : base(options)
        {
            //  for å opprette databasen fysisk dersom den ikke er opprettet
            Database.EnsureCreated();
        }

        public DbSet<Bestillinger> Bestillinger { get; set; }
        public DbSet<Avganger> Avganger { get; set; }
        public DbSet<Stasjoner> Stasjoner { get; set; }
        public DbSet<Priser> Priser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // må importere  Microsoft.EntityFrameworkCore.Proxies
            // og legge til"virtual" på de attriuttene som ønskes å lastes automatisk (LazyLoading)
            optionsBuilder.UseLazyLoadingProxies();
        }

    }






}
