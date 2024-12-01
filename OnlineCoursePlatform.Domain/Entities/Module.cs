using System.ComponentModel.DataAnnotations;

namespace OnlineCoursePlatform.Domain.Entities
{
    public class Module
    {
        public int Id { get;  set; }

        [Required]
        [MaxLength(255)]
        public string Title { get;  set; }

        [MaxLength(1000)]
        public string Description { get;  set; }
        [Required]
        public int Order { get;  set; }

        public int CourseId { get;  set; }
        public Course Course { get;  set; }
        public bool IsDeleted {  get; set; }

        public List<Lesson>? Lessons { get;  set; } = new List<Lesson>();
    }
}
