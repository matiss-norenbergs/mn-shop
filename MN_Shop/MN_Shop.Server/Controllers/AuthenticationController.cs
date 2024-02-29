using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MN_Shop.Server.Helpers;
using MN_Shop.Server.Models;
using MN_Shop.Server.Services;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MN_Shop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthenticationController(UserService userService) => _userService = userService;

        [HttpPost("login")]
        public async Task<ActionResult<bool>> Login([FromBody] AuthenticationData authData)
        {
            try
            {
                var userCollection = _userService.GetUserCollection();
                if (!userCollection.TryGetValue(authData.Email, out var userDetails) || userDetails.Password != UserHelper.EncryptPassword(authData.Password))
                    return BadRequest(false);

                var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, userDetails.Id.ToString()),
                    new(ClaimTypes.Name, userDetails.Name),
                    new(ClaimTypes.Surname, userDetails.Surname),
                    new(ClaimTypes.Email, userDetails.Email),
                    new(ClaimTypes.Role, userDetails.Role.ToString())
                };

                var identity = new ClaimsIdentity(claims, "MNCookieAuthenticationScheme");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(principal);

                var userData = new Dictionary<string, string>
                {
                    { "Name", userDetails.Name },
                    { "Surname", userDetails.Surname },
                    { "Email", userDetails.Email }
                };

                return Ok(userData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<ActionResult<bool>> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
