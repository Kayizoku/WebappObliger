using Gruppeoppgave1.Controller;
using Gruppeoppgave1.Controllers;
using Gruppeoppgave1.DAL;
using Gruppeoppgave1.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1
{
    public class AvgangerControllerTest
    {
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

            var mock = new Mock<IAvgangerRepository>();
            mock.Setup(l => l.LeggTil(avgang)).ReturnsAsync(true);

            var avgangController = new AvgangerController(mock.Object);
            var resultat = await avgangController.LeggTil(avgang);

            Assert.True(resultat);
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

            var mock = new Mock<IAvgangerRepository>();
            mock.Setup(l => l.LeggTil(avgang)).ReturnsAsync(false);

            var avgangController = new AvgangerController(mock.Object);
            var resultat = await avgangController.LeggTil(avgang);

            Assert.False(resultat);
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

            var mock = new Mock<IAvgangerRepository>();
            mock.Setup(l => l.Endre(avgang)).ReturnsAsync(true);

            var avgangController = new AvgangerController(mock.Object);
            var resultat = await avgangController.Endre(avgang);

            Assert.True(resultat);
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

            var mock = new Mock<IAvgangerRepository>();
            mock.Setup(l => l.Endre(avgang)).ReturnsAsync(false);

            var avgangController = new AvgangerController(mock.Object);
            var resultat = await avgangController.Endre(avgang);

            Assert.False(resultat);
        }

        [Fact]
        public async Task SlettTrue()
        {
            var mock = new Mock<IAvgangerRepository>();
            mock.Setup(k => k.Slett(1)).ReturnsAsync(true);

            var avgangcontroller = new AvgangerController(mock.Object);
            var resultat = await avgangcontroller.Slett(1);

            Assert.True(resultat);
        }

        [Fact]
        public async Task SlettFalse()
        {
            var mock = new Mock<IAvgangerRepository>();
            mock.Setup(k => k.Slett(1)).ReturnsAsync(false);

            var avgangController = new AvgangerController(mock.Object);
            var resultat = await avgangController.Slett(1);

            Assert.False(resultat);
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

            var mock = new Mock<IAvgangerRepository>();
            mock.Setup(k => k.HentAlle()).ReturnsAsync(avgangsListe);

            var avgangController = new AvgangerController(mock.Object);

            List<Avgang> resultat = await avgangController.HentAlle();

            Assert.Equal<List<Avgang>>(avgangsListe, resultat);
        }

        [Fact]
        public async Task HentAlleAvgangerNull()
        {
            var mock = new Mock<IAvgangerRepository>();
            mock.Setup(k => k.HentAlle()).ReturnsAsync(() => null);

            var avgangController = new AvgangerController(mock.Object);

            List<Avgang> resultat = await avgangController.HentAlle();

            Assert.Null(resultat);
        }

        [Fact]
        public async Task HentAlleAvgangerTom()
        {
            var avgangsListe = new List<Avgang>();

            var mock = new Mock<IAvgangerRepository>();
            mock.Setup(k => k.HentAlle()).ReturnsAsync(avgangsListe);

            var avgangController = new AvgangerController(mock.Object);

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

            var mock = new Mock<IAvgangerRepository>();
            mock.Setup(l => l.HentEn(1)).ReturnsAsync(avgang);

            var avgangController = new AvgangerController(mock.Object);
            var resultat = await avgangController.HentEn(1);

            Assert.Equal(avgang, resultat);
        }

        [Fact]
        public async Task HentEnAvgangNull()
        {
            var mock = new Mock<IAvgangerRepository>();
            mock.Setup(l => l.HentEn(1)).ReturnsAsync(()=>null);

            var avgangController = new AvgangerController(mock.Object);
            var resultat = await avgangController.HentEn(5);

            Assert.Null(resultat);
        }
    }
}
