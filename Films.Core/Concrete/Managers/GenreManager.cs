using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Films.Core.Abstract.Managers;
using Films.Core.Abstract.Repositories;
using Films.Core.Concrete.Models;

namespace Films.Core.Concrete.Managers
{
    public class GenreManager : IGenreManager
    {
        private readonly IGenreRepository _genreRepository;

        public GenreManager(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public int SaveChanges()
        {
            return _genreRepository.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _genreRepository.SaveChangesAsync();
        }

        public async Task<List<Genre>> GetAllGenres()
        {
            return await _genreRepository.GetAllAsync();
        }

        public async Task<Genre> GetGenre(int id)
        {
            return await _genreRepository.SingleOrDefaultAsync(g => g.Id == id);
        }

        public void AddGenre(Genre genre)
        {
            _genreRepository.Add(genre);
        }

        public void RemoveGenre(Genre genre)
        {
            _genreRepository.Remove(genre);
        }

        public async Task<List<Genre>> GetRangeOfGenres(IEnumerable<int> ids)
        {
            var genres = await _genreRepository.GetAllAsync();
            return ids.Select(id => genres.SingleOrDefault(g => g.Id == id)).ToList();
        }

        public async Task<bool> IsGenresIdValid(IEnumerable<int> genresToValidate)
        {
            var genresInBd = await _genreRepository.GetAllAsync();
            var genres = genresToValidate.ToList();
            var setOfGenres = genres.ToHashSet();
            return genres.Count == setOfGenres.Count &&
                   genres.Select(genre => genresInBd.Any(genre1 => genre1.Id == genre))
                       .All(flag => flag);
        }

        public async Task<List<Genre>> GetGenresWithVideos()
        {
            return await _genreRepository.GetGenresWithVideos();
        }

        public async Task<Genre> GetGenreWithVideos(int id)
        {
            return await _genreRepository.GetGenreWithVideos(id);
        }
    }
}