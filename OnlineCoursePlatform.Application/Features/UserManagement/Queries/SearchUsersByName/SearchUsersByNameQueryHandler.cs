using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Features.UserManagement.Dtos;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.UserManagement.Queries.SearchUsersByName
{
    public class SearchUsersByNameQueryHandler(UserManager<User> _userManager, ILogger<SearchUsersByNameQueryHandler> logger)
        : IRequestHandler<SearchUsersByNameQuery, List<UserResponseModel>>
    {
        public async Task<List<UserResponseModel>> Handle(SearchUsersByNameQuery request, CancellationToken cancellationToken)
        {
            var keyword = request.Keyword;
            logger.LogInformation("Searching Users with keyword: {Keyword}", keyword);
            var users = await _userManager.Users
                .Where(u => u.UserName.Contains(keyword) ||
                            (u.FirstName != null && u.FirstName.Contains(keyword)) ||
                            (u.LastName != null && u.LastName.Contains(keyword)))
                .ToListAsync();
            if (users.Count == 0)
            {
                logger.LogWarning("No users found with keyword: {Keyword}", keyword);
                return new List<UserResponseModel>();
            }
            logger.LogInformation("Returning Users with keyword: {Keyword}", keyword);
            var userResponseModels = new List<UserResponseModel>();

            foreach (var user in users)
            {
                userResponseModels.Add(new UserResponseModel
                {
                    Id = user.Id,
                    Name = $"{user.FirstName} {user.LastName}",
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault(),
                });
            }

            return userResponseModels;
        }
    }
}
