using System.ComponentModel.DataAnnotations;

namespace OnlineCoursePlatform.Application.Features.UserManagement.Dtos
{
    public class UpdateUserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
    }
}
