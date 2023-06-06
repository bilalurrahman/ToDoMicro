
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
            var secretKey = Encoding.ASCII.GetBytes(_iOptions.Value.SecretKey);
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
