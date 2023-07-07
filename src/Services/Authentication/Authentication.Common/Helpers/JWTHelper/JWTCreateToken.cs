
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
        private readonly IOptions<JWTTokenSettings> _iOptions;
        public JWTCreateToken( IOptions<JWTTokenSettings> iOptions)
        {
            //this.configuration = configuration;
            _iOptions = iOptions;
        }

        public async Task<string> GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

        public async Task<JWTModel> GenerateToken(string username, int id)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,username),
                    new Claim(ClaimTypes.Role,"User"),
                    new Claim("UserId",id.ToString())
                };
           
            return await CreateToken(claims);
               
        }

        private async Task<JWTModel> CreateToken(IEnumerable<Claim> claims)
        {  
            var secretKey = Encoding.ASCII.GetBytes(_iOptions.Value.SecretKey);
            var refreshExpiresAt = DateTime.UtcNow.AddMinutes(20);

            var jwt = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(20),
                signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(secretKey),
                        SecurityAlgorithms.HmacSha256Signature
                    )
                );

            return new JWTModel { RefreshTokenExpiry = refreshExpiresAt, Token = new JwtSecurityTokenHandler().WriteToken(jwt),RefreshToken= await GenerateRefreshToken() };
           
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_iOptions.Value.SecretKey)),
                ValidateLifetime = false 
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }


    }
}
