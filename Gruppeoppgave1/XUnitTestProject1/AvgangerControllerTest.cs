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
    public class AvgangerControllerTest
    {
        private const string _loggetInn = "logget inn";
        private const string _ikkeLoggetInn = "";

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();

        private readonly Mock<IAvgangerRepository> mockRep = new Mock<IAvgangerRepository>();
        private readonly Mock<ILogger<AvgangerController>> mockLog = new Mock<ILogger<AvgangerController>>();

        [Fact]
        public async Task LeggTilAvgangLoggetInnOk()
        {
            /*
            var avgang = new Avgang
            {
                Id = 1,
                Fra = "Nordstrand",
                Til = "Oslo",
                Tid = "11:00"
            };*/


            mockRep.Setup(k => k.LeggTil(It.IsAny<Avgang>())).ReturnsAsync(true);

            var avgangController = new AvgangerController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await avgangController.LeggTil(It.IsAny<Avgang>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Avgangen ble lagt til", resultat.Value);
        }

        [Fact]
        public async Task LeggTilAvgangUnauthorized()
        {
            
            mockRep.Setup(l => l.LeggTil(It.IsAny<Avgang>())).ReturnsAsync(false);

            var avgangController = new AvgangerController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await avgangController.LeggTil(It.IsAny<Avgang>()) as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }


        [Fact]
        public async Task KunneIkkeLeggeTilAvgang()
        {

            mockRep.Setup(l => l.LeggTil(It.IsAny<Avgang>())).ReturnsAsync(false);

            var avgangController = new AvgangerController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await avgangController.LeggTil(It.IsAny<Avgang>()) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Kunne ikke legge til avgang", resultat.Value);
        }


        [Fact]
        public async Task LeggTilFeilAvgangObjekt()
        {
            var avgang = new Avgang
            {
                Id = 1,
                Fra = "",
                Til = "Oslo",
                Tid = "11:00"
            };

            mockRep.Setup(l => l.LeggTil(avgang)).ReturnsAsync(true);

            var avgangController = new AvgangerController(mockRep.Object, mockLog.Object);
            avgangController.ModelState.AddModelError("Fra", "Avgangsobjektet er ikke riktig");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await avgangController.LeggTil(avgang) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Avgangsobjektet er ikke riktig", resultat.Value);
        }

        [Fact]
        public async Task EndreAvgangUnauthorized()
        {
            mockRep.Setup(l => l.Endre(It.IsAny<Avgang>())).ReturnsAsync(true);

            var avgangController = new AvgangerController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await avgangController.Endre(It.IsAny<Avgang>()) as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }


        [Fact]
        public async Task EndreAvgangOk()
        {
            mockRep.Setup(k => k.Endre(It.IsAny<Avgang>())).ReturnsAsync(true);

            var avgangController = new AvgangerController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await avgangController.Endre(It.IsAny<Avgang>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Avgangen ble endret", resultat.Value);
        }


        [Fact]
        public async Task EndreAvgangFeilModel()
        {
            var avgang = new Avgang
            {
                Id = 1,
                Fra = "",
                Til = "Drammen",
                Tid = "11:00"
            };

            mockRep.Setup(l => l.Endre(avgang)).ReturnsAsync(true);

            var avgangController = new AvgangerController(mockRep.Object, mockLog.Object);
            avgangController.ModelState.AddModelError("Fra", "Avgangen er feil");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await avgangController.Endre(avgang) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Avgangen er feil", resultat.Value);
        }


        [Fact]
        public async Task KunneIkkeEndreAvgang()
        {
            var avgang = new Avgang
            {
                Id = 1,
                Fra = "Horten",
                Til = "Drammen",
                Tid = "11:00"
            };

            mockRep.Setup(l => l.Endre(avgang)).ReturnsAsync(false);

            var avgangController = new AvgangerController(mockRep.Object, mockLog.Object);
            

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await avgangController.Endre(avgang) as NotFoundObjectResult;

            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Kunne ikke endre på avgangen", resultat.Value);
        }


        [Fact]
        public async Task SlettIkkeLoggetInn()
        {
            
            mockRep.Setup(k => k.Slett(1)).ReturnsAsync(false);

            var avgangController = new AvgangerController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await avgangController.Slett(1) as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task SlettLoggetInnOk()
        {

            mockRep.Setup(k => k.Slett(1)).ReturnsAsync(true);

            var avgangController = new AvgangerController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await avgangController.Slett(1) as OkObjectResult;
 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Avgangen ble slettet", resultat.Value);
        }


        [Fact]
        public async Task KunneIkkeSletteLoggetInn()
        {

            mockRep.Setup(k => k.Slett(1)).ReturnsAsync(false);

            var avgangController = new AvgangerController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await avgangController.Slett(It.IsAny<int>()) as NotFoundObjectResult;

            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Kunne ikke slette avgangen", resultat.Value);
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

            var avgangController = new AvgangerController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await avgangController.HentAlle() as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<Avgang>>((List<Avgang>)resultat.Value, avgangsListe);
        }


        [Fact]
        public async Task HentEnAvgangLoggetInnOK()
        {
            var avgang = new Avgang
            {
                Id = 1,
                Fra = "Skarpstadkrysset",
                Til = "Oslo",
                Tid = "11:00"
            };

            mockRep.Setup(l => l.HentEn(1)).ReturnsAsync(avgang);

            var avgangController = new AvgangerController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await avgangController.HentEn(1) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<Avgang>(avgang, (Avgang)resultat.Value);
        }

        //[Fact]
        //public async Task HentEnAvgangIkkeLoggetInn()
        //{

        //    mockRep.Setup(l => l.HentEn(1)).ReturnsAsync(()=>null);

        //    var avgangController = new AvgangerController(mockRep.Object, mockLog.Object);

        //    mockSession[_loggetInn] = _ikkeLoggetInn;
        //    mockHttpContext.Setup(s => s.Session).Returns(mockSession);
        //    avgangController.ControllerContext.HttpContext = mockHttpContext.Object;

        //    var resultat = await avgangController.HentEn(1) as UnauthorizedObjectResult;

        //    Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
        //    Assert.Equal("Ikke logget inn", resultat.Value);
        //}
    }
}
