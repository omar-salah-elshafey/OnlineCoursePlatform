namespace OnlineCoursePlatform.Application.Models
{
    public class LessonResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Order { get; set; }
        public string ModuleName { get; set; }
        public string? Message { get; set; }
    }
}
