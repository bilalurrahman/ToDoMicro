using System;

namespace Authentication.Common.Helpers.JWTHelper
{
    public class JWTModel
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
