using MediatR;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.TokenManagement.Commands.GenerateRefreshToken
{
    public record GenerateRefreshTokenCommand : IRequest<RefreshToken>;
}
