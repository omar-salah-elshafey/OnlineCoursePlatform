using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Models;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.UserManagement.Queries.GetUsersByRole
{
    public class GetUsersByRoleQueryHandler(UserManager<User> _userManager, ILogger<GetUsersByRoleQueryHandler> _logger)
        : IRequestHandler<GetUsersByRoleQuery, List<UserResponseModel>>
    {
        public async Task<List<UserResponseModel>> Handle(GetUsersByRoleQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching users with role: {Role}", request.Role);
            var role = request.Role;
            var users = await _userManager.GetUsersInRoleAsync(role);
            if (users == null || users.Count == 0)
            {
                _logger.LogWarning("No users found with role: {Role}", request.Role);
                return new List<UserResponseModel>();
            }
            _logger.LogInformation("Returning users with role: {Role}", request.Role);
            return users.Select(u => new UserResponseModel
            {
                Id = u.Id,
                Name = $"{u.FirstName} {u.LastName}",
                UserName = u.UserName,
                Email = u.Email,
                Role = request.Role
            }).ToList();
        }
    }
}
