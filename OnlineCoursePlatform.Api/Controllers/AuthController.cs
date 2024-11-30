using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Interfaces;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly ICookieService _cookieService;
        public AuthController(IAuthService authService, UserManager<User> userManager,
            ITokenService tokenService, ICookieService cookieService)
        {
            _authService = authService;
            _userManager = userManager;
            _tokenService = tokenService;
            _cookieService = cookieService;
        }

        [HttpPost("register-student")]
        public async Task<IActionResult> RegisterReaderAsync([FromBody] RegistrationDto registrationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.RegisterUserAsync(registrationDto, "Student");

            if (!result.IsAuthenticated)
                return Ok(result.Message);

            return Ok(new
            {
                result.Email,
                result.Username,
                result.Role,
                result.IsAuthenticated,
                result.IsConfirmed,
                result.Message,
            });
        }

        [HttpPost("register-instructor")]
        public async Task<IActionResult> RegisterAuthorAsync([FromBody] RegistrationDto registrationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.RegisterUserAsync(registrationDto, "Instructor");

            if (!result.IsAuthenticated)
                return Ok(result.Message);

            return Ok(new
            {
                result.Email,
                result.Username,
                result.Role,
                result.IsAuthenticated,
                result.IsConfirmed,
                result.Message,
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdminAsync([FromBody] RegistrationDto registrationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.RegisterUserAsync(registrationDto, "Admin");

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(new
            {
                result.Email,
                result.Username,
                result.Role,
                result.IsAuthenticated,
                result.IsConfirmed,
                result.Message,
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.LoginAsync(loginDto);
            var user = await _userManager.FindByNameAsync(loginDto.EmailOrUserName)
               ?? await _userManager.FindByEmailAsync(loginDto.EmailOrUserName);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            if (!string.IsNullOrEmpty(result.RefreshToken))
            {
                _cookieService.SetRefreshTokenCookie(result.RefreshToken, result.RefreshTokenExpiresOn);
            }
            _cookieService.SetUserIdCookie(user.Id);
            _cookieService.SetUserNameCookie(user.UserName);
            return Ok(new
            {
                result.AccessToken,
                result.ExpiresAt
            });
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var userId = Request.Cookies["userID"];
            var userName = Request.Cookies["UserName"];
            var result = await _authService.LogoutAsync(refreshToken, userId);
            if (!result)
                return BadRequest(result);
            return Ok("Successfully logged out");
        }

        [HttpGet("refreshtoken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var result = await _tokenService.RefreshTokenAsync(refreshToken);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            _cookieService.SetRefreshTokenCookie(result.RefreshToken, result.RefreshTokenExpiresOn);
            return Ok(new
            {
                result.AccessToken,
                result.ExpiresAt,
                result.RefreshToken,
                result.RefreshTokenExpiresOn,
            });
        }
    }
}
