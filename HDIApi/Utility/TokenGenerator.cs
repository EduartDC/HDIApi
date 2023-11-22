using HDIApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HDIApi.Utility
{
    public class TokenGenerator
    {
        private static IConfigurationRoot configuration;

        private static void InitializeConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            configuration = builder.Build();
        }

        public static TokenDTO GetToken(TokenDTO newToken)
        {
            if (configuration == null)
            {
                InitializeConfiguration();
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JWT:Key")));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", newToken.idUser.ToString()),
                new Claim("roleType", newToken.role),
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.Aes128CbcHmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
            );
            newToken.token = new JwtSecurityTokenHandler().WriteToken(token);
            return newToken;
        }
    }
}
