﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

 namespace Films.Api.Responses
{
    public class GenreResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public ICollection<int> VideosId { get; set; }

        public GenreResponse()
        {
            VideosId = new Collection<int>();
        }
    }
}