using Microsoft.VisualStudio.TestTools.UnitTesting;
using R4._01_TP_Part2_ClientSerie.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R4._01_TP_Part2_ClientSerie.Services.Tests
{
    [TestClass()]
    public class WSServiceTests
    {
        [TestMethod()]
        public async Task GetAllTest()
        {
            // 1. Arrange
            // Vérifiez bien ce port ! (5204, 7089, etc ?)
            WSService service = new WSService("http://localhost:5204/api/");

            try
            {
                var result = await service.GetAll("Series");

                // 3. Assert
                Assert.IsNotNull(result, "La liste retournée est null");
                Assert.IsTrue(result.Count > 0, "La liste est vide (l'API fonctionne mais pas de données)");
            }
            catch (Exception ex)
            {
                // Ceci affichera l'erreur précise dans l'explorateur de tests
                Assert.Fail($"ERREUR CRITIQUE : {ex.Message} | Type: {ex.GetType().Name}");
            }
        }

        [TestMethod()]
        public void GetSerieAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PutSerieAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PostSerieAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteSerieAsyncTest()
        {
            Assert.Fail();
        }
    }
}