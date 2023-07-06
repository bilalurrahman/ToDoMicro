
using MediatR;

namespace Authentication.Application.Features.Login
{
     public class LoginRequest :IRequest<LoginResponse>
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
