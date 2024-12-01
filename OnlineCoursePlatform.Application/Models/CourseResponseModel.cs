namespace OnlineCoursePlatform.Application.Models
{
    public class CourseResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }
        public decimal Price { get; set; }
        public string? Message { get; set; }
        public string ThumbnailUrl { get; set; }
        public string InstructorId { get; set; }
        public string InstructorName { get; set; } // Combine FirstName and LastName
        public List<ModuleResponseModel> Modules { get; set; } // Optionally include modules
    }
}
