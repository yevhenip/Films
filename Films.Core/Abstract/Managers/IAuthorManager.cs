using System.Collections.Generic;
using System.Threading.Tasks;
using Films.Core.Concrete.Models;

namespace Films.Core.Abstract.Managers
{
    public interface IAuthorManager : IManager
    {
        Task<List<Author>> GetAllAuthors();
        Task<Author> GetAuthor(int id);
        void AddAuthor(Author author);
        void RemoveAuthor(Author author);
        Task<List<Author>> GetAuthorsWithVideos();
        Task<Author> GetAuthorWithVideos(int id);
    }
}