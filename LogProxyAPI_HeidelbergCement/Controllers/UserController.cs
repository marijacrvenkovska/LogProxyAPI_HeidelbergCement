using LogProxyAPI.UserManagement.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LogProxyAPI_HeidelbergCement.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //Endpoint to provide the user
        [AllowAnonymous]
        [HttpPost("api/authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateUser model)
        {
            var user = await _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Invalid Username of Password" });

            return Ok(user);
        }


    }
}
