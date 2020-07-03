using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Films.Core.Abstract.Managers;
using Films.Core.Abstract.Repositories;
using Films.Core.Concrete.Models;

namespace Films.Core.Concrete.Managers
{
    public class VideoManager : IVideoManager
    {
        private readonly IVideoRepository _videoRepository;

        public VideoManager(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public int SaveChanges()
        {
            return _videoRepository.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _videoRepository.SaveChangesAsync();
        }

        public async Task<List<Video>> GetAllVideos()
        {
            return await _videoRepository.GetAllAsync();
        }

        public async Task<Video> GetVideo(int id)
        {
            return await _videoRepository.SingleOrDefaultAsync(v => v.Id == id);
        }

        public async Task<List<Video>> GetVideosWithDescription()
        {
            return await _videoRepository.GetVideosWithDescription();
        }

        public void AddVideo(Video video)
        {
            _videoRepository.Add(video);
        }

        public void RemoveVideo(Video video)
        {
            _videoRepository.Remove(video);
        }

        public async Task<List<Video>> GetVideosWithGenres()
        {
            return await _videoRepository.GetVideosWithGenres();
        }

        public async Task<Video> GetVideoWithGenres(int id)
        {
            return await _videoRepository.GetVideoWithGenres(id);
        }

        public async Task<bool> IsVideosIdValid(IEnumerable<int> videosToValidate)
        {
            var videosInBd = await _videoRepository.GetAllAsync();
            var videos = videosToValidate.ToList();
            var setOfGenres = videos.ToHashSet();
            return videos.Count == setOfGenres.Count &&
                   videos.Select(video => videosInBd.Any(video1 => video1.Id == video))
                       .All(flag => flag);
        }

        public async Task<IEnumerable<Video>> GetVideosByUserId(string id)
        {
            return await _videoRepository.GetVideosByUserId(id);
        }

        public List<Video> GetVideosOnRange(IEnumerable<int> ids)
        {
            return _videoRepository.GetVideosOnRange(ids);
        }
    }
}