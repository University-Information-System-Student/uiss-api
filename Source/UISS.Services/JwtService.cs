namespace UISS.Services
{
    using System;
    using System.Text;
    using System.Security.Claims;
    using System.IdentityModel.Tokens.Jwt;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    using Services.Contracts;
    using GlobalConstants.ApplicationSettings;

    public class JwtService : IJwtService
    {
        private readonly SecuritySettings securitySettings;

        public JwtService(IOptions<ApplicationSettings> options)
        {
            this.securitySettings = options.Value.Security;
        }

        public string GenerateJwt(Guid id, string userName, string userRole)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(this.securitySettings.SecretKey));

            var algorithm = SecurityAlgorithms.HmacSha256Signature;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(key, algorithm),
                Expires = DateTime.UtcNow.AddHours(this.securitySettings.JwtLifetime),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, userRole)
                })
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
