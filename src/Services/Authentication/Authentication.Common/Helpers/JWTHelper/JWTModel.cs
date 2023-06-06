using System;

namespace Authentication.Common.Helpers.JWTHelper
{
    public class JWTModel
    {
        public string Token { get; set; }
        public DateTime Expiry { get; set; }
    }
}
