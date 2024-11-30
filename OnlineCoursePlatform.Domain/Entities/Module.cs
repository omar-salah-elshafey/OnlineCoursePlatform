using System.ComponentModel.DataAnnotations;

namespace OnlineCoursePlatform.Domain.Entities
{
    public class Module
    {
        public int Id { get; private set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; private set; }

        [MaxLength(1000)]
        public string Description { get; private set; }

        public int Order { get; private set; }

        public int CourseId { get; private set; }
        public Course Course { get; private set; }

        public List<Lesson> Lessons { get; private set; } = new List<Lesson>();
    }
}
