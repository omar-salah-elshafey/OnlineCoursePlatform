namespace OnlineCoursePlatform.Application.Features.Authentication.Dtos
{
    public class AuthResponseModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string AccessToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiresOn { get; set; }
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
