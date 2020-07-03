using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Films.Api.Requests.UserRequests;
using Films.Api.Responses;
using Films.Core.Abstract.Managers;
using Films.Core.Concrete.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Films.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IVideoManager _videoManager;

        public UsersController(UserManager<User> userManager, IVideoManager videoManager, IMapper mapper)
        {
            _videoManager = videoManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        //GET api/users
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var response = _mapper.Map<IEnumerable<UserResponse>>(users);

            return Ok(response);
        }

        //GET api/users/id
        [AllowAnonymous]
        [HttpGet("{username}")]
        public async Task<IActionResult> GetUser([FromRoute] string username)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.NormalizedUserName == username.Normalize());
            if (user == null)
            {
                return BadRequest("User does not exist");
            }

            var response = _mapper.Map<UserResponse>(user);
            return Ok(response);
        }

        //PUT api/users/username
        [HttpPut("{username}")]
        public async Task<IActionResult> Edit([FromRoute] string username, [FromBody] UserRequest userRequest)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest("User does not exist");
            }

            if (_userManager.GetUserId(HttpContext.User) != user.Id)
            {
                return BadRequest("You have no rights");
            }

            if (userRequest.Id != user.Id)
            {
                return BadRequest("Wrong id");
            }

            _mapper.Map(userRequest, user);
            var response = _mapper.Map<UserResponse>(user);
            return Ok(response);
        }

        //PUT api/users
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                return BadRequest("User does not exist");
            }
            
            if (request.Email != user.Email)
            {
                return BadRequest("Wrong Email");
            }

            if (_userManager.GetUserId(HttpContext.User) != user.Id)
            {
                return BadRequest("You have no rights");
            }

            var result =
                await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        private async Task<bool> IsUserAdmin()
        {
            return await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(HttpContext.User), "admin");
        }

        //DELETE api/users
        [HttpDelete("{userName}")]
        public async Task<ActionResult> DeleteUser([FromRoute] string userName)
        {
            var currentUser = HttpContext.User;
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return BadRequest("User does not exist");
            }

            if (_userManager.GetUserId(currentUser) != user.Id && !await IsUserAdmin())
            {
                return BadRequest("You have no rights");
            }

            var videos = await _videoManager.GetVideosByUserId(user.Id);
            foreach (var video in videos)
            {
                _videoManager.RemoveVideo(video);
            }

            await _videoManager.SaveChangesAsync();
            await _userManager.DeleteAsync(user);
            return Ok();
        }
    }
}