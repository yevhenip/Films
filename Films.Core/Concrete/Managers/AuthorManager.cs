using System.Collections.Generic;
using System.Threading.Tasks;
using Films.Core.Abstract.Managers;
using Films.Core.Abstract.Repositories;
using Films.Core.Concrete.Models;

namespace Films.Core.Concrete.Managers
{
    public class AuthorManager : IAuthorManager
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorManager(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public int SaveChanges()
        {
            return _authorRepository.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _authorRepository.SaveChangesAsync();
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await _authorRepository.GetAllAsync();
        }

        public async Task<Author> GetAuthor(int id)
        {
            return await _authorRepository.SingleOrDefaultAsync(v => v.Id == id);
        }

        public void AddAuthor(Author author)
        {
            _authorRepository.Add(author);
        }

        public  void RemoveAuthor(Author author)
        { 
            _authorRepository.Remove(author);
        }

        public async Task<List<Author>> GetAuthorsWithVideos()
        {
            return await _authorRepository.GetAuthorsWithVideos();
        }

        public async Task<Author> GetAuthorWithVideos(int id)
        {
            return await _authorRepository.GetAuthorWithVideos(id);
        }
    }
}