﻿namespace Films.Core.Concrete.Models
{
    public class VideoGenre
    {
        public int VideoId { get; set; }
        public Video Video { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}