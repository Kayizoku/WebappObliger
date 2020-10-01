using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gruppeoppgave1.Model
{
    public static class DBInit
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<BestillingContext>();

                // m? slette og opprette databasen hver gang n?r den skalinitieres (seed`es)
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var stasjon1 = new Stasjoner { StasjonsNavn = "Oslo", NummerPaaStopp = int.Parse("1") };
                var stasjon2 = new Stasjoner { StasjonsNavn = "Drammen", NummerPaaStopp = int.Parse("2") };
                var stasjon3 = new Stasjoner { StasjonsNavn = "Horten", NummerPaaStopp = int.Parse("3") };

                var avgang1 = new Avganger {
                    Fra = stasjon1,
                    Til = stasjon2,
                    Tid = "07:00"
                };
                var avgang2 = new Avganger
                {
                    Fra = stasjon2,
                    Til = stasjon3,
                    Tid = "08:00"
                };

                var bestilling1 = new Bestillinger { Fra = "Oslo", Til = "Drammen", Tid = "07:00", Pris = double.Parse("50"), Dato = "01.01.2021" };
                var bestilling2 = new Bestillinger { Fra = "Drammen", Til = "Horten", Tid = "08:00", Pris = double.Parse("50"), Dato = "02.01.2021" };

                context.Stasjoner.Add(stasjon1);
                context.Stasjoner.Add(stasjon2);
                context.Stasjoner.Add(stasjon3);
                context.Avganger.Add(avgang1);
                context.Avganger.Add(avgang2);
                context.Bestillinger.Add(bestilling1);
                context.Bestillinger.Add(bestilling2);

                context.SaveChanges();
            }
        }
    }

}
