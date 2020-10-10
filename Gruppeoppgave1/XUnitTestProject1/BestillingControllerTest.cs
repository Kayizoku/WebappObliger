using Gruppeoppgave1.Controller;
using Gruppeoppgave1.DAL;
using Gruppeoppgave1.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1
{
    public class BestillingControllerTest
    {
        [Fact]
        public async Task LagreOK()
        {
            //Arrange
            var innBestilling = new Bestilling
            {
                Id = 1,
                pris = 100.00,
                Fra = "Sandvika",
                Til = "Lysaker",
                Dato = "2020-10-31",
                Tid = "07:00"
            };

            var mock = new Mock<IBestillingRepository>();
            mock.Setup(k => k.Lagre(innBestilling)).ReturnsAsync(true);
            var bestillingController = new BestillingController(mock.Object);


            //Act

            bool resultat = await bestillingController.Lagre(innBestilling);

            //Assert

            Assert.True(resultat);

        }

        [Fact]
        public async Task LagreIkkeOK()
        {
            var innBestilling = new Bestilling
            {
                Id = 1,
                pris = 100.00,
                Fra = "Sandvika",
                Til = "Lysaker",
                Dato = "2020-10-31",
                Tid = "07:00"
            };

            var mock = new Mock<IBestillingRepository>();
            mock.Setup(k => k.Lagre(innBestilling)).ReturnsAsync(false);
            var bestillingController = new BestillingController(mock.Object);

            bool resultat = await bestillingController.Lagre(innBestilling);

            Assert.False(resultat);
        }

        [Fact]
        public async Task HentAlleBestillinger()
        {
            var bestilling1 = new Bestilling
            {
                Id = 2,
                pris = 100.00,
                Fra = "Horten",
                Til = "Drammen",
                Dato = "2020-12-02",
                Tid = "08:00"
            };

            var bestilling2 = new Bestilling
            {
                Id = 3,
                pris = 150.00,
                Fra = "Oslo",
                Til = "Nordstrand",
                Dato = "2020-10-12",
                Tid = "12:00"
            };

            var bestilling3 = new Bestilling
            {
                Id = 4,
                pris = 200.00,
                Fra = "Nordstrand",
                Til = "Bergen",
                Dato = "2020-11-01",
                Tid = "15:00"
            };

            var bestillingListe = new List<Bestilling>();
            bestillingListe.Add(bestilling1);
            bestillingListe.Add(bestilling2);
            bestillingListe.Add(bestilling3);

            var mock = new Mock<IBestillingRepository>();
            mock.Setup(k => k.HentAlle()).ReturnsAsync(bestillingListe);

            var BestillingController = new BestillingController(mock.Object);

            List<Bestilling> resultat = await BestillingController.HentAlle();

            Assert.Equal<List<Bestilling>>(bestillingListe, resultat);
        }

        [Fact]
        public async Task HentAlleTom()
        {
            var bestillingListe = new List<Bestilling>();

            var mock = new Mock<IBestillingRepository>();
            mock.Setup(k => k.HentAlle()).ReturnsAsync(bestillingListe);

            var BestillingController = new BestillingController(mock.Object);

            List<Bestilling> resultat = await BestillingController.HentAlle();

            Assert.Equal<List<Bestilling>>(bestillingListe, resultat);
        }

        [Fact]
        public async Task HentAlleNull()
        {
            
            var mock = new Mock<IBestillingRepository>();
            mock.Setup(k => k.HentAlle()).ReturnsAsync(()=>null);

            var BestillingController = new BestillingController(mock.Object);

            List<Bestilling> resultat = await BestillingController.HentAlle();

            Assert.Null(resultat);
        }

        [Fact]
        public async Task HentEnBestillingOK()
        {
            var bestilling1 = new Bestilling
            {
                Id = 4,
                pris = 200.00,
                Fra = "Nordstrand",
                Til = "Bergen",
                Dato = "2020-11-01",
                Tid = "15:00"
            };

            var mock = new Mock<IBestillingRepository>();
            mock.Setup(k => k.HentEn(1)).ReturnsAsync(bestilling1);

            var bestillingController = new BestillingController(mock.Object);
            var resultat = await bestillingController.HentEn(1);

            Assert.Equal<Bestilling>(bestilling1, resultat);
        }

        [Fact]
        public async Task HentEnBestillingNull()
        {
            var mock = new Mock<IBestillingRepository>();
            mock.Setup(k => k.HentEn(1)).ReturnsAsync(()=>null);

            var bestillingController = new BestillingController(mock.Object);
            var resultat = await bestillingController.HentEn(1);

            Assert.Null(resultat);
        }

        [Fact]
        public async Task SlettTrue()
        {
            var mock = new Mock<IBestillingRepository>();
            mock.Setup(k => k.Slett(1)).ReturnsAsync(true);

            var bestillingController = new BestillingController(mock.Object);
            var resultat = await bestillingController.Slett(1);

            Assert.True(resultat);
        }

        [Fact]
        public async Task SlettFalse()
        {
            var mock = new Mock<IBestillingRepository>();
            mock.Setup(k => k.Slett(1)).ReturnsAsync(false);

            var bestillingController = new BestillingController(mock.Object);
            var resultat = await bestillingController.Slett(1);

            Assert.False(resultat);
        }

        [Fact]
        public async Task EndreTrue()
        {
            var bestilling = new Bestilling
            {
                Id = 3,
                pris = 50.00,
                Fra = "Horten",
                Til = "Drammen",
                Dato = "2020-09-12",
                Tid = "10:00"
            };

            var mock = new Mock<IBestillingRepository>();
            mock.Setup(k => k.Endre(bestilling)).ReturnsAsync(true);

            var bestillingController = new BestillingController(mock.Object);
            var resultat = await bestillingController.Endre(bestilling);

            Assert.True(resultat);
        }

        [Fact]
        public async Task EndreFalse()
        {
            var bestilling = new Bestilling
            {
                Id = 3,
                pris = 50.00,
                Fra = "Horten",
                Til = "Drammen",
                Dato = "2020-09-12",
                Tid = "10:00"
            };

            var mock = new Mock<IBestillingRepository>();
            mock.Setup(k => k.Endre(bestilling)).ReturnsAsync(false);

            var bestillingController = new BestillingController(mock.Object);
            var resultat = await bestillingController.Endre(bestilling);

            Assert.False(resultat);
        }



    }
}
