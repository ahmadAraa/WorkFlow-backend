using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Business.Services
{
    public class JwtSettings
    {
        public string Token { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
    public class JwtService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string CreateToken(User user)
        {
            // Add null and empty checks for robustness
            if (string.IsNullOrEmpty(_jwtSettings.Token) ||
                string.IsNullOrEmpty(_jwtSettings.Issuer) ||
                string.IsNullOrEmpty(_jwtSettings.Audience))
            {
                throw new InvalidOperationException("JWT configuration values are missing.");
            }

            var claims = new List<Claim>
            {
                // Use a unique ID for the user
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                // Add the unique token ID claim
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 
                // Add the username as a custom claim for convenience
                new Claim("email", user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Token));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer, // Add the issuer here
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}