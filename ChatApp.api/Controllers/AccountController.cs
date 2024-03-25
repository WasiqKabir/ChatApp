using BusinessEntities.BindingModels;
using BusinessServices.Services.Account;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [Route("api")]
    public class Account : Controller
    {
        private readonly IAccount _accountService;

        public Account(IAccount accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginBindingModel model)
        {
            var response = await _accountService.Login(model);
            return Ok(response);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] SignupBindingModel model)
        {
            var response = await _accountService.Register(model);
            return Ok(response); 
        }
    }
}
