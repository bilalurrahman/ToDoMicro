using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Features.Register.Commands.AddUser
{
    public class RegisterUserRequest :IRequest<RegisterUserResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
