using Gruppeoppgave1.Controllers;
using Gruppeoppgave1.DAL.IRepositories;
using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace EnhetstestingNor_Way
{
    public class RuteControllerTest
    {
        private const string _loggetInn = "logget inn";
        private const string _ikkeLoggetInn = "";

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();

        public readonly Mock<IRuteRepository> mockRepo = new Mock<IRuteRepository>();
        private readonly Mock<ILogger<RuteController>> mockLog = new Mock<ILogger<RuteController>>();

        [Fact]
        public async Task RuteLeggTilUnatuhorized()
        {
            mockRepo.Setup(k => k.LeggTilRute(It.IsAny<Rute>())).ReturnsAsync(false);

            var ruteController = new RuteController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await ruteController.LeggTilRute(It.IsAny<Rute>()) as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task RuteLeggTilOK()
        {
            mockRepo.Setup(k => k.LeggTilRute(It.IsAny<Rute>())).ReturnsAsync(true);

            var ruteController = new RuteController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await ruteController.LeggTilRute(It.IsAny<Rute>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Ruten ble lagt til", resultat.Value);
        }

        [Fact]
        public async Task RuteLeggTilFeilModel()
        {
            mockRepo.Setup(k => k.LeggTilRute(It.IsAny<Rute>())).ReturnsAsync(false);

            var ruteController = new RuteController(mockRepo.Object, mockLog.Object);
            ruteController.ModelState.AddModelError("Navn", "Ruteobjektet er ikke validert");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await ruteController.LeggTilRute(It.IsAny<Rute>()) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Ruteobjektet er ikke validert", resultat.Value);
        }

        [Fact]
        public async Task RuteLeggTilIkkeFunnet()
        {
            var mockListe = new List<Stasjon>();
            var ruten = new Rute()
            {
                Navn = "Drammen-Oslo",
                Id = 2,
                StasjonerPaaRute = mockListe,
            };
            mockRepo.Setup(k => k.LeggTilRute(ruten)).ReturnsAsync(false);

            var ruteController = new RuteController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await ruteController.LeggTilRute(ruten) as NotFoundObjectResult;

            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Kunne ikke legge til rute", resultat.Value);
        }

        [Fact]
        public async Task RuteEndreUnauthorized()
        {
            mockRepo.Setup(k => k.EndreRute(It.IsAny<Rute>())).ReturnsAsync(false);

            var ruteController = new RuteController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await ruteController.EndreRute(It.IsAny<Rute>()) as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task RuteEndreIkkeFunnet()
        {
            mockRepo.Setup(k => k.EndreRute(It.IsAny<Rute>())).ReturnsAsync(false);

            var ruteController = new RuteController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await ruteController.EndreRute(It.IsAny<Rute>()) as NotFoundObjectResult;

            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Kunne ikke endre ruten", resultat.Value);
        }

        [Fact]
        public async Task RuteEndreOk()
        {
            var mockListe = new List<Stasjon>();
            var ruten = new Rute()
            {
                Navn = "Drammen-Oslo",
                Id = 2,
                StasjonerPaaRute = mockListe,
            };

            mockRepo.Setup(k => k.EndreRute(ruten)).ReturnsAsync(true);

            var ruteController = new RuteController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await ruteController.EndreRute(ruten) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Ruten ble endret", resultat.Value);
        }

        [Fact]
        public async Task RuteEndreFeilModel()
        {
            mockRepo.Setup(k => k.EndreRute(It.IsAny<Rute>())).ReturnsAsync(false);

            var ruteController = new RuteController(mockRepo.Object, mockLog.Object);
            ruteController.ModelState.AddModelError("Navn", "Ruteobjektet er ikke validert");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await ruteController.EndreRute(It.IsAny<Rute>()) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Ruteobjektet er ikke validert", resultat.Value);
        }

        [Fact]
        public async Task SlettRuteUnauthorized()
        {
            mockRepo.Setup(k => k.SlettRute(It.IsAny<int>())).ReturnsAsync(false);

            var ruteController = new RuteController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await ruteController.SlettRute(It.IsAny<int>()) as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task SlettRuteOK()
        {
            mockRepo.Setup(k => k.SlettRute(It.IsAny<int>())).ReturnsAsync(true);

            var ruteController = new RuteController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await ruteController.SlettRute(It.IsAny<int>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Ruten ble slettet", resultat.Value);
        }

        [Fact]
        public async Task SlettRuteNotFound()
        {
            mockRepo.Setup(k => k.SlettRute(It.IsAny<int>())).ReturnsAsync(false);

            var ruteController = new RuteController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await ruteController.SlettRute(It.IsAny<int>()) as NotFoundObjectResult;

            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Kunne ikke slette ruten", resultat.Value);
        }

        //[Fact]
        //public async Task HentAlleRuterUnauthorized()
        //{
        //    mockRepo.Setup(k => k.HentAlleRuter()).ReturnsAsync(It.IsAny<List<Rute>>());

        //    var ruteController = new RuteController(mockRepo.Object, mockLog.Object);

        //    mockSession[_loggetInn] = _ikkeLoggetInn;
        //    mockHttpContext.Setup(s => s.Session).Returns(mockSession);
        //    ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

        //    var resultat = await ruteController.HentAlleRuter() as UnauthorizedObjectResult;

        //    Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
        //    Assert.Equal("Ikke logget inn", resultat.Value);
        //}

        [Fact]
        public async Task HentAlleRuterOK()
        {
            mockRepo.Setup(k => k.HentAlleRuter()).ReturnsAsync(It.IsAny<List<Rute>>());

            var ruteController = new RuteController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await ruteController.HentAlleRuter() as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<Rute>>(It.IsAny<List<Rute>>(), (List<Rute>) resultat.Value);
        }

        [Fact]
        public async Task HentEnRuteUnauthorized()
        {
            mockRepo.Setup(k => k.HentEnRute(It.IsAny<int>())).ReturnsAsync(It.IsAny<Rute>());

            var ruteController = new RuteController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await ruteController.HentEnRute(It.IsAny<int>()) as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task HentEnRuteOK()
        {
            var mockListe = new List<Stasjon>();
            var ruten = new Rute()
            {
                Id = 3,
                Navn = "Spikkestad-Gokk",
                StasjonerPaaRute = mockListe,
            };
            mockRepo.Setup(k => k.HentEnRute(It.IsAny<int>())).ReturnsAsync(ruten);

            var ruteController = new RuteController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await ruteController.HentEnRute(It.IsAny<int>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<Rute>(ruten ,(Rute) resultat.Value);
        }

        [Fact]
        public async Task HentEnRuteNotFound()
        {
            mockRepo.Setup(k => k.HentEnRute(It.IsAny<int>())).ReturnsAsync(It.IsAny<Rute>());

            var ruteController = new RuteController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await ruteController.HentEnRute(It.IsAny<int>()) as NotFoundObjectResult;

            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Ruten ble ikke funnet", resultat.Value);
        }
    }
}
