using System.ComponentModel.DataAnnotations;

namespace OnlineCoursePlatform.Domain.Entities
{
    public class Course
    {
        public int Id { get;  set; }
        [Required]
        [MaxLength(255)]
        public string Title { get;  set; }
        [MaxLength(1000)]
        public string Description { get;  set; }
        public string? ThumbnailUrl { get;  set; } // Optional property for frontend integration
        public bool IsPublished { get;  set; } = false;
        public string InstructorId { get;  set; } // Link to User entity
        public User Instructor { get;  set; }
        public decimal Price { get;  set; }
        public bool IsDeleted { get; set; } = false;
        public List<Module>? Modules { get;  set; } = new List<Module>();
    }
}
