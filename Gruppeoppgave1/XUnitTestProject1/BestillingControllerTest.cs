using Gruppeoppgave1.Controller;
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
    public class BestillingControllerTest
    {

        private const string _loggetInn = "logget inn";
        private const string _ikkeLoggetInn = "";

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();

        public readonly Mock<IBestillingRepository> mockRepo = new Mock<IBestillingRepository>();
        private readonly Mock<ILogger<BestillingController>> mockLog = new Mock<ILogger<BestillingController>>();

        [Fact]
        public async Task LagreLoggetInnOK()
        {

            mockRepo.Setup(k => k.Lagre(It.IsAny<Bestilling>())).ReturnsAsync(true);

            var bestillingController = new BestillingController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.Lagre(It.IsAny<Bestilling>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Bestillingen ble lagret", resultat.Value);
        }

        [Fact]
        public async Task LagreLoggetInnIkkeOK()
        {
            mockRepo.Setup(k => k.Lagre(It.IsAny<Bestilling>())).ReturnsAsync(false);

            var bestillingController = new BestillingController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.Lagre(It.IsAny<Bestilling>()) as NotFoundObjectResult;

            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Kunne ikke lagre bestillingen", resultat.Value);
        }

        [Fact]
        public async Task LagreLoggetInnFeilModel()
        {
            var bestilling1 = new Bestilling
            {
                Id = 2,
                pris = 100.00,
                Fra = "",
                Til = "Drammen",
                Dato = "2020-12-02",
                Tid = "08:00"
            };

            mockRepo.Setup(k => k.Lagre(bestilling1)).ReturnsAsync(true);

            var bestillingController = new BestillingController(mockRepo.Object, mockLog.Object);
            bestillingController.ModelState.AddModelError("Fra", "Bestillingen er ikke riktig");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.Lagre(bestilling1) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Bestillingen er ikke riktig", resultat.Value);
        }

        //[Fact]
        //public async Task LagreIkkeLoggetInn()
        //{
            
        //    mockRepo.Setup(k => k.Lagre(It.IsAny<Bestilling>())).ReturnsAsync(false);

        //    var bestillingController = new BestillingController(mockRepo.Object, mockLog.Object);

        //    mockSession[_loggetInn] = _ikkeLoggetInn;
        //    mockHttpContext.Setup(s => s.Session).Returns(mockSession);
        //    bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

        //    var resultat = await bestillingController.Lagre(It.IsAny<Bestilling>()) as UnauthorizedObjectResult;

        //    Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
        //    Assert.Equal("ikke logget inn", resultat.Value);
        //}


        [Fact]
        public async Task HentAlleBestillingerLoggetInnOK()
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

            mockRepo.Setup(k => k.HentAlle()).ReturnsAsync(bestillingListe);

            var bestillingController = new BestillingController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.HentAlle() as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<Bestilling>>((List<Bestilling>)resultat.Value, bestillingListe);
        }

        //[Fact]
        //public async Task HentAlleIkkeLoggetInn()
        //{
        //    mockRepo.Setup(k => k.HentAlle()).ReturnsAsync(()=>null);

        //    var bestillingController = new BestillingController(mockRepo.Object, mockLog.Object);

        //    mockSession[_loggetInn] = _ikkeLoggetInn;
        //    mockHttpContext.Setup(s => s.Session).Returns(mockSession);
        //    bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

        //    var resultat = await bestillingController.HentAlle() as UnauthorizedObjectResult;

        //    Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
        //    Assert.Equal("ikke logget inn", resultat.Value);
        //}

        [Fact]
        public async Task HentEnBestillingLoggetInnOK()
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

            mockRepo.Setup(k => k.HentEn(1)).ReturnsAsync(bestilling1);

            var bestillingController = new BestillingController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.HentEn(1) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<Bestilling>(bestilling1, (Bestilling)resultat.Value);
        }

        [Fact]
        public async Task HentEnBestillingNotFound()
        {
            mockRepo.Setup(k => k.HentEn(1)).ReturnsAsync(() => null);

            var bestillingController = new BestillingController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.HentEn(1) as NotFoundObjectResult;

            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Bestillingen ble ikke funnet", resultat.Value);
        }

        [Fact]
        public async Task HentEnBestillingIkkeLoggetInn()
        {
            mockRepo.Setup(k => k.HentEn(1)).ReturnsAsync(() => null);

            var bestillingController = new BestillingController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.HentEn(1) as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }


        [Fact]
        public async Task SlettBestillingLoggetInn()
        {
            mockRepo.Setup(k => k.Slett(It.IsAny<int>())).ReturnsAsync(true);

            var bestillingController = new BestillingController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.Slett(It.IsAny<int>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Bestillingen ble slettet", resultat.Value);
        }

        [Fact]
        public async Task SlettBestillingIkkeFunnet()
        {
            mockRepo.Setup(k => k.Slett(It.IsAny<int>())).ReturnsAsync(false);

            var bestillingController = new BestillingController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.Slett(It.IsAny<int>()) as NotFoundObjectResult;

            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Kunne ikke slette bestillingen", resultat.Value);
        }

        [Fact]
        public async Task SlettBestillingIkkeloggetInn()
        {
            mockRepo.Setup(k => k.Slett(1)).ReturnsAsync(true);

            var bestillingController = new BestillingController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.Slett(1) as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("ikke logget inn", resultat.Value);
        }


        [Fact]
        public async Task EndreBestillingLoggetInnOk()
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

            mockRepo.Setup(k => k.Endre(bestilling)).ReturnsAsync(true);

            var bestillingController = new BestillingController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.Endre(bestilling) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Bestillingen ble endret", resultat.Value);
        }

        [Fact]
        public async Task EndreBestillingIkkeOk()
        {

            mockRepo.Setup(k => k.Endre(It.IsAny<Bestilling>())).ReturnsAsync(false);

            var bestillingController = new BestillingController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.Endre(It.IsAny<Bestilling>()) as NotFoundObjectResult;

            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Kunne ikke endre bestillingen", resultat.Value);
        }


        [Fact]
        public async Task EndreBestillingFeilModel()
        {

            mockRepo.Setup(k => k.Endre(It.IsAny<Bestilling>())).ReturnsAsync(false);

            var bestillingController = new BestillingController(mockRepo.Object, mockLog.Object);
            bestillingController.ModelState.AddModelError("Fra", "Bestillingen mangler felt");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.Endre(It.IsAny<Bestilling>()) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Bestillingen mangler felt", resultat.Value);
        }


        [Fact]
        public async Task EndreBestillingIkkeLoggetInn()
        {

            mockRepo.Setup(k => k.Endre(It.IsAny<Bestilling>())).ReturnsAsync(false);

            var bestillingController = new BestillingController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.Endre(It.IsAny<Bestilling>()) as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }
    }
}
