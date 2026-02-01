namespace ETicketing.CA.API.Models.Responses
{
    public class SessionResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpires { get; set; }
    }
}
