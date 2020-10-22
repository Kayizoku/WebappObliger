using Gruppeoppgave1.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Gruppeoppgave1.DAL
{
    [ExcludeFromCodeCoverage]
    public class Bestillinger 
    {
        [Key]
        public int Id { get; set; }
        public string Fra { get; set; }
        public string Til { get; set; }
        public string Tid { get; set; }
        virtual public double Pris { get; set; }
        public string Dato { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Avganger
    {
        [Key]
        public int Id { get; set; }
        virtual public string Fra { get; set; }
        virtual public string Til { get; set; }
        public string Tid { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Stasjoner
    {
        [Key]
        public int Id { get; set; }
        public string StasjonsNavn { get; set;  }
        public int NummerPaaStopp { get; set; } 
    }

    [ExcludeFromCodeCoverage]
    public class Ruter
    {
        [Key]
        public int Id { get; set; }
        public string Navn { get; set; }
        virtual public List<Stasjon> StasjonerPaaRute { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Brukere
    {
        [Key]
        public int Id { get; set; }
        public string Brukernavn { get; set; }
        public byte[] Passord { get; set; }
        public byte[] Salt { get; set; }
    }


    [ExcludeFromCodeCoverage]
    public class BestillingContext : DbContext
    {
        public BestillingContext(DbContextOptions<BestillingContext> options)
                : base(options)
        {
            //  for å opprette databasen fysisk hvis den ikke allerede er det
            Database.EnsureCreated();
        }

        public DbSet<Bestillinger> Bestillinger { get; set; }
        public DbSet<Avganger> Avganger { get; set; }
        public DbSet<Stasjoner> Stasjoner { get; set; }
        public DbSet<Brukere> Brukere { get; set; }
        public DbSet<Ruter> Ruter { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // legg til "virtual" på de attriuttene som skal lastes automatisk 
            optionsBuilder.UseLazyLoadingProxies();
        }

    }






}
