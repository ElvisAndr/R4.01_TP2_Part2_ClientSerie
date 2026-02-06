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
    public partial class ManageSerieVM : ObservableObject
    {
        [ObservableProperty]
        private Serie serieToManage;

        [ObservableProperty]
        private int searchId;

        public IRelayCommand BtnSearchSerie { get; }
        public IRelayCommand BtnUpdateSerie { get; }
        public IRelayCommand BtnDeleteSerie { get; }

        private readonly WSService? _service;
        private const string BaseUri = "https://bouvimaeapiseries-ewg4e6acbybhcrf6.francecentral-01.azurewebsites.net/api/";
        private const string Controleur = "series";

        public ManageSerieVM()
        {
            serieToManage = new Serie();
            _service = new WSService(BaseUri);

            BtnSearchSerie = new RelayCommand(SearchSerie);
            BtnUpdateSerie = new RelayCommand(UpdateSerie);
            BtnDeleteSerie = new RelayCommand(DeleteSerie);
        }

        private async void DeleteSerie()
        {
            _service!.DeleteSerieAsync(Controleur, SerieToManage.Serieid);
            MessageAsync("Serie supprimée avec succès !", "Succès");
        }

        private async void UpdateSerie()
        {
            _service!.PutSerieAsync(Controleur, SerieToManage);
            MessageAsync("Serie mise à jour avec succès !", "Succès");
        }

        private async void SearchSerie()
        {
            if (SearchId <= 0 || SearchId == null)
            {
                await MessageAsync("Veuillez entrer un ID valide pour la recherche.", "ID invalide");
                return;
            }

            SerieToManage = await _service!.GetSerieAsync(Controleur, SearchId);

            if (SerieToManage == null)
            {
                await MessageAsync($"Aucune série trouvée avec l'ID {SearchId}.", "Série non trouvée");
                SerieToManage = new Serie();
            }
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
