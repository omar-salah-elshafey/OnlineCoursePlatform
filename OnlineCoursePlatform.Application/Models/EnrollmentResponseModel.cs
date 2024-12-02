namespace OnlineCoursePlatform.Application.Models
{
    public class EnrollmentResponseModel
    {
        public int Id { get; set; }
        public string StudentId { get; set; } 
        public string StudentName { get; set; } 
        public int CourseId { get; set; } 
        public string CourseTitle { get; set; } 
        public DateTime EnrollmentDate { get; set; } 
        public int Progress { get; set; } // Progress percentage
        public DateTime? CompletionDate { get; set; } 
        public bool IsCompleted { get; set; } 
        public string? Message { get; set; }
    }
}
