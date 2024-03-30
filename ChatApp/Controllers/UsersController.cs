using BusinessServices.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("other-users/{UserId}")]
        [HttpGet]
        public async Task<IActionResult> OtherUsers(string UserId)
        {
            var response = await _userService.GetOtherUsers(Guid.Parse(UserId));
            return Ok(response);
        }

        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> AllUsers()
        {
            var response = await _userService.GetAll();
            return Ok(response);
        }

        [Route("chatted/{userId}")]
        [HttpGet]
        public async Task<IActionResult> ChattedUsers(string userId)
        {
            var response = await _userService.ChattedUser(userId);
            return Ok(response);
        }

    }
}
