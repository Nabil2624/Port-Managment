using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortManagementSystem.BLL.Managers;

namespace PortManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private IUserService _userService;

        public AdminController(IUserService userService) {
            _userService = userService;
        }
        
        [HttpGet("Users")]
        public IActionResult GetAllUsers()
        {
            var found = _userService.GetAllUsers();
            return Ok(found);
        }

        [HttpGet("Admins")]
        public IActionResult GetAllAdmins()
        {
            var found = _userService.GetAllAdmins();
            return Ok(found);
        }
    }
}
