using System.ComponentModel.DataAnnotations;

namespace OnlineCoursePlatform.Application.DTOs
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
