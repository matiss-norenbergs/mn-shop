using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MN_Shop.Server.Helpers;
using MN_Shop.Server.Models;
using MN_Shop.Server.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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
                var userCollection = _userService.GetUserCollectionByEmail();
                if (!userCollection.TryGetValue(authData.Email, out var userDetails) || userDetails.Password != UserHelper.EncryptPassword(authData.Password))
                    return BadRequest();

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

                var userDataResp = new Dictionary<string, object>
                {
                    { "Name", userDetails.Name },
                    { "Surname", userDetails.Surname },
                    { "Email", userDetails.Email }
                };

                if (userDetails.Role == EUserRole.Admin)
                    userDataResp.Add("IsAdmin", true);

                return Ok(userDataResp);
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

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("update")]
        public async Task<ActionResult<bool>> UpdateUserData()
        {
            try
            {   
                var userIdString = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                var userCollection = _userService.GetUserCollectionById();
                if (userIdString == null || !long.TryParse(userIdString, out var userId) || !userCollection.TryGetValue(userId, out var userData))
                    return BadRequest();

                var userDataResp = new Dictionary<string, object>
                {
                    { "Name", userData.Name },
                    { "Surname", userData.Surname },
                    { "Email", userData.Email }
                };

                if (userData.Role == EUserRole.Admin)
                    userDataResp.Add("IsAdmin", true);

                return Ok(userDataResp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
