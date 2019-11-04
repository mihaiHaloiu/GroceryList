using System.Threading.Tasks;
using GroceryList.Bll.Models.Requests;
using GroceryList.Bll.Services;
using GroceryList.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroceryList.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
{
        private readonly IUserService _userService;
        private readonly IAuth _auth;

        public UsersController(IUserService userService, IAuth auth)
        {
            _userService = userService;
            _auth = auth;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]        
        public async Task<IActionResult> Authenticate([FromBody]UserLogin userParam)
        {
            var user = await _auth.Authenticate(userParam.Email, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet("testAuthorized")]
        public IActionResult TestAuthorized()
        {
            return Ok("Test Authorized Working");
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult TestAnonymous()
        {
            return Ok("Test Anonymous Working");
        }
    }
}