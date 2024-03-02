using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MN_Shop.Server.Helpers;
using MN_Shop.Server.Models;
using MN_Shop.Server.ModelsJson;
using MN_Shop.Server.Services;

namespace MN_Shop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDataController(UserService userService, UserContext userContext) : ControllerBase
    {
        private readonly UserService _userService = userService;

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<UserDataJson>> Get()
        {
            try
            {
                var userList = _userService.GetUserList();
                if (userList == null)
                    return NotFound();

                userList.ForEach(x => x.Password = string.Empty);

                var jsonData = UserHelper.GetUserDataJson(userList, userContext.User);

                return jsonData;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<UserDataJson> Get(long id)
        {
            try
            {
                var user = _userService.GetUserData(id);
                if (user == null)
                    throw new Exception("Error getting user data!");

                user.Password = string.Empty;

                var jsonData = UserHelper.GetUserDataJson(user, new UserData());

                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<UserData> Post([FromBody] UserData userData)
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
        public ActionResult Delete(long id)
        {
            try
            {
                var response = _userService.DeleteUser(id);
                if (!response)
                    throw new Exception("Error deleting user data!");

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
