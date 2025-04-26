using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortManagementSystem.BLL.Dto_s;
using PortManagementSystem.BLL.Managers;

namespace PortManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("add")]
        public IActionResult AddUser([FromBody] UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("Invalid user data");
            }
            _userService.AddUser(userDTO);
            return Ok("User added successfully");
        }

        [HttpPut("edit/{id}")]
        public IActionResult EditUser(int id, [FromBody] UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("Invalid user data");
            }
            _userService.EditUser(id, userDTO);
            return Ok("User updated successfully");
        }

        [HttpDelete("remove/{id}")]
        public IActionResult RemoveUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid user ID");
            }
            _userService.RemoveUser(id);
            return Ok("User removed successfully");
        }
    }
}
