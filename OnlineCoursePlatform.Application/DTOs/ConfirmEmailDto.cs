using System.ComponentModel.DataAnnotations;

namespace OnlineCoursePlatform.Application.DTOs
{
    public class ConfirmEmailDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
