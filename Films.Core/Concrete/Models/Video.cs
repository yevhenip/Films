using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Films.Core.Concrete.Models
{
    public class Video
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<VideoGenre> Genres { get; set; }

        public decimal Price { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdated { get; set; }

        public Video()
        {
            Genres = new Collection<VideoGenre>();
        }
    }
}