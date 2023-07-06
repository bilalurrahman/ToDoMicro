using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication.Domain.Entities;
using MediatR;
namespace Authentication.Application.Features.Token.Command.UpdateRefreshToken
{
    public class UpdateRefreshTokenRequest:RegisterUser,IRequest<UpdateRefreshTokenResponse>
    {
        public string refresh_token { get; set; }
        public DateTime refresh_token_expiry { get; set; }
    }
}
