using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MN_Shop.Server.Helpers;
using MN_Shop.Server.Models;
using MN_Shop.Server.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using MN_Shop.Server.ModelsJson;

namespace MN_Shop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(UserService userService, UserContext userContext) : ControllerBase
    {
        private readonly UserService _userService = userService;

        [HttpPost("login")]
        public async Task<ActionResult<bool>> Login([FromBody] AuthenticationJson authData)
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

                var userDataResponse = new Dictionary<string, object>
                {
                    { "Name", userDetails.Name },
                    { "Surname", userDetails.Surname },
                    { "Email", userDetails.Email }
                };

                if (userDetails.Role == EUserRole.Admin)
                    userDataResponse.Add("IsAdmin", true);

                return Ok(userDataResponse);
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

        [HttpPost("update")]
        public ActionResult UpdateUserData()
        {
            try
            {
                if (!userContext.IsAuthenticated)
                    return NoContent();

                var userDataResponse = new Dictionary<string, object>
                {
                    { "Name", userContext.User.Name },
                    { "Surname", userContext.User.Surname },
                    { "Email", userContext.User.Email }
                };

                if (userContext.User.Role == EUserRole.Admin)
                    userDataResponse.Add("IsAdmin", true);

                return Ok(userDataResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
