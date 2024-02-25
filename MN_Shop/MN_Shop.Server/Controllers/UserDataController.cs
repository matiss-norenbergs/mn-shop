using Microsoft.AspNetCore.Mvc;
using MN_Shop.Server.Helpers;
using MN_Shop.Server.Models;
using MN_Shop.Server.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MN_Shop.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserDataController : ControllerBase
    {
        private readonly UserService _userService;

        public UserDataController(UserService userService) => _userService = userService;

        // GET: api/<UserDataController>
        [HttpGet]
        public ActionResult<IEnumerable<UserData>> Get()
        {
            try
            {
                var userList = _userService.GetUserList();
                if (userList == null)
                    return NotFound();

                return userList;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<UserDataController>/5
        [HttpGet("{id}")]
        public ActionResult<UserData> Get(long id)
        {
            try
            {
                var user = _userService.GetUserData(id);
                if (user == null)
                    throw new Exception("Error getting user data");

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<UserDataController>
        [HttpPost]
        public async Task<ActionResult<UserData>> Post([FromBody] UserData userData)
        {
            try
            {
                if (!UserHelper.IsValidUserData(userData))
                    return BadRequest();

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

        [HttpPost("login")]
        public async Task<ActionResult<bool>> UserLogin([FromBody] UserData userData)
        {
            try
            {
                var userCollection = _userService.GetUserCollection();
                if (userCollection.TryGetValue(userData.Email, out var userDetails) && userDetails.Password == UserHelper.EncryptPassword(userData.Password))
                    return Ok(true);

                return BadRequest(false);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<UserDataController>/5
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
