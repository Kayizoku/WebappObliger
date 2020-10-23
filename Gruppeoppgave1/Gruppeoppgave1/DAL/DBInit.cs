using Gruppeoppgave1.DAL.Repositories;
using Gruppeoppgave1.Model;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Gruppeoppgave1.DAL
{
    [ExcludeFromCodeCoverage]
    public static class DBInit
    {
        public static void Initialize(IServiceScope serviceScope)
        {
            var context = serviceScope.ServiceProvider.GetService<BestillingContext>();

            // må slette og opprette databasen hver gang n?r den skalinitieres (seed`es)
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();




            var stasjonerList = new List<Stasjoner>
            {
                new Stasjoner { StasjonsNavn = "Oslo", NummerPaaStopp = int.Parse("1") },
                new Stasjoner { StasjonsNavn = "Sandvika", NummerPaaStopp = int.Parse("2") },
                new Stasjoner { StasjonsNavn = "Asker", NummerPaaStopp = int.Parse("3") },
                new Stasjoner { StasjonsNavn = "Drammen", NummerPaaStopp = int.Parse("4") },
                new Stasjoner { StasjonsNavn = "Skoger", NummerPaaStopp = int.Parse("5") },
                new Stasjoner { StasjonsNavn = "Sande", NummerPaaStopp = int.Parse("6") },
                new Stasjoner { StasjonsNavn = "Holmestrand", NummerPaaStopp = int.Parse("7") },
                new Stasjoner { StasjonsNavn = "Kopstadkrysset", NummerPaaStopp = int.Parse("8") },
                new Stasjoner { StasjonsNavn = "Horten", NummerPaaStopp = int.Parse("9") }
            };

            var avgang1 = new Avganger
            {
                Fra = stasjonerList[0].StasjonsNavn,
                Til = stasjonerList[1].StasjonsNavn,
                Tid = "07:00"
            };
            var avgang2 = new Avganger
            {
                Fra = stasjonerList[3].StasjonsNavn,
                Til = stasjonerList[4].StasjonsNavn,
                Tid = "08:00"
            };

            var avgang3 = new Avganger
            {
                Fra = stasjonerList[5].StasjonsNavn,
                Til = stasjonerList[6].StasjonsNavn,
                Tid = "11:00"
            };
            var avgang4 = new Avganger
            {
                Fra = stasjonerList[7].StasjonsNavn,
                Til = stasjonerList[8].StasjonsNavn,
                Tid = "12:00"
            };

            var avgang5 = new Avganger
            {
                Fra = stasjonerList[0].StasjonsNavn,
                Til = stasjonerList[1].StasjonsNavn,
                Tid = "14:00"
            };
            var avgang6 = new Avganger
            {
                Fra = stasjonerList[3].StasjonsNavn,
                Til = stasjonerList[4].StasjonsNavn,
                Tid = "15:00"
            };

            var avgang7 = new Avganger
            {
                Fra = stasjonerList[5].StasjonsNavn,
                Til = stasjonerList[6].StasjonsNavn,
                Tid = "18:00"
            };
            var avgang8 = new Avganger
            {
                Fra = stasjonerList[7].StasjonsNavn,
                Til = stasjonerList[8].StasjonsNavn,
                Tid = "21:00"
            };

            var bestilling1 = new Bestillinger { Fra = "Oslo", Til = "Drammen", Tid = "07:00", Pris = double.Parse("50"), Dato = "2021-01-13" };
            var bestilling2 = new Bestillinger { Fra = "Drammen", Til = "Horten", Tid = "08:00", Pris = double.Parse("50"), Dato = "2021-02-20" };

            foreach (Stasjoner s in stasjonerList)
            {
                context.Stasjoner.Add(s);
            }
            /*
            context.Stasjoner.Add(stasjon1);
            context.Stasjoner.Add(stasjon2);
            context.Stasjoner.Add(stasjon3);
            */
            context.Avganger.Add(avgang1);
            context.Avganger.Add(avgang2);
            context.Bestillinger.Add(bestilling1);
            context.Bestillinger.Add(bestilling2);

            var db = serviceScope.ServiceProvider.GetService<BestillingContext>();

            var adminBruker = new Brukere();

            adminBruker.Brukernavn = "Admin1";
            string passord = "Passord1";

            
            byte[] salt = BrukerRepository.Salt();
            byte[] hash = BrukerRepository.Hash(passord, salt);

            adminBruker.Passord = hash;
            adminBruker.Salt = salt;

            db.Brukere.Add(adminBruker);
            

            context.SaveChanges();

        }
    }

}
