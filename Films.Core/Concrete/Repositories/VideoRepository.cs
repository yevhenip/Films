using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Films.Core.Abstract.Repositories;
using Films.Core.Concrete.Models;
using Microsoft.EntityFrameworkCore;

namespace Films.Core.Concrete.Repositories
{
    public class VideoRepository : Repository<Video>, IVideoRepository
    {
        public VideoRepository(DbContext context) : base(context)
        {
        }

        public Task<List<Video>> GetVideosWithDescription()
        {
            return Context.Set<Video>().Where(v => v.Description != null).ToListAsync();
        }

        public Task<List<Video>> GetVideosWithGenres()
        {
            return Context.Set<Video>().Include(v => v.Genres).ToListAsync();
        }

        public Task<Video> GetVideoWithGenres(int id)
        {
            return Context.Set<Video>().Include(v => v.Genres)
                .SingleOrDefaultAsync(v => v.Id == id);
        }

        public List<Video> GetVideosOnRange(IEnumerable<int> ids)
        {
            var videos = GetAll();

            return (from video in videos from id in ids where id == video.Id select video).ToList();
        }

        public async Task<IEnumerable<Video>> GetVideosByUserId(string id)
        {
            return await Context.Set<Video>().Include(v => v.User)
                .Where(v => v.User.Id == id).ToListAsync();
        }
    }
}