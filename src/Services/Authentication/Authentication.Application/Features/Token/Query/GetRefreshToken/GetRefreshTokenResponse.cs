using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Features.Token.Query.GetRefreshToken
{
    public class GetRefreshTokenResponse
    {
        public string refresh_token { get; set; }
        public DateTime refresh_token_expiry { get; set; }
    }
}
