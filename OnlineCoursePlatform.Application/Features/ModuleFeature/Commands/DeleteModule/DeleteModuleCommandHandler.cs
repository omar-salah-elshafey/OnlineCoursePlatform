using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Interfaces.IRepositories;
using OnlineCoursePlatform.Application.Models;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.ModuleFeature.Commands.DeleteModule
{
    public class DeleteModuleCommandHandler(IModuleRepository moduleRepository, ILogger<DeleteModuleCommandHandler> logger,
        UserManager<User> _userManager) : IRequestHandler<DeleteModuleCommand, DeleteResponseModel>
    {
        public async Task<DeleteResponseModel> Handle(DeleteModuleCommand request, CancellationToken cancellationToken)
        {
            var moduleId = request.ModuleId;
            var module = await moduleRepository.GetModuleByIdAsync(moduleId);
            if (module == null)
            {
                logger.LogError("Module Not Found.");
                return new DeleteResponseModel { IsDeleted = false, Message = "Module Not Found." };
            }

            var currentUserId = request.CurrentUserId;
            var currentUser = await _userManager.FindByIdAsync(currentUserId);
            var instructorID = module.Course.InstructorId;
            var isAdmin = await _userManager.IsInRoleAsync(currentUser, "Admin");
            if (!instructorID.Equals(currentUserId) && !isAdmin)
            {
                logger.LogError("Unauthorized action.");
                return new DeleteResponseModel
                {
                    IsDeleted = false,
                    Message = "You are not authorized to delete this Module."
                };
            }

            await moduleRepository.DeleteModuleAsync(module);
            await moduleRepository.SaveChangesAsync();
            logger.LogInformation("Course deleted successfully.");
            return new DeleteResponseModel { IsDeleted = true, Message = "Module Has been Deleted Succeessfully." };
        }
    }
}
