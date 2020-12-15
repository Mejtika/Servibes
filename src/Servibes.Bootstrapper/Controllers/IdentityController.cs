using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Servibes.Bootstrapper.Models;

namespace Servibes.Bootstrapper.Controllers
{
    [ApiController]
    [Route("api/identity")]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUserInfo()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return Ok(user);
        }

        [HttpGet("{clientId}")]
        public async Task<IActionResult> GetClientUserInfo(Guid clientId)
        {
            var user = await _userManager.FindByIdAsync(clientId.ToString());
            var isClient = await _userManager.IsInRoleAsync(user, "Client");
            if (!isClient)
            {
                return Unauthorized();
            }

            return Ok(user);
        }
    }
}
