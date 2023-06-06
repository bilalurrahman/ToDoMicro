
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Common.Helpers.JWTHelper
{
    public class JWTCreateToken: IJWTCreateToken
    {
        private readonly IConfiguration configuration;
        public JWTCreateToken(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<JWTModel> Generate(string username)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,username),
                    new Claim(ClaimTypes.Role,"User")
                };
           
            return CreateToken(claims);
               
        }

        private JWTModel CreateToken(IEnumerable<Claim> claims)
        {
            var secretKey = Encoding.ASCII.GetBytes(configuration.GetValue<string>("SecretKey"));
            var expiresAt = DateTime.UtcNow.AddMinutes(20);

            var jwt = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expiresAt,
                signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(secretKey),
                        SecurityAlgorithms.HmacSha256Signature
                    )
                );

            return new JWTModel { Expiry = expiresAt, Token = new JwtSecurityTokenHandler().WriteToken(jwt) };
           
        }
    }
}
