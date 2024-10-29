using Business_Layer.Services;
using Data_Access_Layer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModelDTO model)
        {
            var result = await _authService.RegisterUser(model);
            if (result)
                return Ok(new { Status = "Success", Message = "User registered successfully!" });
            else
                return StatusCode(500, new { Status = "Error", Message = "User registration failed!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModelDTO model)
        {
            var token = await _authService.LoginUser(model);
            if (token != null)
            {
                return Ok(new { token });
            }
            return Unauthorized();
        }
    }
}
