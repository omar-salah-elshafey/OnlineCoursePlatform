using OnlineCoursePlatform.Application.Features.Authentication.Dtos;
using OnlineCoursePlatform.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace OnlineCoursePlatform.Application.Interfaces
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> CreateJwtTokenAsync(User user);
        Task<AuthResponseModel> RefreshTokenAsync(string token);
        Task<bool> RevokeRefreshTokenAsync(string token);
        Task<RefreshToken> GenerateRefreshToken();
    }
}
