using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OnlineCoursePlatform.Domain.Entities
{
    public class User : IdentityUser
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow.ToLocalTime();
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime DateOfBirth { get; set; }
        public List<RefreshToken>? RefreshTokens { get; set; }
        public List<Course> Courses { get; private set; } = new List<Course>(); // Courses created by the instructor
        public List<Enrollment> Enrollments { get; private set; } = new List<Enrollment>(); // Courses enrolled by the student
    }
}
