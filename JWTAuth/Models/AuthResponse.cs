namespace JWTAuth.Models
{
    public class AuthResponse
    {
        public string UserName { get; set; }
        public string JwtToken { get; set; }
        public string ExpiresIn { get; set; }

    }
}
