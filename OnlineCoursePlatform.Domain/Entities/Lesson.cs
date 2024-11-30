using System.ComponentModel.DataAnnotations;

namespace OnlineCoursePlatform.Domain.Entities
{
    public class Lesson
    {
        public int Id { get; private set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; private set; }

        public string Content { get; private set; } // Could be text, video URL, etc.

        public int Order { get; private set; }

        public int ModuleId { get; private set; }
        public Module Module { get; private set; }
    }
}
