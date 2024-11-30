using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Interfaces;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordManagerController : ControllerBase
    {
        private readonly IPasswordManagementService _passwordManagementService;
        public PasswordManagerController(IPasswordManagementService passwordManagementService)
        {
            _passwordManagementService = passwordManagementService;
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _passwordManagementService.ResetPasswordAsync(resetPasswordDto);
            return Ok(result);
        }

        [HttpPut("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _passwordManagementService.ChangePasswordAsync(changePasswordDto);
            return Ok(result);
        }

        [HttpPost("reset-password-request")]
        public async Task<IActionResult> ResetPasswordRequestAsync(string email)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _passwordManagementService.ResetPasswordRequestAsync(email);
            return Ok(result);
        }

        [HttpPost("verify-password-reset-token")]
        public async Task<IActionResult> VerifyResetPasswordRequestAsync(ConfirmEmailDto confirmEmailDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // Call the service to verify the email
            var result = await _passwordManagementService.VerifyResetPasswordRequestAsync(confirmEmailDto);

            // Check if the verification failed
            if (!result.IsRequestVerified)
                return BadRequest(result.Message);
            return Ok(result);
        }
    }
}
