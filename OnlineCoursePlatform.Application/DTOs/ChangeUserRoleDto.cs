using System.ComponentModel.DataAnnotations;

namespace OnlineCoursePlatform.Application.DTOs
{
    public class ChangeUserRoleDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
