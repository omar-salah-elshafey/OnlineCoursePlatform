using System.ComponentModel.DataAnnotations;

namespace OnlineCoursePlatform.Domain.Entities
{
    public class Lesson
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; } // Could be text, video URL, etc.

        public bool IsDeleted { get; set; } = false;
        [Required]
        public int Order { get; set; }

        public int ModuleId { get; set; }
        public Module Module { get; set; }
    }
}
