using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Films.Api.Requests
{
    public class VideoRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<int> GenresId { get; set; }

        public decimal Price { get; set; }

        public int AuthorId { get; set; }
        
        public string Description { get; set; }

        public VideoRequest()
        {
            GenresId = new Collection<int>();
        }
    }
}