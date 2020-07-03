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
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthorsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthorManager _authorManager;
        private readonly UserManager<User> _userManager;

        public AuthorsController(IAuthorManager authorManager, UserManager<User> userManager,
            IMapper mapper)
        {
            _authorManager = authorManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        private async Task<bool> IsUserAdmin()
        {
            return await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(HttpContext.User), "admin");
        }

        //GET api/v1/authors
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _authorManager.GetAuthorsWithVideos();
            var response = _mapper.Map<IEnumerable<AuthorResponse>>(authors);
            return Ok(response);
        }

        //GET api/v1/authors/id
        [HttpGet("{id:min(1)}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAuthorById([FromRoute] int id)
        {
            var author = await _authorManager.GetAuthorWithVideos(id);
            if (author == null)
            {
                return BadRequest("Author not found");
            }

            var response = _mapper.Map<AuthorResponse>(author);
            return Ok(response);
        }

        //POST api/v1/authors
        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorRequest authorRequest)
        {
            if (!await IsUserAdmin())
            {
                return Forbid();
            }

            var author = _mapper.Map<Author>(authorRequest);
            _authorManager.AddAuthor(author);
            await _authorManager.SaveChangesAsync();
            var response = _mapper.Map<AuthorResponse>(author);
            return CreatedAtAction(nameof(GetAuthorById), new {id = author.Id}, response);
        }

        //DELETE api/v1/authors/id
        [HttpDelete("{id:min(1)}")]
        public async Task<IActionResult> DeleteAuthorById([FromRoute] int id)
        {
            if (!await IsUserAdmin())
            {
                return Forbid();
            }

            var author = await _authorManager.GetAuthorWithVideos(id);

            if (author == null)
            {
                return BadRequest("Author not found");
            }

            _authorManager.RemoveAuthor(author);
            await _authorManager.SaveChangesAsync();
            return Ok();
        }

        //PUT api/authors/id
        [HttpPut("{id:min(1)}")]
        public async Task<IActionResult> UpdateAuthor([FromRoute] int id, [FromBody] AuthorRequest authorRequest)
        {
            if (!await IsUserAdmin())
            {
                return Forbid();
            }

            if (id != authorRequest.Id)
            {
                return BadRequest("Wrong Id");
            }

            var author = await _authorManager.GetAuthorWithVideos(id);
            if (author == null)
            {
                return BadRequest("Author not found");
            }

            _mapper.Map(authorRequest, author);
            await _authorManager.SaveChangesAsync();
            var response = _mapper.Map<AuthorResponse>(author);
            return Ok(response);
        }
    }
}