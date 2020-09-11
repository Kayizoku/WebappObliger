using Audit.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Model.DBModels;

namespace Data.Access.Layer
{
    public class DbContext : DbContext
    {
        public DbContext(DbContextOptions<DbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<DbAvgang> Avganger Tickets { get; set; }
        public DbSet<DbBillett> Billetter { get; set; }
        public DbSet<DbBillettype> Billettyper { get; set; }
        public DbSet<DbRute> Ruter { get; set; }
        public DbSet<DbStasjon> Stasjoner { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            optionsBuilder.UseLazyLoadingProxies();
        }
    }   
}