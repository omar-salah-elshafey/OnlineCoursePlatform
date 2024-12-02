using System.ComponentModel.DataAnnotations;

namespace OnlineCoursePlatform.Application.Features.UserManagement.Dtos
{
    public class ChangeUserRoleDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
