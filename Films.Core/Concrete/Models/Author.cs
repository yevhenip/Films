using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Films.Core.Concrete.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public ICollection<Video> Videos { get; set; }
        
        public Author()
        {
            Videos = new Collection<Video>();
        }
    }
}