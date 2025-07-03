using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using myapi.Interfaces;
using myapi.Models;

namespace myapi.Service
{
    class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        //khoa doi xung de ky token 
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _config = config;
            // do trong config la string de su dung duoc trong jwt thi phai dang byte
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
        }

        public string CreatetToken(AppUser user)
        {
            var claim = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName,user.FullName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                 new Claim(JwtRegisteredClaimNames.Sub, user.Id)

            };
            //ki token bang thuat toan hmacsha
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };
            var tokenHandle = new JwtSecurityTokenHandler();
            var token = tokenHandle.CreateToken(tokenDescriptor);
            return tokenHandle.WriteToken(token);
        }
    }
}