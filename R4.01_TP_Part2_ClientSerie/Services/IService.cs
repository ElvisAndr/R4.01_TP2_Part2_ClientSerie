using System.Collections.Generic;
using System.Threading.Tasks;
using  R4._01_TP_Part2_ClientSerie.Models.EntityFramework;

namespace  R4._01_TP_Part2_ClientSerie.Services
{
    public interface IService
    {
        Task<List<Serie>> GetAll(string nomControleur);
        Task<Serie> GetSerieAsync(string nomControleur, int idSerie);
        Task<bool> PutSerieAsync(string nomControleur, Serie serie);
        Task<bool> PostSerieAsync(string nomControleur, Serie serie);
        Task<bool> DeleteSerieAsync(string nomControleur, int idSerie);
    }
}