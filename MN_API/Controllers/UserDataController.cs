﻿using Microsoft.AspNetCore.Mvc;
using MN_API.Models;
using MN_API.Services;

namespace MN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDataController : ControllerBase
    {
        private readonly UserService _userService;

        public UserDataController(UserService userService) => _userService = userService;

        // GET: api/UserData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserData>>> GetUserDataSet()
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

        // GET: api/UserData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserData>> GetUserData(long id)
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

        // POST: api/UserData
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserData>> PostUserData(UserData userData)
        {
            try
            {
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

        // DELETE: api/UserData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserData(long id)
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
