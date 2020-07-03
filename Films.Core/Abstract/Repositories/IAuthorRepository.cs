using System.Collections.Generic;
using System.Threading.Tasks;
using Films.Core.Concrete.Models;

namespace Films.Core.Abstract.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<List<Author>> GetAuthorsWithVideos();
        Task<Author> GetAuthorWithVideos(int id);
    }
}