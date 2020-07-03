using System.Linq;
using AutoMapper;
using Films.Api.Mapper.AfterMaps;
using Films.Core.Concrete.Models;
using Films.Api.Requests;
using Films.Api.Requests.AccountsRequests;
using Films.Api.Requests.RoleRequests;
using Films.Api.Requests.UserRequests;
using Films.Api.Responses;
using Microsoft.AspNetCore.Identity;

namespace Films.Api.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<VideoRequest, Video>()
                .ForMember(v => v.Id, opt
                    => opt.Ignore()).AfterMap<UpdateGenres>();
            CreateMap<Video, VideoResponse>()
                .ForMember(vr => vr.GenresId, opt
                    => opt.MapFrom(v => v.Genres.Select(id => id.GenreId)));


            CreateMap<AuthorRequest, Author>().ForMember(a => a.Id, opt
                => opt.Ignore());
            CreateMap<Author, AuthorResponse>()
                .ForMember(ar => ar.VideosId, opt
                    => opt.MapFrom(a => a.Videos.Select(v => v.Id)));

            CreateMap<GenreRequest, Genre>()
                .ForMember(g => g.Id, opt
                    => opt.Ignore()).AfterMap<UpdateVideos>();
            CreateMap<Genre, GenreResponse>()
                .ForMember(gr => gr.VideosId, opt
                    => opt.MapFrom(g => g.Videos.Select(v => v.VideoId)));

            CreateMap<RegisterRequest, User>();
            CreateMap<UserRequest, User>()
                .ForMember(u => u.Id, opt
                    => opt.Ignore());
            CreateMap<User, UserResponse>();
            
            CreateMap<RoleRequest, IdentityRole>();
            CreateMap<IdentityRole, RoleResponse>();
        }
    }
}