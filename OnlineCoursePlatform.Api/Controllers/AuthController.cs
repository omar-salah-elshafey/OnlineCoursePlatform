using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Features.Authentication.Commands.Login;
using OnlineCoursePlatform.Application.Features.Authentication.Commands.Logout;
using OnlineCoursePlatform.Application.Features.Authentication.Commands.RegisterUser;
using OnlineCoursePlatform.Application.Features.TokenManagement.Commands.RefreshtheToken;
using OnlineCoursePlatform.Application.Interfaces;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ICookieService _cookieService;
        private readonly IMediator _mediator;
        public AuthController(UserManager<User> userManager,
            ICookieService cookieService, IMediator mediator)
        {
            _userManager = userManager;
            _cookieService = cookieService;
            _mediator = mediator;
        }

        [HttpPost("register-student")]
        public async Task<IActionResult> RegisterReaderAsync([FromBody] RegistrationDto registrationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _mediator.Send(new RegisterUserCommand(registrationDto, "Student"));

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
            var result = await _mediator.Send(new RegisterUserCommand(registrationDto, "Instructor"));

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
            var result = await _mediator.Send(new RegisterUserCommand(registrationDto, "Admin"));

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
            var result = await _mediator.Send(new LoginCommand(loginDto));
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

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
            var result = await _mediator.Send(new LogoutCommand(refreshToken, userId));
            if (!result)
                return BadRequest(result);
            return Ok("Successfully logged out");
        }

        [HttpGet("refreshtoken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var result = await _mediator.Send(new RefreshTokenCommand(refreshToken));

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
