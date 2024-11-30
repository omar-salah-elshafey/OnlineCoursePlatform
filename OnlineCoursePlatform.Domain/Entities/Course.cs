using System.ComponentModel.DataAnnotations;

namespace OnlineCoursePlatform.Domain.Entities
{
    public class Course
    {
        public int Id { get; private set; }
        [Required]
        [MaxLength(255)]
        public string Title { get; private set; }
        [MaxLength(1000)]
        public string Description { get; private set; }
        public string ThumbnailUrl { get; private set; } // Optional property for frontend integration
        public bool IsPublished { get; private set; } = false;
        public string InstructorId { get; private set; } // Link to User entity
        public User Instructor { get; private set; }
        public decimal Price { get; private set; }
        public List<Module> Modules { get; private set; } = new List<Module>();
    }
}
