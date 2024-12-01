namespace OnlineCoursePlatform.Application.DTOs
{
    public record UpdateCourseDto(
        string Title,
        string Description,
        decimal Price,
        string InstructorId,  // To update the instructor if needed
        string? ThumbnailUrl,  // Optional
        bool IsPublished,
        List<ModuleDto>? Modules  // Optional modules to update
    );
}
