using OnlineCoursePlatform.Application.Features.ModuleFeature.Dtos;

namespace OnlineCoursePlatform.Application.Features.CourseFeature.Dtos
{
    public record UpdateCourseDto(
        string Title,
        string Description,
        decimal Price,
        string InstructorId,
        string? ThumbnailUrl, 
        bool IsPublished,
        List<ModuleDto>? Modules 
    );
}
