using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.ModuleFeature.Dtos
{
    public record UpdateModuleDto(string Title, string Description, int Order);
}
