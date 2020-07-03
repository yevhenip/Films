using System.Collections.Generic;
using System.Threading.Tasks;
using Films.Core.Concrete.Models;

namespace Films.Core.Abstract.Managers
{
    public interface IVideoManager : IManager
    {
        Task<List<Video>> GetAllVideos();
        Task<Video> GetVideo(int id);
        Task<List<Video>> GetVideosWithDescription();
        void AddVideo(Video video);
        void RemoveVideo(Video video);
        Task<List<Video>> GetVideosWithGenres();
        Task<Video> GetVideoWithGenres(int id);
        Task<bool> IsVideosIdValid(IEnumerable<int> videosToValidate);
        Task<IEnumerable<Video>> GetVideosByUserId(string id);
    }
}