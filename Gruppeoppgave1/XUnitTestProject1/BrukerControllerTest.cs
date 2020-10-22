using Gruppeoppgave1.Controllers;
using Gruppeoppgave1.DAL.IRepositories;
using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace EnhetstestingNor_Way
{
    public class BrukerControllerTest
    {
        private const string _loggetInn = "loggetInn";
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
    }
}
