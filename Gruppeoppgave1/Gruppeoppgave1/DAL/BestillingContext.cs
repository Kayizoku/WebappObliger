using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Gruppeoppgave1.Model
{
    public class Bestillinger 
    {
        public int Id { get; set; }
        virtual public double Pris { get; set; }
        public string Dato { get; set; }
        virtual public Avganger Avgang { get; set; }
    }

    public class Avganger
    {
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        virtual public Stasjoner Fra { get; set; }
        virtual public Stasjoner Til { get; set; }
        public string Tid { get; set; }
        virtual public List<Bestillinger> Bestillinger { get; set; }

    }

    public class Stasjoner
    {
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string StasjonsNavn { get; set;  }
        public int NummerPaaStopp { get; set; } //vet ikke om dette skal med enda
        virtual public List<Avganger> Avganger { get; set; }

    }

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // legg til "virtual" på de attriuttene som skal lastes automatisk 
            optionsBuilder.UseLazyLoadingProxies();
        }

    }






}
