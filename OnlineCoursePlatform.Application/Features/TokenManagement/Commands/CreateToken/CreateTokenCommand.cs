using MediatR;
using OnlineCoursePlatform.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace OnlineCoursePlatform.Application.Features.TokenManagement.Commands.CreateToken
{
    public record CreateTokenCommand(User user) : IRequest<JwtSecurityToken>;
}
