using System.Collections.Generic;
using System.Threading.Tasks;
using Films.Core.Concrete.Models;

namespace Films.Core.Abstract.Repositories
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Task<List<Genre>> GetGenresWithVideos();
        Task<Genre> GetGenreWithVideos(int id);
    }
}