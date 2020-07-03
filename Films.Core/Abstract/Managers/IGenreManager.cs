using System.Collections.Generic;
using System.Threading.Tasks;
using Films.Core.Concrete.Models;

namespace Films.Core.Abstract.Managers
{
    public interface IGenreManager : IManager
    {
        Task<List<Genre>> GetAllGenres();
        Task<Genre> GetGenre(int id);
        void AddGenre(Genre genre);
        void RemoveGenre(Genre genre);
        Task<List<Genre>> GetRangeOfGenres(IEnumerable<int> genresId);
        Task<bool> IsGenresIdValid(IEnumerable<int> genres);
        Task<List<Genre>> GetGenresWithVideos();
        Task<Genre> GetGenreWithVideos(int id);
    }
}