using System.Linq;
using AutoMapper;
using Films.Api.Requests;
using Films.Core.Concrete.Models;

namespace Films.Api.Mapper.AfterMaps
{
    public class UpdateVideos : IMappingAction<GenreRequest, Genre>
    {
        public void Process(GenreRequest genreRequest, Genre genre, ResolutionContext context)
        {
            var removedVideos = genre.Videos.Where(v => !genreRequest.VideosId.Contains(v.VideoId)).ToList();
            var addedVideos = genreRequest.VideosId
                .Where(id => genre.Videos
                    .All(v => v.VideoId != id))
                .Select(id => new VideoGenre {VideoId = id, GenreId = genre.Id}).ToList();
            
            removedVideos.ForEach(v => genre.Videos.Remove(v));
            addedVideos.ForEach(v => genre.Videos.Add(v));
        }
    }
}