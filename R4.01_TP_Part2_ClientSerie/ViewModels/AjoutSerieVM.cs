using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using R4._01_TP_Part2_ClientSerie.Models.EntityFramework;
using R4._01_TP_Part2_ClientSerie.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R4._01_TP_Part2_ClientSerie.ViewModels
{
    public partial class AjoutSerieVM : ObservableObject
    {
        [ObservableProperty]
        private Serie serieToAdd;

        public IRelayCommand BtnAddSerie { get; }

        private readonly WSService? _service;
        private const string BaseUri = "https://bouvimaeapiseries-ewg4e6acbybhcrf6.francecentral-01.azurewebsites.net/api/";
        private const string Controleur = "series";

        public AjoutSerieVM()
        {
            BtnAddSerie = new RelayCommand(AddSerie);
            _service = new WSService(BaseUri);
            serieToAdd = new Serie();
        }

        private async void AddSerie()
        {
            //SerieToAdd.Serieid = 500;
            bool result = await _service!.PostSerieAsync(Controleur, SerieToAdd);

            if(SerieToAdd.Titre == null || SerieToAdd.Resume == null || SerieToAdd.Network == null
                || SerieToAdd.Nbsaisons <= 0 || SerieToAdd.Nbepisodes <= 0 || SerieToAdd.Anneecreation <= 0)
            {
                MessageAsync("Veuillez remplir tous les champs avant d'ajouter la série.", "Champs manquants");
                return;
            }

            if (result)
                MessageAsync("La série a été ajoutée avec succès !", "Succès");
            else
                MessageAsync("Une erreur est survenue lors de l'ajout de la série.", "Erreur");
        }
        private async Task MessageAsync(string content, string title)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "Ok",
                XamlRoot = App.MainRoot.XamlRoot
            };

            await dialog.ShowAsync();
        }

    }
}
