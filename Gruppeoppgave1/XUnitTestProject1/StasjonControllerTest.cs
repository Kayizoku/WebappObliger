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
    public class StasjonControllerTest 
    {
        public readonly Mock<IStasjonRepository> mockRepo = new Mock<IStasjonRepository>();

        [Fact]
        public async Task HentAlleOK()
        {
            var stasjon1 = new Stasjon
            {
                Id = 1,
                NummerPaaStopp = 3,
                StasjonsNavn = "Nannestad"
            };
            var stasjon2 = new Stasjon
            {
                Id = 2,
                NummerPaaStopp = 4,
                StasjonsNavn = "Lillehammer"
            };
            var stasjon3 = new Stasjon
            {
                Id = 1,
                NummerPaaStopp = 4,
                StasjonsNavn = "Tromsø"
            };

            var stasjonsListe = new List<Stasjon>();
            stasjonsListe.Add(stasjon1);
            stasjonsListe.Add(stasjon2);
            stasjonsListe.Add(stasjon3);

            mockRepo.Setup(k => k.HentAlleStasjoner()).ReturnsAsync(stasjonsListe);

            var stasjonController = new StasjonController(mockRepo.Object);

            var resultat = await stasjonController.HentAlleStasjoner();

            Assert.Equal(resultat, stasjonsListe);
        }

        [Fact]
        public async Task HentAlleTom()
        {
            var stasjonsListe = new List<Stasjon>();

            mockRepo.Setup(k => k.HentAlleStasjoner()).ReturnsAsync(stasjonsListe);

            var stasjonController = new StasjonController(mockRepo.Object);

            var resultat = await stasjonController.HentAlleStasjoner();

            Assert.Equal(resultat, stasjonsListe);
        }

        [Fact]
        public async Task HentAlleNull()
        {
            mockRepo.Setup(k => k.HentAlleStasjoner()).ReturnsAsync(()=>null);

            var stasjonController = new StasjonController(mockRepo.Object);

            var resultat = await stasjonController.HentAlleStasjoner();

            Assert.Null(resultat);
        }

        [Fact]
        public async Task HentEnStasjonOK()
        {
            var stasjon = new Stasjon
            {
                Id = 1,
                NummerPaaStopp = 2,
                StasjonsNavn = "Bekkestua"
            };

            mockRepo.Setup(k => k.HentEnStasjon(1)).ReturnsAsync(stasjon);

            var stasjonController = new StasjonController(mockRepo.Object);
            var resultat = await stasjonController.HentEnStasjon(1);

            Assert.Equal(resultat, stasjon);
        }

        [Fact]
        public async Task HentEnStasjonNull()
        {
            mockRepo.Setup(k => k.HentEnStasjon(1)).ReturnsAsync(()=>null);

            var stasjonController = new StasjonController(mockRepo.Object);
            var resultat = await stasjonController.HentEnStasjon(1);

            Assert.Null(resultat);
        }

        [Fact]
        public async Task EndreStasjonTrue()
        {
            var nyStasjon = new Stasjon
            {
                Id = 1,
                NummerPaaStopp = 1,
                StasjonsNavn = "Oslo"
            };

            mockRepo.Setup(k => k.EndreStasjon(nyStasjon)).ReturnsAsync(true);

            var stasjonController = new StasjonController(mockRepo.Object);
            var resultat = await stasjonController.EndreStasjon(nyStasjon);

            Assert.True(resultat);
        }

        [Fact]
        public async Task EndreStasjonFalse()
        {
            var nyStasjon = new Stasjon
            {
                Id = 2,
                NummerPaaStopp = 2,
                StasjonsNavn = "Drammen"
            };

            mockRepo.Setup(k => k.EndreStasjon(nyStasjon)).ReturnsAsync(false);

            var stasjonController = new StasjonController(mockRepo.Object);
            var resultat = await stasjonController.EndreStasjon(nyStasjon);

            Assert.False(resultat);
        }

        [Fact]
        public async Task FjernStasjonTrue()
        {
            mockRepo.Setup(k => k.FjernStasjon(1)).ReturnsAsync(true);

            var stasjonController = new StasjonController(mockRepo.Object);
            var resultat = await stasjonController.FjernStasjon(1);

            Assert.True(resultat);
        }

        [Fact]
        public async Task FjernStasjonFalse()
        {
            mockRepo.Setup(k => k.FjernStasjon(1)).ReturnsAsync(false);

            var stasjonController = new StasjonController(mockRepo.Object);
            var resultat = await stasjonController.FjernStasjon(1);

            Assert.False(resultat);
        }
    }
}
