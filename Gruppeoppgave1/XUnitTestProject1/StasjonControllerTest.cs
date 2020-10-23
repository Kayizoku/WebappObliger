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
    public class StasjonControllerTest 
    {
        private const string _loggetInn = "logget inn";
        private const string _ikkeLoggetInn = "";

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();

        public readonly Mock<IStasjonRepository> mockRepo = new Mock<IStasjonRepository>();
        private readonly Mock<ILogger<StasjonController>> mockLog = new Mock<ILogger<StasjonController>>();

        [Fact]
        public async Task HentAlleLoggetInnOK()
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

            var stasjonController = new StasjonController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            stasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await stasjonController.HentAlleStasjoner() as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<Stasjon>>((List<Stasjon>)resultat.Value, stasjonsListe);
        }

        
        //[Fact]
        //public async Task HentAlleIkkeLoggetInn()
        //{
        //    mockRepo.Setup(k => k.HentAlleStasjoner()).ReturnsAsync(()=>null);

        //    var stasjonController = new StasjonController(mockRepo.Object, mockLog.Object);

        //    mockSession[_loggetInn] = _ikkeLoggetInn;
        //    mockHttpContext.Setup(s => s.Session).Returns(mockSession);
        //    stasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

        //    var resultat = await stasjonController.HentAlleStasjoner() as UnauthorizedObjectResult;

        //    Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
        //    Assert.Equal("Ikke logget inn", resultat.Value);
        //}

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

            var stasjonController = new StasjonController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            stasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await stasjonController.HentEnStasjon(1) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<Stasjon>(stasjon, (Stasjon)resultat.Value);
        }

        [Fact]
        public async Task HentEnStasjonIkkeFunnet()
        {
            mockRepo.Setup(k => k.HentEnStasjon(1)).ReturnsAsync(()=>null);

            var stasjonController = new StasjonController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            stasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await stasjonController.HentEnStasjon(1) as NotFoundObjectResult;

            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Fant ikke stasjonen", resultat.Value);
        }

        [Fact]
        public async Task HentEnStasjonIkkeLoggetInn()
        {
            mockRepo.Setup(k => k.HentEnStasjon(It.IsAny<int>())).ReturnsAsync(()=>null);

            var stasjonController = new StasjonController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            stasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await stasjonController.HentEnStasjon(It.IsAny<int>()) as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task EndreStasjonLoggetInnOk()
        {
            var nyStasjon = new Stasjon
            {
                Id = 1,
                NummerPaaStopp = 1,
                StasjonsNavn = "Oslo"
            };

            mockRepo.Setup(k => k.EndreStasjon(nyStasjon)).ReturnsAsync(true);

            var stasjonController = new StasjonController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            stasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await stasjonController.EndreStasjon(nyStasjon) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Stasjonen ble endret", resultat.Value);
        }

        [Fact]
        public async Task EndreStasjonIkkeLoggetInn()
        {
            var nyStasjon = new Stasjon
            {
                Id = 2,
                NummerPaaStopp = 2,
                StasjonsNavn = "Drammen"
            };

            mockRepo.Setup(k => k.EndreStasjon(nyStasjon)).ReturnsAsync(false);
            var stasjonController = new StasjonController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            stasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await stasjonController.EndreStasjon(nyStasjon) as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task EndreStasjonLoggetInnIkkeOk()
        {
            
            mockRepo.Setup(k => k.EndreStasjon(It.IsAny<Stasjon>())).ReturnsAsync(false);

            var stasjonController = new StasjonController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            stasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await stasjonController.EndreStasjon(It.IsAny<Stasjon>()) as NotFoundObjectResult;

            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Kunne ikke endre stasjon!", resultat.Value);
        }

        [Fact]
        public async Task EndreStasjonFeilModel()
        {
            var nyStasjon = new Stasjon
            {
                Id = 2,
                NummerPaaStopp = 2,
                StasjonsNavn = "Drammen"
            };

            mockRepo.Setup(k => k.EndreStasjon(nyStasjon)).ReturnsAsync(false);
            var stasjonController = new StasjonController(mockRepo.Object, mockLog.Object);
            stasjonController.ModelState.AddModelError("StasjonsNavn", "ikke gyldig Stasjon");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            stasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await stasjonController.EndreStasjon(It.IsAny<Stasjon>()) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Ikke gyldig Stasjon", resultat.Value);
        }


        [Fact]
        public async Task FjernStasjonUnauthorized()
        {
            mockRepo.Setup(k => k.FjernStasjon(It.IsAny<int>())).ReturnsAsync(false);
            var stasjonController = new StasjonController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            stasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await stasjonController.FjernStasjon(It.IsAny<int>()) as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task FjernStasjonLoggetInnIkkeOk()
        {
            mockRepo.Setup(k => k.FjernStasjon(It.IsAny<int>())).ReturnsAsync(false);

            var stasjonController = new StasjonController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            stasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await stasjonController.FjernStasjon(It.IsAny<int>()) as NotFoundObjectResult;

            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Kunne ikke slette stasjonen", resultat.Value);
        }


        [Fact]
        public async Task FjernStasjonOK()
        {
            mockRepo.Setup(k => k.FjernStasjon(It.IsAny<int>())).ReturnsAsync(true);

            var stasjonController = new StasjonController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            stasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await stasjonController.FjernStasjon(It.IsAny<int>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Stasjonen ble fjernet", resultat.Value);
        }

        [Fact]
        public async Task LagreStasjonOK()
        {

            mockRepo.Setup(k => k.LagreStasjon(It.IsAny<Stasjon>())).ReturnsAsync(true);

            var stasjonController = new StasjonController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            stasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await stasjonController.LagreStasjon(It.IsAny<Stasjon>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Stasjonen ble lagt til", resultat.Value);
        }

        [Fact]
        public async Task LagreStasjonIkkeOKLoggetInn()
        {

            mockRepo.Setup(k => k.LagreStasjon(It.IsAny<Stasjon>())).ReturnsAsync(true);

            var stasjonController = new StasjonController(mockRepo.Object, mockLog.Object);
            stasjonController.ModelState.AddModelError("StasjonsNavn", "Stasjonen mangler felt");


            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            stasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await stasjonController.LagreStasjon(It.IsAny<Stasjon>()) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Stasjonen mangler felt", resultat.Value);
        }

        [Fact]
        public async Task LagreStasjonUnauthorized()
        {

            mockRepo.Setup(k => k.LagreStasjon(It.IsAny<Stasjon>())).ReturnsAsync(true);

            var stasjonController = new StasjonController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            stasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await stasjonController.LagreStasjon(It.IsAny<Stasjon>()) as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task LagreStasjonIkkeOk()
        {
            mockRepo.Setup(k => k.LagreStasjon(It.IsAny<Stasjon>())).ReturnsAsync(false);

            var stasjonController = new StasjonController(mockRepo.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            stasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await stasjonController.LagreStasjon(It.IsAny<Stasjon>()) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Kunne ikke legge til stasjon", resultat.Value);
        }
    }
}
