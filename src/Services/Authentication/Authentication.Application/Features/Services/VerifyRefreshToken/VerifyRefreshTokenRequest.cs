using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace Authentication.Application.Features.Token
{
    public class VerifyRefreshTokenRequest:IRequest<VerifyRefreshTokenResponse>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshExpiryDate { get; set; }
    }
}
