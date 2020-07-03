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
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "admin")]
    public class GenresController : Controller
    {
        private readonly IGenreManager _genreManager;
        private readonly IMapper _mapper;
        private readonly IVideoManager _videoManager;
        private readonly UserManager<User> _userManager;

        public GenresController(IGenreManager genreManager, IVideoManager videoManager, UserManager<User> userManager,
            IMapper mapper)
        {
            _videoManager = videoManager;
            _mapper = mapper;
            _userManager = userManager;
            _genreManager = genreManager;
        }

        private async Task<bool> IsUserAdmin()
        {
            return await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(HttpContext.User), "admin");
        }

        //GET api/v1/genres
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _genreManager.GetGenresWithVideos();
            var response = _mapper.Map<IEnumerable<GenreResponse>>(genres);
            return Ok(response);
        }

        //GET api/v1/genres/id
        [AllowAnonymous]
        [HttpGet("{id:min(1)}")]
        public async Task<IActionResult> GetGenreById([FromRoute] int id)
        {
            var genre = await _genreManager.GetGenreWithVideos(id);
            if (genre == null)
            {
                return BadRequest("Genre not found");
            }

            var response = _mapper.Map<GenreResponse>(genre);
            return Ok(response);
        }

        //POST api/v1/genres
        [HttpPost]
        public async Task<IActionResult> AddGenre([FromBody] GenreRequest genreRequest)
        {
            if (!await IsUserAdmin())
            {
                return Forbid();
            }

            if (!await _videoManager.IsVideosIdValid(genreRequest.VideosId))
            {
                return BadRequest("Videos is Invalid");
            }

            var genre = _mapper.Map<Genre>(genreRequest);
            _genreManager.AddGenre(genre);
            await _genreManager.SaveChangesAsync();
            var response = _mapper.Map<GenreResponse>(genre);
            return CreatedAtAction(nameof(GetGenreById), new {id = genre.Id}, response);
        }

        //DELETE api/v1/genres/id
        [HttpDelete("{id:min(1)}")]
        public async Task<IActionResult> DeleteGenreById([FromRoute] int id)
        {
            if (!await IsUserAdmin())
            {
                return Forbid();
            }

            var genre = await _genreManager.GetGenreWithVideos(id);
            if (genre == null)
            {
                return BadRequest("Genre not found");
            }

            _genreManager.RemoveGenre(genre);
            await _genreManager.SaveChangesAsync();
            return Ok();
        }

        //PUT api/genres/id
        [HttpPut("{id:min(1)}")]
        public async Task<IActionResult> UpdateGenre([FromRoute] int id, [FromBody] GenreRequest genreRequest)
        {
            if (!await IsUserAdmin())
            {
                return Forbid();
            }

            if (id != genreRequest.Id)
            {
                return BadRequest("Wrong Id");
            }

            if (!await _videoManager.IsVideosIdValid(genreRequest.VideosId))
            {
                return BadRequest("Videos is Invalid");
            }

            var genre = await _genreManager.GetGenreWithVideos(id);
            if (genre == null)
            {
                return BadRequest("Genre not found");
            }

            _mapper.Map(genreRequest, genre);
            await _genreManager.SaveChangesAsync();
            var response = _mapper.Map<GenreResponse>(genre);
            return Ok(response);
        }
    }
}