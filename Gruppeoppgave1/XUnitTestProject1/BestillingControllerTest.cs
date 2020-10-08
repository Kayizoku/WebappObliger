using Gruppeoppgave1.Controller;
using Gruppeoppgave1.DAL;
using Gruppeoppgave1.Model;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1
{
    public class BestillingControllerTest
    {
        [Fact]
        public async Task Lagre()
        {
            //Arrange
            var innBestilling = new Bestilling
            {
                Id = 1,
                pris = 100,
                Fra = "Sandvika",
                Til = "Lysaker",
                Dato = "2020-10-31",
                Tid = "07:00"
            };

            var mock = new Mock<IBestillingRepository>();
            mock.Setup(innBestilling => innBestilling.Lagre((Bestilling)innBestilling)).ReturnsAsync(true);
            var bestillingController = new BestillingController(mock.Object);


            //Act

            bool resultat = await bestillingController.Lagre(innBestilling);

            //Assert

            Assert.True(resultat);

        }
    }
}
