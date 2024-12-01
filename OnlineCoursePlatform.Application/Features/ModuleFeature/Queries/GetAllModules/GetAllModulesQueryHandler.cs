    using MediatR;
    using Microsoft.Extensions.Logging;
    using OnlineCoursePlatform.Application.Interfaces.IRepositories;
    using OnlineCoursePlatform.Application.Models;

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

                    });
                }
                logger.LogInformation("Returning Modules Details..");
                return moduleResponseModel;
            }
        }

    }
