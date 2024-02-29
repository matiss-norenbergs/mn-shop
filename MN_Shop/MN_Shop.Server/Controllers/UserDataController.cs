using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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
    public class UserDataController : ControllerBase
    {
        private readonly UserService _userService;

        public UserDataController(UserService userService) => _userService = userService;

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<UserData>> Get()
        {
            try
            {
                var userList = _userService.GetUserList();
                if (userList == null)
                    return NotFound();

                userList.ForEach(x => x.Password = string.Empty);

                return userList;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<UserData> Get(long id)
        {
            try
            {
                var user = _userService.GetUserData(id);
                if (user == null)
                    throw new Exception("Error getting user data");

                user.Password = string.Empty;

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<UserData>> Post([FromBody] UserData userData)
        {
            try
            {
                if (!UserHelper.IsValidUserData(userData, userData.Id == 0))
                    return BadRequest();

                if (userData.Id > 0)
                {
                    var prevUserData = _userService.GetUserData(userData.Id);
                    if (prevUserData == null)
                        throw new Exception("Error getting user data!");

                    userData.Password = prevUserData.Password;
                }

                var resp = _userService.SetUserData(userData);
                if (resp <= 0)
                    return BadRequest();

                return Ok(userData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var resp = _userService.DeleteUser(id);
                if (!resp)
                    throw new Exception("Error deleting user data");

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
