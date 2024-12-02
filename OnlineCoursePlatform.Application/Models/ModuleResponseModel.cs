namespace OnlineCoursePlatform.Application.Models
{
    public class ModuleResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public string CourseName { get; set; }
        public int CourseId { get; set; }
        public string Message { get; set; }

        public List<LessonResponseModel>? Lessons { get; set; }
    }
}
