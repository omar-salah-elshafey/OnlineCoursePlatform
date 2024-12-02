using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Features.LessonFeature.Dtos;
using OnlineCoursePlatform.Application.Features.ModuleFeature.Dtos;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;

namespace OnlineCoursePlatform.Application.Features.ModuleFeature.Queries.GetAllModules
{
    public class GetAllModulesQueryHandler(IModuleRepository moduleRepository, ILogger<GetAllModulesQueryHandler> logger) 
            : IRequestHandler<GetAllModuleQuery, List<ModuleResponseModel>>
        {

            public async Task<List<ModuleResponseModel>> Handle(GetAllModuleQuery request, CancellationToken cancellationToken)
            {
                var modules = await moduleRepository.GetAllModulesAsync();
                if (!modules.Any() || modules is null)
                {
                    logger.LogWarning("No Modules were found.");
                    return new List<ModuleResponseModel>();
                }
                var moduleResponseModel = new List<ModuleResponseModel>();
                foreach (var module in modules)
                {
                    moduleResponseModel.Add(new ModuleResponseModel
                    {
                        Id = module.Id,
                        Title = module.Title,
                        Description = module.Description,
                        Order = module.Order,
                        CourseName = module.Course.Title,
                        CourseId = module.CourseId,
                        Lessons = module.Lessons.Select(l => new LessonResponseModel
                        {
                            Id = l.Id,
                            Title = l.Title,
                            Content = l.Content,
                            ModuleName = l.Module.Title,
                            ModuleId = l.ModuleId,
                            Order = l.Order
                        }).ToList()

                    });
                }
                logger.LogInformation("Returning Modules Details..");
                return moduleResponseModel;
            }
        }

    }
