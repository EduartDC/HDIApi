using HDIApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.IdentityModel.Tokens;

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
            

            var claims = new[]
            {
                new Claim(ClaimTypes.Sid, newToken.idUser),
                new Claim(ClaimTypes.Name, newToken.fullName),
                new Claim(ClaimTypes.Role, newToken.role),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JWT:Key")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );

            newToken.token = new JwtSecurityTokenHandler().WriteToken(token);
            return newToken;
        }
    }
}
