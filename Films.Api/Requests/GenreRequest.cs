using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Films.Api.Requests
{
    public class GenreRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public ICollection<int> VideosId { get; set; }

        public GenreRequest()
        {
            VideosId = new Collection<int>();
        }
    }
}