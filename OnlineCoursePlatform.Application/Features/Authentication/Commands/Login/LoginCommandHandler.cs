using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OnlineCoursePlatform.Application.Features.Authentication.Commands.RegisterUser;
using OnlineCoursePlatform.Application.Features.Authentication.Dtos;
using OnlineCoursePlatform.Application.Features.TokenManagement.Commands.CreateToken;
using OnlineCoursePlatform.Application.Features.TokenManagement.Commands.GenerateRefreshToken;
using OnlineCoursePlatform.Application.Interfaces;
using OnlineCoursePlatform.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace OnlineCoursePlatform.Application.Features.Authentication.Commands.Login
{
    public class LoginCommandHandler(UserManager<User> _userManager, ILogger<RegisterUserCommandHandler> _logger,
        IMediator _mediator, ICookieService _cookieService) : IRequestHandler<LoginCommand, AuthResponseModel>
    {
        public async Task<AuthResponseModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting the Login Process....");
            var loginDto = request.loginDto;
            var authResponseModel = new AuthResponseModel();
            var user = await _userManager.FindByNameAsync(loginDto.EmailOrUserName)
               ?? await _userManager.FindByEmailAsync(loginDto.EmailOrUserName); //check if the user exists
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                authResponseModel.IsAuthenticated = false;
                authResponseModel.Message = "Invalid Email or Password!";
                _logger.LogWarning(authResponseModel.Message);
                return authResponseModel;
            }
            if (user.IsDeleted)
            {
                authResponseModel.Message = "User Not Found!";
                _logger.LogWarning(authResponseModel.Message);
                return authResponseModel;
            }
            if (!user.EmailConfirmed)
                return new AuthResponseModel { Message = "Please Confirm Your Email First." };
            var jwtSecurityToken = await _mediator.Send(new CreateTokenCommand(user));
            authResponseModel.IsAuthenticated = true;
            authResponseModel.Email = user.Email;
            authResponseModel.ExpiresAt = jwtSecurityToken.ValidTo;
            authResponseModel.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? "User";
            authResponseModel.AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authResponseModel.Username = user.UserName;
            authResponseModel.IsConfirmed = true;
            user.IsActive = true;

            //checj if the user already has an active refresh token
            if (!user.RefreshTokens.Any(t => t.IsActive))
            {
                _logger.LogInformation("Generate a new refresh token and add it to the user's tokens...");
                // Generate a new refresh token and add it to the user's tokens
                var refreshToken = await _mediator.Send(new GenerateRefreshTokenCommand());
                user.RefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(user);

                // Send the refresh token along with the JWT token
                authResponseModel.RefreshToken = refreshToken.Token;
                authResponseModel.RefreshTokenExpiresOn = refreshToken.ExpiresOn;
                _cookieService.SetRefreshTokenCookie(refreshToken.Token, refreshToken.ExpiresOn);
            }
            else
            {
                _logger.LogInformation("Getting the active refreshToken...");
                var activeToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                authResponseModel.RefreshToken = activeToken.Token;
                authResponseModel.RefreshTokenExpiresOn = activeToken.ExpiresOn;
                _cookieService.SetRefreshTokenCookie(activeToken.Token, activeToken.ExpiresOn);
            }
            _logger.LogInformation("User logged in..");
            _cookieService.SetUserIdCookie(user.Id);
            _cookieService.SetUserNameCookie(user.UserName);
            
            return authResponseModel;
        }
    }
}
