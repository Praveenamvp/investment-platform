using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Models.Request;

namespace InvestmentApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddUser(UserRegisterRequest userRegisterRequest)
        {
            if (await _userService.Add(userRegisterRequest))
            {
                return Ok("User");
            }
            return BadRequest("Failed to add user");

        }
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDTO>> GetAllShowTimes(UserRequest userRequest)
        {
            var user = await _userService.LoginUser(userRequest);

            if (user != null)
            {
                return Ok(user);
            }
            throw new Exception();

        }
    }
}
