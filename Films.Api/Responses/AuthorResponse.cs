using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Films.Api.Responses
{
    public class AuthorResponse
    {
        public int Id { get; set; }
        
        public string FullName { get; set; }
        public ICollection<int> VideosId { get; set; }
        
        public AuthorResponse()
        {
            VideosId = new Collection<int>();
        }
    }
}