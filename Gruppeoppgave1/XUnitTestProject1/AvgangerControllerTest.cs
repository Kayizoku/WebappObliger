using Gruppeoppgave1.Controllers;
using Gruppeoppgave1.DAL.IRepositories;
using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1
{
    public class AvgangerControllerTest
    {
        private readonly Mock<IAvgangerRepository> mockRep = new Mock<IAvgangerRepository>();

        [Fact]
        public async Task LeggTilAvgangTrue()
        {
            var avgang = new Avgang
            {
                Id = 1,
                Fra = "Nordstrand",
                Til = "Oslo",
                Tid = "11:00"
            };

            
            mockRep.Setup(l => l.LeggTil(avgang)).ReturnsAsync(true);

            var avgangController = new AvgangerController(mockRep.Object);
            var resultat = await avgangController.LeggTil(avgang);

            Assert.IsType<OkObjectResult>(resultat);
        }

        [Fact]
        public async Task LeggTilAvgangFalse()
        {
            var avgang = new Avgang
            {
                Id = 1,
                Fra = "Nordstrand",
                Til = "Oslo",
                Tid = "11:00"
            };

            
            mockRep.Setup(l => l.LeggTil(avgang)).ReturnsAsync(false);

            var avgangController = new AvgangerController(mockRep.Object);
            var resultat = await avgangController.LeggTil(avgang);

            Assert.IsType<BadRequestResult>(resultat);
        }

        [Fact]
        public async Task EndreAvgangTrue()
        {
            var avgang = new Avgang
            {
                Id = 1,
                Fra = "Horten",
                Til = "Drammen",
                Tid = "11:00"
            };

            
            mockRep.Setup(l => l.Endre(avgang)).ReturnsAsync(true);

            var avgangController = new AvgangerController(mockRep.Object);
            var resultat = await avgangController.Endre(avgang);

            Assert.IsType<OkObjectResult>(resultat);
        }

        [Fact]
        public async Task EndreAvgangFalse()
        {
            var avgang = new Avgang
            {
                Id = 1,
                Fra = "Oslo",
                Til = "Nordstrand",
                Tid = "14:00"
            };

            
            mockRep.Setup(l => l.Endre(avgang)).ReturnsAsync(false);

            var avgangController = new AvgangerController(mockRep.Object);
            var resultat = await avgangController.Endre(avgang);

            Assert.IsType<BadRequestResult>(resultat);
        }

        [Fact]
        public async Task SlettTrue()
        {
            
            mockRep.Setup(k => k.Slett(1)).ReturnsAsync(true);

            var avgangcontroller = new AvgangerController(mockRep.Object);
            var resultat = await avgangcontroller.Slett(1);

            Assert.IsType<OkObjectResult>(resultat);
        }

        [Fact]
        public async Task SlettFalse()
        {
            
            mockRep.Setup(k => k.Slett(1)).ReturnsAsync(false);

            var avgangController = new AvgangerController(mockRep.Object);
            var resultat = await avgangController.Slett(1);

            Assert.IsType<BadRequestResult>(resultat);
        }

        [Fact]
        public async Task HentAlleAvgangerOK()
        {
            var avgang1 = new Avgang
            {
                Id = 1,
                Fra = "Horten",
                Til = "Drammen",
                Tid = "11:00"
            };

            var avgang2 = new Avgang
            {
                Id = 2,
                Fra = "Horten",
                Til = "Drammen",
                Tid = "14:00"
            };

            var avgang3 = new Avgang
            {
                Id = 3,
                Fra = "Horten",
                Til = "Drammen",
                Tid = "12:00"
            };

            var avgangsListe = new List<Avgang>();
            avgangsListe.Add(avgang1);
            avgangsListe.Add(avgang2);
            avgangsListe.Add(avgang3);

            mockRep.Setup(k => k.HentAlle()).ReturnsAsync(avgangsListe);

            var avgangController = new AvgangerController(mockRep.Object);

            ActionResult resultat = await avgangController.HentAlle();

            Assert.Equal<OkObjectResult>(avgangsListe, resultat);
        }

        [Fact]
        public async Task HentAlleAvgangerNull()
        {
            mockRep.Setup(k => k.HentAlle()).ReturnsAsync(() => null);

            var avgangController = new AvgangerController(mockRep.Object);

            List<Avgang> resultat = await avgangController.HentAlle();

            Assert.Null(resultat);
        }

        [Fact]
        public async Task HentAlleAvgangerTom()
        {
            var avgangsListe = new List<Avgang>();

            mockRep.Setup(k => k.HentAlle()).ReturnsAsync(avgangsListe);

            var avgangController = new AvgangerController(mockRep.Object);

            List<Avgang> resultat = await avgangController.HentAlle();

            Assert.Equal<List<Avgang>>(avgangsListe, resultat);
        }

        [Fact]
        public async Task HentEnAvgangOK()
        {
            var avgang = new Avgang
            {
                Id = 1,
                Fra = "Skarpstadkrysset",
                Til = "Oslo",
                Tid = "11:00"
            };

            mockRep.Setup(l => l.HentEn(1)).ReturnsAsync(avgang);

            var avgangController = new AvgangerController(mockRep.Object);
            var resultat = await avgangController.HentEn(1);

            Assert.Equal(avgang, resultat);
        }

        [Fact]
        public async Task HentEnAvgangNull()
        {
            
            mockRep.Setup(l => l.HentEn(1)).ReturnsAsync(()=>null);

            var avgangController = new AvgangerController(mockRep.Object);
            var resultat = await avgangController.HentEn(5);

            Assert.Null(resultat);
        }
    }
}
