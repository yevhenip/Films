using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Films.Api.Requests.RoleRequests;
using Films.Api.Responses;
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
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public RolesController(RoleManager<IdentityRole> roleManager, IMapper mapper, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        private async Task<bool> IsUserAdmin()
        {
            return await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(HttpContext.User), "admin");
        }

        //GET api/roles
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var response = _mapper.Map<IEnumerable<RoleResponse>>(roles);

            return Ok(response);
        }

        //POST api/roles
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleRequest request)
        {
            if (!await IsUserAdmin())
            {
                return Forbid();
            }

            var role = _mapper.Map<IdentityRole>(request);
            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Role was successfully created.");
        }

        //POST api/roles/assign
        [HttpPost("assign")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return BadRequest("User does not exist.");
            }

            if (!await IsUserAdmin())
            {
                return Forbid();
            }

            if (!await _roleManager.RoleExistsAsync(request.RoleName))
            {
                return BadRequest("Role does not exist.");
            }

            await _userManager.AddToRoleAsync(user, request.RoleName);

            return Ok();
        }
        
        //POST api/roles/remove
        [HttpPost("remove")]
        public async Task<IActionResult> RemoveRole([FromBody] RemoveRoleRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return BadRequest("User does not exist.");
            }

            if (!await IsUserAdmin())
            {
                return Forbid();
            }

            if (!await _roleManager.RoleExistsAsync(request.RoleName))
            {
                return BadRequest("Role does not exist.");
            }

            await _userManager.RemoveFromRoleAsync(user, request.RoleName);

            return Ok();
        }

        //DELETE api/roles
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] RoleRequest request)
        {
            var roleInDb = await _roleManager.FindByNameAsync(request.Name);
            if (roleInDb == null)
            {
                return BadRequest("Role does not exist.");
            }

            if (!await IsUserAdmin())
            {
                return Forbid();
            }

            await _roleManager.DeleteAsync(roleInDb);

            return Ok();
        }
    }
}