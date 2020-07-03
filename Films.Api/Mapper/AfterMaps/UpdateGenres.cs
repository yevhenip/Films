using System.Linq;
using AutoMapper;
using Films.Api.Requests;
using Films.Core.Concrete.Models;

namespace Films.Api.Mapper.AfterMaps
{
    public class UpdateGenres : IMappingAction<VideoRequest, Video>
    {
        public void Process(VideoRequest videoRequest, Video video, ResolutionContext context)
        {
            var removedGenres = video.Genres.Where(g => !videoRequest.GenresId.Contains(g.GenreId)).ToList();
            var addedGenres = videoRequest.GenresId
                .Where(id => video.Genres
                    .All(g => g.GenreId != id))
                .Select(id => new VideoGenre {GenreId = id, VideoId = video.Id}).ToList();
            
            removedGenres.ForEach(g => video.Genres.Remove(g));
            addedGenres.ForEach(g => video.Genres.Add(g));
        }
    }
}