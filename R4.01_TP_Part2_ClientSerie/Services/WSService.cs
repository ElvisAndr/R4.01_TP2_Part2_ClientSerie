using  R4._01_TP_Part2_ClientSerie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using  R4._01_TP_Part2_ClientSerie.Models.EntityFramework;

namespace  R4._01_TP_Part2_ClientSerie.Services
{
    public class WSService : IService
    {
        private readonly HttpClient httpClient;

        public WSService(string uri)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(uri);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Serie>> GetAll(string nomControleur)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<List<Serie>>(nomControleur);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<Serie> GetSerieAsync(string nomControleur, int idSerie)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<Serie>($"{nomControleur}/{idSerie}");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> PutSerieAsync(string nomControleur, Serie serie)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync($"{nomControleur}/{serie.Serieid}", serie);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> PostSerieAsync(string nomControleur, Serie serie)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(nomControleur, serie);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteSerieAsync(string nomControleur, int idSerie)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"{nomControleur}/{idSerie}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}