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
    public class BrukerControllerTest
    {
        private const string _loggetInn = "logget inn";
        private const string _ikkeLoggetInn = "";

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();

        public readonly Mock<IBrukerRepository> mockRepo = new Mock<IBrukerRepository>();
        private readonly Mock<ILogger<BrukerController>> mockLog = new Mock<ILogger<BrukerController>>();

        [Fact]
        public async Task BrukerLoggInnOK()
        {
            mockRepo.Setup(k => k.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(true);

            var brukerController = new BrukerController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await brukerController.LoggInn(It.IsAny<Bruker>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.True((bool)resultat.Value);
        }


        [Fact]
        public async Task BrukerLoggInnFeil()
        {
            mockRepo.Setup(k => k.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(false);

            var brukerController = new BrukerController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await brukerController.LoggInn(It.IsAny<Bruker>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.False((bool)resultat.Value);
        }

        [Fact]
        public async Task BrukerFeilInputValidering()
        {
            mockRepo.Setup(k => k.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(false);

            var brukerController = new BrukerController(mockRepo.Object, mockLog.Object);
            brukerController.ModelState.AddModelError("Brukernavn", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await brukerController.LoggInn(It.IsAny<Bruker>()) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public void LoggUtTest()
        {
            var brukerController = new BrukerController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;

            brukerController.LoggUt();

            Assert.Equal(_ikkeLoggetInn, mockSession[_loggetInn]);
        }

        [Fact]
        public async Task HentAlleAvgangerAdminIkkeLoggetInn()
        {

            mockRepo.Setup(k => k.HentAlleAvgangerAdmin()).ReturnsAsync(It.IsAny<List<Avgang>>());
            
            var brukerController = new BrukerController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await brukerController.HentAlleAvgangerAdmin() as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task HentAlleAvgangerAdminOK()
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

            mockRepo.Setup(k => k.HentAlleAvgangerAdmin()).ReturnsAsync(avgangsListe);

            var brukerController = new BrukerController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await brukerController.HentAlleAvgangerAdmin() as OkObjectResult;
            
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<Avgang>>(avgangsListe,(List<Avgang>) resultat.Value);
        }

        [Fact]
        public async Task HentAlleBestillingerAdminUnauthorized()
        {
            mockRepo.Setup(k => k.HentAlleBestillingerAdmin()).ReturnsAsync(It.IsAny<List<Bestilling>>());

            var brukerController = new BrukerController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await brukerController.HentAlleBestillingerAdmin() as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task HentAlleBestillingerAdminOK()
        {

            mockRepo.Setup(k => k.HentAlleBestillingerAdmin()).ReturnsAsync(It.IsAny<List<Bestilling>>());

            var brukerController = new BrukerController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await brukerController.HentAlleBestillingerAdmin() as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<Bestilling>>(It.IsAny<List<Bestilling>>(), (List<Bestilling>)resultat.Value);
        }

        [Fact]
        public async Task HentAlleRuterAdminUnauthorized()
        {
            mockRepo.Setup(k => k.HentAlleRuterAdmin()).ReturnsAsync(It.IsAny<List<Rute>>());

            var brukerController = new BrukerController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await brukerController.HentAlleRuterAdmin() as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task HentAlleRuterAdminOK()
        {
            mockRepo.Setup(k => k.HentAlleRuterAdmin()).ReturnsAsync(It.IsAny<List<Rute>>());

            var brukerController = new BrukerController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await brukerController.HentAlleRuterAdmin() as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<Rute>>(It.IsAny<List<Rute>>(), (List<Rute>)resultat.Value);
        }

        [Fact]
        public async Task HentAlleStasjonerAdminUnauthorized()
        {
            mockRepo.Setup(k => k.HentAlleStasjonerAdmin()).ReturnsAsync(It.IsAny<List<Stasjon>>());

            var brukerController = new BrukerController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await brukerController.HentAlleStasjonerAdmin() as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task HentAlleStasjonerAdminOK()
        {
            mockRepo.Setup(k => k.HentAlleStasjonerAdmin()).ReturnsAsync(It.IsAny<List<Stasjon>>());

            var brukerController = new BrukerController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await brukerController.HentAlleStasjonerAdmin() as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<Stasjon>>(It.IsAny<List<Stasjon>>(), (List<Stasjon>)resultat.Value);
        }
    }
}
