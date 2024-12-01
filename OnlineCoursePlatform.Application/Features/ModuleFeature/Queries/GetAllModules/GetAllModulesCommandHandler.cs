using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.ModuleFeature.Queries.GetAllModules
{
    public class GetAllModulesCommandHandler(IModuleRepository moduleRepository, ILogger<GetAllModulesCommandHandler> logger) 
        : IRequestHandler<GetAllModuleCommand, List<ModuleResponseModel>>
    {

        public async Task<List<ModuleResponseModel>> Handle(GetAllModuleCommand request, CancellationToken cancellationToken)
        {
            var modules = await moduleRepository.GetAllModulesAsync();
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
            return moduleResponseModel;
        }
    }

}
