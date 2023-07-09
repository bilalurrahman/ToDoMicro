using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace Authentication.Application.Features.Client.Login
{
    public class ClientLoginRequest:IRequest<ClientLoginResponse>
    {
        public string ClientUsername{ get; set; }
        public string ClientPassword { get; set; }
    }
}
