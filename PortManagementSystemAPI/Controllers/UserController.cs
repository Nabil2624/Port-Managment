using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortManagementSystem.BLL.Dto_s;
using PortManagementSystem.BLL.Managers;
using System.Security.Claims;

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
        [HttpPost("addUser")]
        public IActionResult AddUser([FromBody] UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("Invalid user data");
            }
            _userService.AddUser(userDTO);
            return Ok("User added successfully");
        }

        [HttpPost("addAdmin")]
        public IActionResult AddAdmin([FromBody] AdminDTO adminDTO)
        {
            if (adminDTO == null)
            {
                return BadRequest("Invalid user data");
            }
            _userService.AddAdmin(adminDTO);
            return Ok("Admin added successfully");
        }

        [HttpPut("edit/{id}")]
        public IActionResult EditUser(int id, [FromBody] UserEditDTO userDTO)
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

        [AllowAnonymous]
        [HttpPut("editSelf")]
        public IActionResult UserEditSelf([FromBody] UserEditHisSelfDTO userDTO)
        {
            int UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (userDTO == null)
            {
                return BadRequest("Invalid user data");
            }
            _userService.UserEditHisSelf(userDTO, UserId);
            return Ok("User updated successfully");
        }

        [AllowAnonymous]
        [HttpDelete("removeSelf")]
        public IActionResult UserRemoveHisSelf()
        {
            int UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (UserId <= 0)
            {
                return BadRequest("Invalid user ID");
            }
            _userService.UserRemoveHisSelf(UserId);
            return Ok("User removed successfully");
        }
    }
}
