namespace OnlineCoursePlatform.Domain.Entities
{
    public class Enrollment
    {
        public int Id { get; private set; }
        public string StudentId { get; private set; }
        public User Student { get; private set; }
        public int CourseId { get; private set; }
        public Course Course { get; private set; }
        public DateTime EnrollmentDate { get; private set; }
        // You might want to add a Progress property (e.g., percentage complete)
        public int Progress { get; private set; }
        public DateTime? CompletionDate { get; private set; } // Tracks completion date if applicable
        public bool IsCompleted { get; private set; } // Tracks whether the course is completed
    }
}
