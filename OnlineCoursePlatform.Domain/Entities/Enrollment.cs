namespace OnlineCoursePlatform.Domain.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public User Student { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public DateTime EnrollmentDate { get; set; }
        // You might want to add a Progress property (e.g., percentage complete)
        public int Progress { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? CompletionDate { get; set; } // Tracks completion date if applicable
        public bool IsCompleted { get; set; } // Tracks whether the course is completed
    }
}
