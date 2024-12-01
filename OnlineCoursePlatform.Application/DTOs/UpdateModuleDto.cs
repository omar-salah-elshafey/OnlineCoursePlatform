using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.DTOs
{
    public record UpdateModuleDto(string Title, string Description, int Order, int CourseId);
}
