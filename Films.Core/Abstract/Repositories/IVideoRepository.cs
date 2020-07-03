using System.Collections.Generic;
using System.Threading.Tasks;
using Films.Core.Concrete.Models;

namespace Films.Core.Abstract.Repositories
{
    public interface IVideoRepository : IRepository<Video>
    {
        Task<List<Video>> GetVideosWithDescription();
        Task<List<Video>> GetVideosWithGenres();
        Task<Video> GetVideoWithGenres(int id);
        List<Video> GetVideosOnRange(IEnumerable<int> ids);
        Task<IEnumerable<Video>> GetVideosByUserId(string id);
    }
}