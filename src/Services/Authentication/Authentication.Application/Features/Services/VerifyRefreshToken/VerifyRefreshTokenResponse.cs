using System;


namespace Authentication.Application.Features.Token
{
    public class VerifyRefreshTokenResponse
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
    }
}

