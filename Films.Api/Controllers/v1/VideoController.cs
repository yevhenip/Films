using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Films.Api.Requests;
using Films.Api.Responses;
using Films.Core.Abstract.Managers;
using Films.Core.Concrete.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Films.Api.Controllers.v1
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VideosController : Controller
    {
        private readonly IAuthorManager _authorManager;
        private readonly IGenreManager _genreManager;
        private readonly IMapper _mapper;
        private readonly IVideoManager _videoManager;
        private readonly UserManager<User> _userManager;

        public VideosController(IVideoManager videoManager, IAuthorManager authorManager,
            IGenreManager genreManager, UserManager<User> userManager, IMapper mapper)
        {
            _genreManager = genreManager;
            _authorManager = authorManager;
            _videoManager = videoManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        private async Task<bool> IsUserAdmin()
        {
            return await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(HttpContext.User), "admin");
        }

        private async Task<bool> IsUser()
        {
            return await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(HttpContext.User), "user");
        }

        //GET api/v1/videos
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllVideos()
        {
            var videos = await _videoManager.GetVideosWithGenres();
            var response = _mapper.Map<IEnumerable<VideoResponse>>(videos);
            return Ok(response);
        }

        //GET api/v1/videos/id
        [AllowAnonymous]
        [HttpGet("{id:min(1)}")]
        public async Task<IActionResult> GetVideoById([FromRoute] int id)
        {
            var video = await _videoManager.GetVideoWithGenres(id);
            if (video == null)
            {
                return BadRequest("Video not found");
            }

            var response = _mapper.Map<VideoResponse>(video);
            return Ok(response);
        }

        //POST api/v1/
        [HttpPost]
        public async Task<IActionResult> AddVideo([FromBody] VideoRequest videoRequest)
        {
            if (!await _genreManager.IsGenresIdValid(videoRequest.GenresId))
            {
                return BadRequest("Genres is Invalid");
            }

            if (!await IsUser())
            {
                return Forbid();
            }

            var video = _mapper.Map<Video>(videoRequest);
            video.LastUpdated = DateTime.Now;
            var author = await _authorManager.GetAuthor(videoRequest.AuthorId);
            if (author == null)
            {
                return BadRequest("Author is Invalid");
            }

            video.UserId = _userManager.GetUserId(HttpContext.User);
            video.AuthorId = author.Id;
            _videoManager.AddVideo(video);
            await _videoManager.SaveChangesAsync();
            var response = _mapper.Map<VideoResponse>(video);
            return CreatedAtAction(nameof(GetVideoById), new {id = video.Id}, response);
        }

        //DELETE api/videos/id

        [HttpDelete("{id:min(1)}")]
        public async Task<IActionResult> DeleteVideoById([FromRoute] int id)
        {
            var video = await _videoManager.GetVideoWithGenres(id);
            if (video == null)
            {
                return BadRequest("Video not found");
            }

            if (_userManager.GetUserId(HttpContext.User) != video.UserId && !await IsUserAdmin()
                                                                         && !await IsUser())
            {
                return Forbid();
            }

            _videoManager.RemoveVideo(video);
            await _videoManager.SaveChangesAsync();
            return Ok();
        }

        //PUT api/videos/id
        [HttpPut("{id:min(1)}")]
        public async Task<IActionResult> UpdateVideo([FromRoute] int id, [FromBody] VideoRequest videoRequest)
        {
            if (id != videoRequest.Id)
            {
                return BadRequest("Wrong Id");
            }

            var video = await _videoManager.GetVideoWithGenres(id);
            if (video == null)
            {
                return BadRequest("Video not found");
            }

            if (_userManager.GetUserId(HttpContext.User) != video.UserId)
            {
                return Forbid();
            }

            if (videoRequest.AuthorId != video.AuthorId)
            {
                var author = await _authorManager.GetAuthor(videoRequest.AuthorId);
                if (author == null)
                {
                    return BadRequest("Author is Invalid");
                }

                video.AuthorId = author.Id;
            }

            if (!await _genreManager.IsGenresIdValid(videoRequest.GenresId))
            {
                return BadRequest("Genres is Invalid");
            }

            _mapper.Map(videoRequest, video);
            video.LastUpdated = DateTime.Now;
            await _videoManager.SaveChangesAsync();
            var response = _mapper.Map<VideoResponse>(video);
            return Ok(response);
        }
    }
}