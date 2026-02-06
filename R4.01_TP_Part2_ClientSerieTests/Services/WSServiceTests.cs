using Microsoft.VisualStudio.TestTools.UnitTesting;
using R4._01_TP_Part2_ClientSerie.Models.EntityFramework;
using R4._01_TP_Part2_ClientSerie.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R4._01_TP_Part2_ClientSerieTests.Services
{
    [TestClass()]
    public class WSServiceTests
    {
        private WSService? _service;
        private const string BaseUri = "https://bouvimaeapiseries-ewg4e6acbybhcrf6.francecentral-01.azurewebsites.net/api/";
        private const string Controleur = "series";


        [TestMethod]
        public void WSServiceTest()
        {

        }

        [TestInitialize]
        public void Setup()
        {
            _service = new WSService(BaseUri);
        }


        [TestMethod]
        public async Task GetSeriesAsync_ReturnsList()
        {
            // Act
            var result = await _service!.GetAll(Controleur);

            // Assert
            Assert.IsNotNull(result, "La liste des séries ne devrait pas être nulle.");
            Assert.IsInstanceOfType(result, typeof(List<Serie>));
        }

        [TestMethod]
        public async Task Post_Put_Delete_FullCycle_Test()
        {
            // 1. TEST POST (Création)
            var nouvelleSerie = new Serie()
            {
                Titre = "Série Test",
                Resume = "Ceci est un test unitaire",
                Nbsaisons = 1,
                Nbepisodes = 10,
                Anneecreation = 2024
            };

            bool postSuccess = await _service!.PostSerieAsync(Controleur, nouvelleSerie);
            Assert.IsTrue(postSuccess, "Le POST a échoué.");

            // Récupération pour obtenir l'ID (si ton API retourne la liste mise à jour)
            var series = await _service.GetAll(Controleur);
            var serieCree = series.Find(s => s.Titre == "Série Test");
            Assert.IsNotNull(serieCree, "La série créée n'a pas été retrouvée.");
            int id = serieCree.Serieid;

            // 2. TEST GET (Récupération unique)
            var serieGet = await _service.GetSerieAsync(Controleur, id);
            Assert.IsNotNull(serieGet);
            Assert.AreEqual("Série Test", serieGet.Titre);

            // 3. TEST PUT (Modification)
            serieGet.Titre = "Série Test Modifiée";
            bool putSuccess = await _service.PutSerieAsync(Controleur, serieGet);
            // Note : Vérifie si ton API attend "Series" ou "Series/id" pour le PUT
            Assert.IsTrue(putSuccess, "Le PUT a échoué.");

            // 4. TEST DELETE (Suppression)
            bool deleteSuccess = await _service.DeleteSerieAsync(Controleur, id);
            Assert.IsTrue(deleteSuccess, "Le DELETE a échoué.");
        }

        [TestMethod]
        public async Task GetSerieAsync_InvalidId_ReturnsNull()
        {
            // Act
            var result = await _service!.GetSerieAsync(Controleur, -1); // ID inexistant

            // Assert
            Assert.IsNull(result, "Le service devrait retourner null pour un ID invalide.");
        }
    }
}