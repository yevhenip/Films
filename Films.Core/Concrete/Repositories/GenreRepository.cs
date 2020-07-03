using System.Collections.Generic;
using System.Threading.Tasks;
using Films.Core.Abstract.Repositories;
using Films.Core.Concrete.Models;
using Microsoft.EntityFrameworkCore;

namespace Films.Core.Concrete.Repositories
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(DbContext context) : base(context)
        {
        }

      

        public async Task<List<Genre>> GetGenresWithVideos()
        {
            return await Context.Set<Genre>().Include(a => a.Videos).ToListAsync();
        }

        public async Task<Genre> GetGenreWithVideos(int id)
        {
            return await Context.Set<Genre>().Include(a => a.Videos)
                .SingleOrDefaultAsync(a => a.Id == id);
        }
    }
}