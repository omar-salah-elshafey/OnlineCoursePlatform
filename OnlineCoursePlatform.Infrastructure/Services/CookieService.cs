using Microsoft.AspNetCore.Http;
using OnlineCoursePlatform.Application.Interfaces;

namespace OnlineCoursePlatform.Infrastructure.Services
{
    public class CookieService : ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void SetRefreshTokenCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime(),
                Secure = true, // Set to true in production
                SameSite = SameSiteMode.Strict
            };
            _httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        public void SetUserIdCookie(string userId)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Set to true in production
                SameSite = SameSiteMode.Strict
            };
            _httpContextAccessor.HttpContext?.Response.Cookies.Append("userID", userId, cookieOptions);
        }

        public void SetUserNameCookie(string userName)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Set to true in production
                SameSite = SameSiteMode.Strict
            };
            _httpContextAccessor.HttpContext?.Response.Cookies.Append("userName", userName, cookieOptions);
        }

        public void RemoveFromCookies(string key)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(-1).ToLocalTime(),
                Secure = true, // Set to true in production
                SameSite = SameSiteMode.Strict
            };
            _httpContextAccessor.HttpContext?.Response.Cookies.Append(key, "", cookieOptions);
        }

        public string GetFromCookies(string key)
        {
            return _httpContextAccessor.HttpContext?.Request.Cookies[key];
        }
    }
}
