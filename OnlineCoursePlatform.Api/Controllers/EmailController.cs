using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Interfaces;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> VerifyEmail(ConfirmEmailDto confirmEmailDto)
        {
            // Validate the input fields (UserName and token)
            if (string.IsNullOrEmpty(confirmEmailDto.Email) || string.IsNullOrEmpty(confirmEmailDto.Token))
                return BadRequest(new { Message = "User ID and token are required." });

            // Call the service to verify the email
            var result = await _emailService.VerifyEmail(confirmEmailDto);

            // Check if the verification failed
            if (!result.IsConfirmed)
                return BadRequest(new { result.Message });

            // Return a success response
            return Ok(new
            {
                result.Message,
                result.IsConfirmed
            });
        }

        [HttpPost("resend-confirmation-email")]
        public async Task<IActionResult> ResendConfirmationEmail(string UserName)
        {
            var result = await _emailService.ResendEmailConfirmationTokenAsync(UserName);

            return Ok(result.Message);
        }
    }
}
