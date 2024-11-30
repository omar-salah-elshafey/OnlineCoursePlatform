using Microsoft.AspNetCore.Http;

namespace OnlineCoursePlatform.Application.Interfaces
{
    public interface ICookieService
    {
        void SetRefreshTokenCookie(string refreshToken, DateTime expires);
        void SetUserIdCookie(string userId);
        void SetUserNameCookie(string userName);

        void RemoveFromCookies(string key);
        //void RemoveRefreshTokenCookie();
        //void RemoveUserIdCookie();
        //void RemoveUserNameCookie();
    }
}
