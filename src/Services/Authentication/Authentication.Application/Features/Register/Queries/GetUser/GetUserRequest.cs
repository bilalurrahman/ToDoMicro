using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Features.Register.Queries.GetUser
{
    public class GetUserRequest : IRequest<GetUserResponse>
    {
        public string username { get; set; }
    }
}
