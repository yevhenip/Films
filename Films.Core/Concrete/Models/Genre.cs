﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

 namespace Films.Core.Concrete.Models
{
    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<VideoGenre> Videos { get; set; }

        public Genre()
        {
            Videos = new Collection<VideoGenre>();
        }
    }
}