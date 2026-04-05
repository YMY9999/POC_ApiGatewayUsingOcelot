using JWTAuth.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuth.Handlers
{
    public class JwtTokenHandler
    {
        public const string SecreytKey = "%^%&*^#$%4dKJSmwc45wqwq@#@#@!@#!@#!@SDASDAS%^%^%^%";
        public const int Jwt_TimeOut = 15;

        private readonly List<UserAccount> _userAccount;

        public JwtTokenHandler()
        {
            _userAccount = new List<UserAccount>
            {
                new UserAccount() { UserName = "admin", Password = "admin", Role = "Administrator" },
                new UserAccount() { UserName = "user", Password = "user", Role = "User" },
            };

        }
        public AuthResponse GenerateJwtToken(AuthRequest _authRequest)
        {
            if (string.IsNullOrWhiteSpace(_authRequest.UserName) || string.IsNullOrWhiteSpace(_authRequest.Password))
                return null;

            var useraccount = _userAccount.FirstOrDefault(x => x.UserName == _authRequest.UserName && x.Password == _authRequest.Password);
            if (useraccount == null) return null;

            var TokenExpiresIn = DateTime.UtcNow.AddMinutes(Jwt_TimeOut);
            var TokenKey = Encoding.ASCII.GetBytes(SecreytKey);
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, _authRequest.UserName),
                new Claim(ClaimTypes.Role, useraccount.Role)
            });

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(TokenKey),
                SecurityAlgorithms.HmacSha256Signature);

            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = TokenExpiresIn,
                SigningCredentials = signingCredentials
            };

            var jwtsecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtsecurityTokenHandler.CreateToken(TokenDescriptor);
            var tokenString = jwtsecurityTokenHandler.WriteToken(securityToken);

            return new AuthResponse
            {
                UserName = _authRequest.UserName,
                JwtToken = tokenString,
                ExpiresIn = TokenExpiresIn.ToString("HH:mm:ss")
            };
        }
    }
}